using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Dwp.Adep.Framework.Management.IoC.ServiceLocation;
using Dwp.Adep.Framework.Management.DataServices.Models;
using System.Data.Common;
using System.Configuration;
using System.Data.Metadata.Edm;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public class UnitOfWork : IUnitOfWork, IDisposable 
    {
        // Readonly instance of EF ObjectContext
        private readonly IObjectContext _objectContext;

        // Store the user name
        private readonly string _userName;

        //Context for storing values of old entities for when auditing updates
        private IObjectContext _objectContextForOldEntities;

        // Audit ObjectSet
        protected readonly IObjectSet<Audit> _objectSetAudit;

        public UnitOfWork(string userName)
        {
            //Get context based upon repository
            _objectContext = SimpleServiceLocator.Instance.Get<IObjectContext>();
            _userName = userName;
        }

        public UnitOfWork(IObjectContext objectContext, string userName)
        {
            _objectContext = objectContext;
            _userName = userName;
        }

        public IObjectContext ObjectContext
        {
            get { return _objectContext; }
        }

        public void Dispose() 
        { 
            if (_objectContext != null) 
            { 
                _objectContext.Dispose(); 
            }
            GC.SuppressFinalize(this);
        }
  
        private T GetOriginalEntity<T>(ObjectStateEntry entry, T entity) where T : class, IAuditable
        {
            IObjectSet<T> objectSet = _objectContextForOldEntities.CreateObjectSet<T>();
            return _objectContextForOldEntities.GetObjectByKey(entry.EntityKey) as T;
        }

        public void Commit()
        {
            //Use existing connection
            DbConnection conn = _objectContext.Connection;
            
            // Open database connection
            conn.Open();

            // Create new transaction so that the normal updates and the audit updates are in a single transaciton
            DbTransaction transaction = conn.BeginTransaction();

            UpdateDatabase(transaction);

            // Commit the transaction here (ok to do so as we created it earlier)
            transaction.Commit();
            
        }

        #region Database Updates
        private void UpdateDatabase(DbTransaction transaction)
        {
            // Force Detectchanges (normally runs as part of SaveChanges but we need to preempt)
            _objectContext.DetectChanges();

            // Get entity references for entities which have changed
            IEnumerable<ObjectStateEntry> changes = _objectContext.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Modified);

            // List to store changes
            List<Audit> audits = new List<Audit>();

            // Iterate and process all update and delete changes. Uses Lists to force LINQ to be evaluated at this point.
            List<ObjectStateEntry> deleteChanges = changes.Where(x => x.State == System.Data.EntityState.Deleted).ToList();
            List<ObjectStateEntry> updateChanges = changes.Where(x => x.State == System.Data.EntityState.Modified).ToList();
            List<ObjectStateEntry> addChanges = changes.Where(x => x.State == System.Data.EntityState.Added).ToList();

            //Get context for retrieving old versions of Entities
             _objectContextForOldEntities = SimpleServiceLocator.Instance.Get<IObjectContext>();

            // Process updates
            audits.AddRange(AuditUpdateRecords(updateChanges));

            // Process deletes
            audits.AddRange(AuditDeleteRecords(deleteChanges));

            // Save changes to the database. This will also cause any database identity field values to be generated
            _objectContext.SaveChanges();

            // Process adds
            audits.AddRange(AuditAddedRecords(addChanges));

            IObjectSet<Audit> auditObjectSet = _objectContext.CreateObjectSet<Audit>();

            foreach (Audit auditToSave in audits)
            {
                auditObjectSet.AddObject(auditToSave);
            }

            // Save Audit records to database
            _objectContext.SaveChanges();
        }

        #endregion

        #region Audit Updates
        //Create 'Update' audit records
        private List<Audit> AuditUpdateRecords(List<ObjectStateEntry> updatedChanges)
        {
            Audit auditItem = null;
            List<Audit> audits = new List<Audit>();

            //Check if Auditing is enabled
            if (bool.Parse(ConfigurationManager.AppSettings.Get("EnableLogging")))
            {
                // Process Updates
                foreach (ObjectStateEntry stateEntry in updatedChanges)
                {
                    if (!stateEntry.IsRelationship && stateEntry.Entity != null)
                    {
                        Type t;
                        //if object is a dynamic proxy, then get its basetype
                        t = stateEntry.Entity.GetType().Namespace == "System.Data.Entity.DynamicProxies" ? stateEntry.Entity.GetType().BaseType : stateEntry.Entity.GetType();

                        //Create an instance of t. This is then used in the GetOriginalEntity method to infer the Type.
                        dynamic Empty_t = Activator.CreateInstance(t);

                        IAuditable oldEntity = GetOriginalEntity(stateEntry, Empty_t);

                        
                        StringBuilder sb = new StringBuilder();

                        //Check if Detailed Logging is Enabled
                        if (bool.Parse(ConfigurationManager.AppSettings.Get("EnableDetailedLogging")))
                        {

                            foreach (EdmMember member in ((stateEntry.EntitySet.ElementType.Members).Where(x => x.TypeUsage.EdmType is PrimitiveType && x.Name != "RowIdentifier")))
                            {
                                string propertyName = member.Name;
                                var prop = stateEntry.Entity.GetType().GetProperties().Where(x => x.Name == propertyName).Single();
                                var newValue = prop.GetValue(stateEntry.Entity, null);

                                dynamic oldValue = oldEntity.GetType().GetProperty(propertyName).GetValue(oldEntity, null);
                                if (!object.Equals(newValue, oldValue))
                                {
                                    sb.AppendLine(string.Format("PropertyName:{0} OldValue:{1}, NewValue:{2}", propertyName, oldValue, newValue));
                                }
                            }
                        }
                        else
                        {
                            sb.AppendLine("Record Updated");
                        }

                        auditItem = CreateAudit(stateEntry.Entity, "Update", sb.ToString(), Empty_t.GetType());
                        audits.Add(auditItem);
                    }
                }
            }

            return audits;
        }

        #endregion

        #region Audit Deletes
        //Create 'Delete' audit records
        private List<Audit> AuditDeleteRecords(List<ObjectStateEntry> deletedChanges)
        {
            Audit auditItem = null;
            List<Audit> audits = new List<Audit>();

            //Check if Auditing is enabled
            if (bool.Parse(ConfigurationManager.AppSettings.Get("EnableLogging")))
            {
                // Process Deletes
                foreach (ObjectStateEntry stateEntry in deletedChanges)
                {
                    if (!stateEntry.IsRelationship && stateEntry.Entity != null)
                    {
                        Type t;
                        //if object is a dynamic proxy, then get its basetype
                        t = stateEntry.Entity.GetType().Namespace == "System.Data.Entity.DynamicProxies" ? stateEntry.Entity.GetType().BaseType : stateEntry.Entity.GetType();

                        //Create an instance of t. This is then used in the GetOriginalEntity method to infer the Type.
                        dynamic Empty_t = Activator.CreateInstance(t);

                        //Get the original entitity so we can check the old values
                        IAuditable oldEntity = GetOriginalEntity(stateEntry, Empty_t);

                        StringBuilder sb = new StringBuilder();
                        //Check if Detailed Logging is Enabled
                        if (bool.Parse(ConfigurationManager.AppSettings.Get("EnableDetailedLogging")))
                        {
                            foreach (EdmMember member in ((stateEntry.EntitySet.ElementType.Members).Where(x => x.TypeUsage.EdmType is PrimitiveType && x.Name != "RowIdentifier")))
                            {
                                string propertyName = member.Name;
                                var oldValue = oldEntity.GetType().GetProperty(propertyName).GetValue(oldEntity, null);
                                sb.AppendLine(string.Format("PropertyName:{0}, Value:{1};", propertyName, oldValue));
                            }
                        }
                        else
                        {
                            sb.AppendLine("Record Deleted");
                        }

                        auditItem = CreateAudit(stateEntry.Entity, "Delete", sb.ToString(), Empty_t.GetType());
                        audits.Add(auditItem);
                    }
                }
            }

            return audits;
        }

        #endregion

        #region Audit Inserts
        //Create 'Insert' audit records
        private List<Audit> AuditAddedRecords(List<ObjectStateEntry> addedChanges)
        {
            Audit auditItem = null;
            List<Audit> audits = new List<Audit>();

            //Check if Auditing is enabled
            if (bool.Parse(ConfigurationManager.AppSettings.Get("EnableLogging")))
            {
                // Process Adds. This is delayed (i.e it is done AFTER save changes is called) to pick up any identity field values that will now have been populated
                foreach (ObjectStateEntry stateEntry in addedChanges)
                {
                    if (!stateEntry.IsRelationship && stateEntry.Entity != null)
                    {
                        Type t;
                        //if object is a dynamic proxy, then get its basetype
                        t = stateEntry.Entity.GetType().Namespace == "System.Data.Entity.DynamicProxies" ? stateEntry.Entity.GetType().BaseType : stateEntry.Entity.GetType();

                        //Create an instance of t. This is then used in the GetOriginalEntity method to infer the Type.
                        dynamic Empty_t = Activator.CreateInstance(t);

                        StringBuilder sb = new StringBuilder();

                        //Check if Detailed Logging is Enabled
                        if (bool.Parse(ConfigurationManager.AppSettings.Get("EnableDetailedLogging")))
                        {
                            foreach (EdmMember member in ((stateEntry.EntitySet.ElementType.Members).Where(x => x.TypeUsage.EdmType is PrimitiveType && x.Name != "RowIdentifier")))
                            {
                                string propertyName = member.Name;
                                string currentValue = stateEntry.CurrentValues[propertyName].ToString();
                                sb.AppendLine(string.Format("PropertyName:{0}, Value:{1};", propertyName, currentValue));
                                //sb.AppendLine(string.Format("<property><name>{0}</name><value>{1}</value></property>", propertyName, currentValue));
                            }
                        }
                        else
                        {
                            sb.AppendLine("Record Added");
                        }
                        auditItem = CreateAudit(stateEntry.Entity, "Insert", sb.ToString(), Empty_t.GetType());
                        audits.Add(auditItem);
                    }
                }
            }

            return audits;
        }

        #endregion

        private Audit CreateAudit<T>(T entity, string action, string auditText, Type objectType)
        {
            //Creating Audit Trail
            Audit audit = new Audit();
            audit.Code = Guid.NewGuid();
            audit.AuditAction = action;
            audit.AuditText = auditText;
            audit.ChangedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name + " : " + _userName;
            audit.DateUpdated = DateTime.Now;
            audit.ObjectCode = entity.GetType().GetProperty("Code").GetValue(entity, null).ToString();
            audit.TypeOfObject = objectType.FullName;
            return audit;
        }
    } 
}