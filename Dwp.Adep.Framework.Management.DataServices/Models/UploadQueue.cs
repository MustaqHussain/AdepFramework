//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace Dwp.Adep.Framework.Management.DataServices.Models
{
    public partial class UploadQueue : IAuditable, IActiveAware 
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual Nullable<System.Guid> ApplicationCode
        {
            get { return _applicationCode; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_applicationCode != value)
                    {
                        if (Application != null && Application.Code != value)
                        {
                            Application = null;
                        }
                        _applicationCode = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<System.Guid> _applicationCode;
    
        public virtual string TempTableName
        {
            get;
            set;
        }
    
        public virtual string StoredProcedure
        {
            get;
            set;
        }
    
        public virtual string ConnectionString
        {
            get;
            set;
        }
    
        public virtual string UploadFileName
        {
            get;
            set;
        }
    
        public virtual string Status
        {
            get;
            set;
        }
    
        public virtual bool IsActive
        {
            get;
            set;
        }
    
        public virtual byte[] RowIdentifier
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual Application Application
        {
            get { return _application; }
            set
            {
                if (!ReferenceEquals(_application, value))
                {
                    var previousValue = _application;
                    _application = value;
                    FixupApplication(previousValue);
                }
            }
        }
        private Application _application;
    
        public virtual ICollection<UploadErrorLog> UploadErrorLog
        {
            get
            {
                if (_uploadErrorLog == null)
                {
                    var newCollection = new FixupCollection<UploadErrorLog>();
                    newCollection.CollectionChanged += FixupUploadErrorLog;
                    _uploadErrorLog = newCollection;
                }
                return _uploadErrorLog;
            }
            set
            {
                if (!ReferenceEquals(_uploadErrorLog, value))
                {
                    var previousValue = _uploadErrorLog as FixupCollection<UploadErrorLog>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupUploadErrorLog;
                    }
                    _uploadErrorLog = value;
                    var newValue = value as FixupCollection<UploadErrorLog>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupUploadErrorLog;
                    }
                }
            }
        }
        private ICollection<UploadErrorLog> _uploadErrorLog;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupApplication(Application previousValue)
        {
            if (previousValue != null && previousValue.UploadQueue.Contains(this))
            {
                previousValue.UploadQueue.Remove(this);
            }
    
            if (Application != null)
            {
                if (!Application.UploadQueue.Contains(this))
                {
                    Application.UploadQueue.Add(this);
                }
                if (ApplicationCode != Application.Code)
                {
                    ApplicationCode = Application.Code;
                }
            }
            else if (!_settingFK)
            {
                ApplicationCode = null;
            }
        }
    
        private void FixupUploadErrorLog(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (UploadErrorLog item in e.NewItems)
                {
                    item.UploadQueue = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UploadErrorLog item in e.OldItems)
                {
                    if (ReferenceEquals(item.UploadQueue, this))
                    {
                        item.UploadQueue = null;
                    }
                }
            }
        }

        #endregion
    }
}
