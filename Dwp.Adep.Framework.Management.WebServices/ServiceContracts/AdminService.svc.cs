using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.WebServices.Exceptions;
using System.IO;
using System.Data;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    public partial class AdminService : IAdminService
    {
        AdepDatabaseEntities ObjContext = new AdepDatabaseEntities();

        IRepository<DataServices.Models.Audit> auditRepository;

        public AdminService()
        {
            BootStrapper.InitializeIoc();

            DefineTypeMappings();
        }

        partial void DefineTypeMappings();        

        #region Get Staff Application Administration By Staff Code and Application Code

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentUserName"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public StaffApplicationAdminVMDC GetStaffApplicationAdministrationByStaffCodeAndApplicationCode(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode,Guid staffCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUserName);

            // Create repository
            // Create repositories for lookup data
            IRepository<Application> applicationRepository = new Repository<Application>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<StaffAttributes> staffAttributesRepository = new Repository<StaffAttributes>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<StaffOrganisation> staffOrganisationsRepository = new Repository<StaffOrganisation>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<Organisation> organisationsRepository = new Repository<Organisation>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            // Get ApplicationAttribute
            return GetStaffApplicationAdministrationByStaffCodeAndApplicationCode(userName, currentUserName, appID, overrideID, applicationCode, staffCode, uow, applicationRepository
            , staffRepository, staffAttributesRepository, staffOrganisationsRepository, organisationsRepository
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentUserName"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public StaffApplicationAdminVMDC GetStaffApplicationAdministrationByStaffCodeAndApplicationCode(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode, Guid staffCode, IUnitOfWork uow
            , IRepository<Application> applicationRepository, IRepository<Staff> staffRepository,IRepository<StaffAttributes> staffAttributesRepository,IRepository<StaffOrganisation> staffOrganisationsRepository,IRepository<Organisation> organisationsRepository
            )
        {
            try
            {
                using (uow)
                {
                    // Create aggregate contract
                    StaffApplicationAdminVMDC message = new StaffApplicationAdminVMDC();

                    // Retrieve specific Application with child data
                    Application applicationEntity = applicationRepository.Single(new Specification<Application>(x => x.Code == applicationCode),
                        "ApplicationAttribute", /*All Attributes for the application*/
                        "ApplicationOrganisationTypeGroup", /*All the Applications' OrganisationTypeGroups*/
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup", /*The Organisation Type Groups For the App  */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType", /*The Organisation Types For the app */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType.Organisation", /*All Organisations For the app */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType.Organisation.OrganisationHierarchy", /*All ancestors in organisationns For the app */
                        "ApplicationOrganisationTypeGroup.Organisation");/*Root Organisation For App*/

                    //Retreive staff entity
                    Staff staffEntity = staffRepository.Single(new Specification<Staff>(x => x.Code == staffCode),
                        "StaffOrganisation" /*All organisations that the staff member has*/,
                        "StaffOrganisation.Organisation"); /*All organisations that the staff member has*/

                    //All staff attributes for this application
                    var StaffAttributes = staffAttributesRepository.Find(new Specification<StaffAttributes>(x => x.ApplicationCode == applicationCode && x.StaffCode == staffCode));

                    //All stafforganisations for this staff and application with child organisation objects 
                    var StaffOrganisations = staffOrganisationsRepository.Find(new Specification<StaffOrganisation>(x => x.ApplicationCode == applicationCode && x.StaffCode == staffCode), x => x.Organisation.Name, "Organisation");

                    // Convert to data contract for passing through service interface
                    message.SelectedApplicationCode = applicationEntity.Code;

                    //Set the staff dc
                    message.StaffItem = Mapper.Map<Staff, StaffDC>(staffEntity);

                    List<OrganisationByTypeVMDC> orgsByType = new List<OrganisationByTypeVMDC>();

                    //Combine the appplication attributes with the staff attributes
                    message.StaffApplicationAttributeList = new List<StaffApplicationAttributeVMDC>();
                    foreach (ApplicationAttribute AttributeItem in applicationEntity.ApplicationAttribute)
                    {
                        message.StaffApplicationAttributeList.Add(new StaffApplicationAttributeVMDC
                        {
                            ApplicationAttributeItem = Mapper.Map<ApplicationAttributeDC>(AttributeItem),
                            StaffAttributeItem = Mapper.Map<StaffAttributesDC>(StaffAttributes.SingleOrDefault(x => x.ApplicationAttributeCode == AttributeItem.Code))
                        });
                    }

                    //Staff organisation list
                    message.StaffOrganisationList = Mapper.Map<IEnumerable<StaffOrganisation>, IEnumerable<StaffOrganisationDC>>(StaffOrganisations).ToList();
                    foreach (var staffOrgitem in message.StaffOrganisationList)
                    {
                        var StaffOrganisationItem = StaffOrganisations.Select(x => x.Organisation).All(x => x.Code == staffOrgitem.OrganisationCode);
                        var PathArray = organisationsRepository.Find(
                            new Specification<Organisation>(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == staffOrgitem.OrganisationCode)), x => new { x.OrganisationType.LevelNumber }).Select(x => x.Name);
                        staffOrgitem.OrganisationPath = PathArray.ToList();
                    }

                    message.OrganisationsByTypesList = new List<OrganisationByTypeVMDC>();
                    foreach (ApplicationOrganisationTypeGroup AppGroup in applicationEntity.ApplicationOrganisationTypeGroup)
                    {
                        var OrgGroup = AppGroup.OrganisationTypeGroup;
                        foreach (var OrgTypeItem in OrgGroup.OrganisationType.Where(x=>x.LevelNumber >= AppGroup.Organisation.OrganisationType.LevelNumber).OrderBy(x=>x.LevelNumber))
                        {
                            message.OrganisationsByTypesList.Add(new OrganisationByTypeVMDC()
                            {
                                OrganisationTypeItem = Mapper.Map<OrganisationTypeDC>(OrgTypeItem),
                                OrganisationList = Mapper.Map<IEnumerable<Organisation>, IEnumerable<OrganisationDC>>(OrgTypeItem.Organisation.OrderBy(x=>x.Name)).ToList()
                            });
                            foreach(var item in message.OrganisationsByTypesList.Last().OrganisationList)
                            {
                                var OrganisationToFind = OrgTypeItem.Organisation.Single(x => x.Code == item.Code);
                                var Parent = OrganisationToFind.OrganisationHierarchy.SingleOrDefault(x => x.ImmediateParent == true);
                                if (Parent != null) { item.ParentID = Parent.AncestorOrganisationCode; }
                            }
                            
                        }

                    }

                    if (applicationEntity.ApplicationOrganisationTypeGroup.Count() != 0)
                    {
                        message.RootNodeOrganisation = Mapper.Map<OrganisationDC>(applicationEntity.ApplicationOrganisationTypeGroup.First().Organisation);
                    }
                    
                    //NEED TO GET FROM LDAP FOR USER
                    message.RoleList = new List<string>();
                    
                    //message.ApplicationItem = destination;
                    //message.ApplicationList = applicationDestinationList;

                    return message;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }
        #endregion


        #region Get Application Organisations by Application Code

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentUserName"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ApplicationOrganisationSelectVMDC GetApplicationOrganisationsByApplicationCode(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUserName);

            // Create repository
            // Create repositories for lookup data
            IRepository<Application> applicationRepository = new Repository<Application>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<Organisation> organisationsRepository = new Repository<Organisation>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            // Get ApplicationAttribute
            return GetApplicationOrganisationsByApplicationCode(userName, currentUserName, appID, overrideID, applicationCode, uow, applicationRepository
            , organisationsRepository
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentUserName"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public ApplicationOrganisationSelectVMDC GetApplicationOrganisationsByApplicationCode(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode, IUnitOfWork uow
            , IRepository<Application> applicationRepository, IRepository<Organisation> organisationsRepository
            )
        {
            try
            {
                using (uow)
                {
                    // Create aggregate contract
                    ApplicationOrganisationSelectVMDC message = new ApplicationOrganisationSelectVMDC();

                    // Retrieve specific Application with child data
                    Application applicationEntity = applicationRepository.Single(new Specification<Application>(x => x.Code == applicationCode),
                        "ApplicationAttribute", /*All Attributes for the application*/
                        "ApplicationOrganisationTypeGroup", /*All the Applications' OrganisationTypeGroups*/
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup", /*The Organisation Type Groups For the App  */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType", /*The Organisation Types For the app */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType.Organisation", /*All Organisations For the app */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType.Organisation.OrganisationHierarchy", /*All ancestors in organisationns For the app */
                        "ApplicationOrganisationTypeGroup.Organisation");/*Root Organisation For App*/

                    // Convert to data contract for passing through service interface
                    message.SelectedApplicationCode = applicationEntity.Code;

                    List<OrganisationByTypeVMDC> orgsByType = new List<OrganisationByTypeVMDC>();

                    message.OrganisationsByTypesList = new List<OrganisationByTypeVMDC>();
                    foreach (ApplicationOrganisationTypeGroup AppGroup in applicationEntity.ApplicationOrganisationTypeGroup)
                    {
                        var OrgGroup = AppGroup.OrganisationTypeGroup;
                        foreach (var OrgTypeItem in OrgGroup.OrganisationType.Where(x => x.LevelNumber >= AppGroup.Organisation.OrganisationType.LevelNumber).OrderBy(x => x.LevelNumber))
                        {
                            message.OrganisationsByTypesList.Add(new OrganisationByTypeVMDC()
                            {
                                OrganisationTypeItem = Mapper.Map<OrganisationTypeDC>(OrgTypeItem),
                                OrganisationList = Mapper.Map<IEnumerable<Organisation>, IEnumerable<OrganisationDC>>(OrgTypeItem.Organisation.OrderBy(x => x.Name)).ToList()
                            });
                            foreach (var item in message.OrganisationsByTypesList.Last().OrganisationList)
                            {
                                var OrganisationToFind = OrgTypeItem.Organisation.Single(x => x.Code == item.Code);
                                var Parent = OrganisationToFind.OrganisationHierarchy.SingleOrDefault(x => x.ImmediateParent == true);
                                if (Parent != null) { item.ParentID = Parent.AncestorOrganisationCode; }
                            }

                        }

                    }

                    if (applicationEntity.ApplicationOrganisationTypeGroup.Count() != 0)
                    {
                        message.RootNodeOrganisation = Mapper.Map<OrganisationDC>(applicationEntity.ApplicationOrganisationTypeGroup.First().Organisation);
                    }

                    return message;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }
        #endregion


        #region GetOrganisation

        /// <summary>
        /// Retrieve a Organisation with associated lookups
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public ApplicationOrganisationAdminVMDC GetOrganisationWithParent(string currentUser, string user, string appID, string overrideID, Guid organisationCode, Guid applicationCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Organisation> dataRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Create repositories for lookup data
            IRepository<OrganisationHierarchy> organisationHierarchyRepository = new Repository<OrganisationHierarchy>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<OrganisationType> organisationTypeRepository = new Repository<OrganisationType>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Application> applicationRepository = new Repository<Application>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Call overload with injected objects
            return GetOrganisationWithParent(currentUser, user, appID, overrideID, organisationCode, applicationCode, uow, dataRepository
            , organisationTypeRepository, organisationHierarchyRepository, applicationRepository
            );
        }

        /// <summary>
        /// Retrieve a Organisation with associated lookups
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public ApplicationOrganisationAdminVMDC GetOrganisationWithParent(string currentUser, string user, string appID, string overrideID, Guid organisationCode, Guid applicationCode, IUnitOfWork uow, IRepository<Organisation> dataRepository
            , IRepository<OrganisationType> organisationTypeRepository, IRepository<OrganisationHierarchy> organisationHierarchyRepository, IRepository<Application> applicationRepository
            )
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {

                    OrganisationDC destination = null;
                    ApplicationOrganisationAdminVMDC returnObject = new ApplicationOrganisationAdminVMDC();

                    // If code is null then just return supporting lists
                    if (!(organisationCode == Guid.Empty))
                    {
                        // Retrieve specific Organisation
                        Organisation dataEntity = dataRepository.Single(x => x.Code == organisationCode);
                        var parentOrganisationLink = organisationHierarchyRepository.Find(new Specification<OrganisationHierarchy>(x => x.OrganisationCode == organisationCode && x.HopsBetweenOrgAndAncestor == 1), x => new { x.IsActive }, "Organisation1").SingleOrDefault();
                        Organisation parentOrganisation = null;
                        if (parentOrganisationLink != null)
                        {
                            parentOrganisation = parentOrganisationLink.Organisation1;
                        }
                       
                        //Determine if org has child orgs
                        int? maxHopsToChild = organisationHierarchyRepository.Find(x => x.AncestorOrganisationCode == organisationCode && x.OrganisationCode != organisationCode).Select(x=>x.HopsBetweenOrgAndAncestor).Max();
                        returnObject.MaximumHopsToChildOrganisation = maxHopsToChild.HasValue ? maxHopsToChild.Value : 0;
                        // Convert to data contract for passing through service interface
                        destination = Mapper.Map<Organisation, OrganisationDC>(dataEntity);
                        returnObject.ParentOrganisation = Mapper.Map<Organisation, OrganisationDC>(parentOrganisation);
                    }

                    IEnumerable<OrganisationType> organisationTypeList = organisationTypeRepository.Find(new Specification<OrganisationType>(x => x.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(y => y.ApplicationCode == applicationCode)), x => new { x.LevelNumber });

                    List<OrganisationTypeDC> organisationTypeDestinationList = Mapper.Map<List<OrganisationTypeDC>>(organisationTypeList);


                    
                    // Retrieve specific Application with child data
                    Application applicationEntity = applicationRepository.Single(new Specification<Application>(x => x.Code == applicationCode),
                        "ApplicationAttribute", /*All Attributes for the application*/
                        "ApplicationOrganisationTypeGroup", /*All the Applications' OrganisationTypeGroups*/
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup", /*The Organisation Type Groups For the App  */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType", /*The Organisation Types For the app */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType.Organisation", /*All Organisations For the app */
                        "ApplicationOrganisationTypeGroup.OrganisationTypeGroup.OrganisationType.Organisation.OrganisationHierarchy", /*All ancestors in organisationns For the app */
                        "ApplicationOrganisationTypeGroup.Organisation");/*Root Organisation For App*/

                    
                    List<OrganisationByTypeVMDC> orgsByType = new List<OrganisationByTypeVMDC>();

                    returnObject.OrganisationsByTypesList = new List<OrganisationByTypeVMDC>();
                    foreach (ApplicationOrganisationTypeGroup AppGroup in applicationEntity.ApplicationOrganisationTypeGroup)
                    {
                        var OrgGroup = AppGroup.OrganisationTypeGroup;
                        foreach (var OrgTypeItem in OrgGroup.OrganisationType.Where(x => x.LevelNumber >= AppGroup.Organisation.OrganisationType.LevelNumber).OrderBy(x => x.LevelNumber))
                        {
                            returnObject.OrganisationsByTypesList.Add(new OrganisationByTypeVMDC()
                            {
                                OrganisationTypeItem = Mapper.Map<OrganisationTypeDC>(OrgTypeItem),
                                OrganisationList = Mapper.Map<IEnumerable<Organisation>, IEnumerable<OrganisationDC>>(OrgTypeItem.Organisation.OrderBy(x => x.Name)).ToList()
                            });
                            foreach (var item in returnObject.OrganisationsByTypesList.Last().OrganisationList)
                            {
                                var OrganisationToFind = OrgTypeItem.Organisation.Single(x => x.Code == item.Code);
                                var Parent = OrganisationToFind.OrganisationHierarchy.SingleOrDefault(x => x.ImmediateParent == true);
                                if (Parent != null) { item.ParentID = Parent.AncestorOrganisationCode; }
                            }

                        }

                    }

                    if (applicationEntity.ApplicationOrganisationTypeGroup.Count() != 0)
                    {
                        returnObject.RootNodeOrganisation = Mapper.Map<OrganisationDC>(applicationEntity.ApplicationOrganisationTypeGroup.First().Organisation);
                    }
                    // Create aggregate contract
                    
                    returnObject.OrganisationItem = destination;
                    returnObject.AllTypesForApplication = organisationTypeDestinationList;

                    return returnObject;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion


        #region Update

        /// <summary>
        /// Update a Organisation
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        public ApplicationOrganisationAdminVMDC UpdateOrganisationForApplication(string currentUser, string user, string appID, string overrideID, OrganisationDC dc, Guid parentOrganisationCode, Guid applicationCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Organisation> organisationRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Organisation> organisationRepositoryForRead = new Repository<Organisation>(currentUser, user, appID, overrideID); //<----USES DIFFERENT CONTEXT
            IRepository<OrganisationHierarchy> organisationHierarchyRepository = new Repository<OrganisationHierarchy>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<OrganisationType> organisationTypeRepository = new Repository<OrganisationType>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Call overload with injected objects
            return UpdateOrganisationForApplication(currentUser, user, appID, overrideID, dc, parentOrganisationCode, applicationCode, organisationRepository, organisationHierarchyRepository, organisationTypeRepository, organisationRepositoryForRead, uow);
        }

        /// <summary>
        /// Update a Organisation
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public ApplicationOrganisationAdminVMDC UpdateOrganisationForApplication(string currentUser, string user, string appID, string overrideID, OrganisationDC dc, Guid parentOrganisationCode, Guid applicationCode, IRepository<Organisation> organisationRepository,
            IRepository<OrganisationHierarchy> organisationHierarchyRepository, IRepository<OrganisationType> organisationTypeRepository, IRepository<Organisation> organisationRepositoryForRead, IUnitOfWork uow)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == dc) throw new ArgumentOutOfRangeException("dc");
                if (null == organisationRepository) throw new ArgumentOutOfRangeException("organisationRepository");
                if (null == organisationHierarchyRepository) throw new ArgumentOutOfRangeException("organisationHierarchyRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                bool OrganisationAlreadyExists = false;

                using (uow)
                {
                    // Map data contract to model
                    Organisation destination = Mapper.Map<OrganisationDC, Organisation>(dc);
                    organisationRepository.Update(destination);


                    var siblinsAndSelf = organisationHierarchyRepository.Find(x => x.AncestorOrganisationCode == parentOrganisationCode && x.HopsBetweenOrgAndAncestor == 1,"Organisation");
                    var organisationCount = siblinsAndSelf.Count(x=>x.Organisation.Name.Equals(destination.Name));

                    //if existing organisation already exists with the name
                    if (organisationCount != null && organisationCount > 1)
                    {
                        OrganisationAlreadyExists = true;
                    }

                    else
                    {
                        organisationRepository.Update(destination);

                        //Get All Types for application
                        IEnumerable<OrganisationType> organisationTypeList = organisationTypeRepository.Find
                            (new Specification<OrganisationType>(x => x.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(y => y.ApplicationCode == applicationCode)), x => new { x.LevelNumber });


                        //Find Current Parent Organisation
                        Organisation CurrentParent = organisationRepository.Find(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == destination.Code && y.HopsBetweenOrgAndAncestor == 1)).SingleOrDefault();

                        //If new parentOrganisationCode is different then need to change the org hierarchy and child links and possibly organisation types
                        if ((CurrentParent == null && parentOrganisationCode != Guid.Empty) || CurrentParent.Code != parentOrganisationCode)
                        {
                            //Get the ancestor hierarchies of the organisation this will contain the Codes of all organisations above in the path
                            var ExistingAncestorHierarchies = organisationHierarchyRepository.Find(x => x.OrganisationCode == destination.Code && x.AncestorOrganisationCode != destination.Code); //Find all oh where organisationcode is current org

                            //get the child hierarchies of the organisation this will contain the Codes of all organisations below in the path
                            var ExistingChildHierarchies = organisationHierarchyRepository.Find(x => x.AncestorOrganisationCode == destination.Code && x.OrganisationCode != destination.Code);//find all oh where ancestorOrgcode is current org

                            //Removes all links between the org,its children with it's ancestors.
                            UnlinkExistingOrganisations(organisationHierarchyRepository, destination, CurrentParent, ExistingAncestorHierarchies, ExistingChildHierarchies);

                            //******************************************************************************************************************************************************
                            //******************************************AT THIS POINT ALL LINKS THAT NEED TO BE, ARE SEVERRED*******************************************************
                            //******************************************************************************************************************************************************

                            var NewOrgType = organisationTypeRepository.Single(x => x.Code == destination.OrganisationTypeCode);
                            var existingorgtypecode = organisationRepositoryForRead.Single(x => x.Code == destination.Code, "OrganisationType").OrganisationTypeCode;
                            var ExisitingOrgType = organisationTypeRepository.Single(y => y.Code == existingorgtypecode);

                            //Adds new links between the org,its children with the new ancestors
                            LinkOrganisationStructureToNewAncestors(parentOrganisationCode, organisationRepository, organisationHierarchyRepository, organisationRepositoryForRead, destination, organisationTypeList, ExistingChildHierarchies, NewOrgType, ExisitingOrgType);
                        }

                        // Add the new item
                        //organisationRepository.Update(destination);

                        // Commit unit of work
                        uow.Commit();

                        dc = Mapper.Map<Organisation, OrganisationDC>(destination);
                    }
                }

                    // Create new data contract to return - BIG CHEAT JUST CALL THE GET WITH PARENT SERVICE METHOD AND ALLOW IT TO USE NEW CONTEXT - DON@T CARE ABOUT PERFORMANCE ON RELATIVELY LIGHTLY USED FUNCTION                    
                ApplicationOrganisationAdminVMDC returnObject = GetOrganisationWithParent(currentUser, user, appID, overrideID, dc.Code, applicationCode);

                if (OrganisationAlreadyExists)
                {
                    returnObject.Message = "Update failed as Organisation with the same name already exists";
                }

                return returnObject;

            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }

        #region private methods to sort hierarchy links
        /// <summary>
        /// //Adds new links between the org,its children with the new ancestors
        /// </summary>
        /// <param name="parentOrganisationCode"></param>
        /// <param name="organisationRepository"></param>
        /// <param name="organisationHierarchyRepository"></param>
        /// <param name="organisationRepositoryForRead"></param>
        /// <param name="destination"></param>
        /// <param name="organisationTypeList"></param>
        /// <param name="ExistingChildHierarchies"></param>
        /// <param name="OrgType"></param>
        private static void LinkOrganisationStructureToNewAncestors(Guid parentOrganisationCode, IRepository<Organisation> organisationRepository, IRepository<OrganisationHierarchy> organisationHierarchyRepository, IRepository<Organisation> organisationRepositoryForRead, Organisation destination, IEnumerable<OrganisationType> organisationTypeList, IEnumerable<OrganisationHierarchy> ExistingChildHierarchies, OrganisationType newOrgType, OrganisationType existingOrgType)
        {
            
            //Find all Ancestors of the new parent - this will contain the Codes of all organiosations above in the path INCLUDING PARENT ITSELF
            var NewAncestors = organisationHierarchyRepository.Find(x => x.OrganisationCode == parentOrganisationCode);

            //Loop through the ancestors for the ancestor organisation Code
            foreach (OrganisationHierarchy NewAncOh in NewAncestors)
            {
                Guid AncestorCode = NewAncOh.AncestorOrganisationCode;
                var AncestorOrg = organisationRepositoryForRead.Single(x => x.Code == AncestorCode, "OrganisationType");
                var HopsFromAncestorToOrganisation = newOrgType.LevelNumber - AncestorOrg.OrganisationType.LevelNumber;

                if (ExistingChildHierarchies != null)
                {
                    //Loop through the existing children for the child organisation codes
                    foreach (OrganisationHierarchy ChildOh in ExistingChildHierarchies)
                    {
                        Guid ChildCode = ChildOh.OrganisationCode;
                        var HopsFromOrganisationToChild = organisationRepositoryForRead.Single(x => x.Code == ChildCode, "OrganisationType").OrganisationType.LevelNumber - existingOrgType.LevelNumber;
                        var NumberOfHopsBetweenChildAndAncestor = HopsFromOrganisationToChild + HopsFromAncestorToOrganisation;
                        OrganisationHierarchy OhToAdd = new OrganisationHierarchy()
                        {
                            AncestorOrganisationCode = AncestorCode,
                            Code = Guid.NewGuid(),
                            HopsBetweenOrgAndAncestor = NumberOfHopsBetweenChildAndAncestor,
                            ImmediateParent = (NumberOfHopsBetweenChildAndAncestor == 1),
                            IsActive = true,
                            OrganisationCode = ChildCode
                        };
                        organisationHierarchyRepository.Add(OhToAdd);
                    }
                }
                //Don't Forget the current org links to new parent and ancestors
                Guid CurrentOrgCode = destination.Code;
                OrganisationHierarchy OhForCurrent = new OrganisationHierarchy()
                {
                    AncestorOrganisationCode = AncestorCode,
                    Code = Guid.NewGuid(),
                    HopsBetweenOrgAndAncestor = HopsFromAncestorToOrganisation,
                    ImmediateParent = (HopsFromAncestorToOrganisation == 1),
                    IsActive = true,
                    OrganisationCode = CurrentOrgCode
                };
                organisationHierarchyRepository.Add(OhForCurrent);


            }
            if (ExistingChildHierarchies != null)
            {
                foreach (OrganisationHierarchy ChildOh in ExistingChildHierarchies)
                {
                    Guid ChildCode = ChildOh.OrganisationCode;
                    var HopsFromOrganisationToChild = organisationRepositoryForRead.Single(x => x.Code == ChildCode, "OrganisationType").OrganisationType.LevelNumber - existingOrgType.LevelNumber;

                    //NEED TO UPDATE THE ORGANISATION TYPES ON ALL CHILDREN IF THE ORGANISATION TYPE HAS CHANGED
                    if (destination.OrganisationTypeCode != organisationRepositoryForRead.Single(x => x.Code == destination.Code, "OrganisationType").OrganisationTypeCode)
                    {
                        var OrgCode = ChildOh.OrganisationCode;
                        Organisation orgToUpdate = organisationRepository.Single(x => x.Code == OrgCode);
                        orgToUpdate.OrganisationTypeCode = organisationTypeList.Single(x => x.LevelNumber == (HopsFromOrganisationToChild + newOrgType.LevelNumber)).Code;
                    }
                }
            }
        }

        
        /// <summary>
        /// Removes all links between the org,its children with it's ancestors.
        /// </summary>
        /// <param name="organisationHierarchyRepository"></param>
        /// <param name="destination"></param>
        /// <param name="CurrentParent"></param>
        /// <param name="ExistingAncestorHierarchies"></param>
        /// <param name="ExistingChildHierarchies"></param>

        private static void UnlinkExistingOrganisations(IRepository<OrganisationHierarchy> organisationHierarchyRepository, Organisation destination, Organisation CurrentParent, IEnumerable<OrganisationHierarchy> ExistingAncestorHierarchies, IEnumerable<OrganisationHierarchy> ExistingChildHierarchies)
        {
            //Loop through the ancestors for the ancestor org code
            foreach (OrganisationHierarchy AncOh in ExistingAncestorHierarchies)
            {
                Guid AncestorCode = AncOh.AncestorOrganisationCode;
                //Loop through the children for the child organisation codes
                if (ExistingChildHierarchies != null)
                {
                    foreach (OrganisationHierarchy ChiOh in ExistingChildHierarchies)
                    {
                        //Delete any links between any ancestor and any child of the organisation
                        Guid ChildCode = ChiOh.OrganisationCode;
                        var OhToDelete = organisationHierarchyRepository.Find(x => x.AncestorOrganisationCode == AncestorCode && x.OrganisationCode == ChildCode).SingleOrDefault();
                        if (OhToDelete != null)
                        {
                            organisationHierarchyRepository.Delete(OhToDelete);
                        }
                    }
                }
            }
            //Loop through and delete the organisations links with its ancestors 
            foreach (OrganisationHierarchy AncOh in ExistingAncestorHierarchies)
            {
                organisationHierarchyRepository.Delete(AncOh);
            }

            //Delete the link between organisation and its direct parent
            if (CurrentParent != null)
            {
                var OhMainLink = organisationHierarchyRepository.Find(x => x.AncestorOrganisationCode == CurrentParent.Code && x.OrganisationCode == destination.Code).SingleOrDefault();
                if (OhMainLink != null)
                {
                    organisationHierarchyRepository.Delete(OhMainLink);
                }
            }
        }
        #endregion
        #endregion

        #region Create

        /// <summary>
        /// Create a Organisation
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        public ApplicationOrganisationAdminVMDC CreateOrganisationForApplication(string currentUser, string user, string appID, string overrideID, OrganisationDC dc, Guid parentOrganisationCode, Guid applicationCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Organisation> organisationRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Organisation> organisationRepositoryForRead = new Repository<Organisation>(currentUser, user, appID, overrideID); //<----USES DIFFERENT CONTEXT
            IRepository<OrganisationHierarchy> organisationHierarchyRepository = new Repository<OrganisationHierarchy>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<OrganisationType> organisationTypeRepository = new Repository<OrganisationType>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Call overload with injected objects
            return CreateOrganisationForApplication(currentUser, user, appID, overrideID, dc,parentOrganisationCode,applicationCode, organisationRepository, organisationRepositoryForRead, organisationHierarchyRepository, organisationTypeRepository, uow);
        }

        /// <summary>
        ///  Create a Organisation
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public ApplicationOrganisationAdminVMDC CreateOrganisationForApplication(string currentUser, string user, string appID, string overrideID, OrganisationDC dc, Guid parentOrganisationCode, Guid applicationCode, IRepository<Organisation> organisationRepository,
            IRepository<Organisation> organisationRepositoryForRead, IRepository<OrganisationHierarchy> organisationHierarchyRepository, IRepository<OrganisationType> organisationTypeRepository, IUnitOfWork uow)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == dc) throw new ArgumentOutOfRangeException("dc");
                if (null == organisationRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                bool OrganisationAlreadyExists = false;

                using (uow)
                {
                    // Create a new ID for the Organisation item
                    dc.Code = Guid.NewGuid();

                    // Map data contract to model
                    Organisation destination = Mapper.Map<OrganisationDC, Organisation>(dc);

                    // Add the new item
                    organisationRepository.Add(destination);

                    //Find Current Parent Organisation
                    Organisation existingOrganisation = organisationRepository.Find(x => x.OrganisationHierarchy1.Any(y => y.AncestorOrganisationCode == parentOrganisationCode && y.HopsBetweenOrgAndAncestor == 1 && y.Organisation.Name.Equals(destination.Name))).SingleOrDefault();

                    if (existingOrganisation == null)
                    {

                        //Get All Types for application
                        IEnumerable<OrganisationType> organisationTypeList = organisationTypeRepository.Find
                            (new Specification<OrganisationType>(x => x.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(y => y.ApplicationCode == applicationCode)), x => new { x.LevelNumber });

                        var NewOrgType = organisationTypeRepository.Single(x => x.Code == destination.OrganisationTypeCode);


                        //Adds new links between the org and the new ancestors
                        LinkOrganisationStructureToNewAncestors(parentOrganisationCode, organisationRepository, organisationHierarchyRepository, organisationRepositoryForRead, destination, organisationTypeList, null, NewOrgType, null);

                        //Add Selflink Hierarchy
                        OrganisationHierarchy SelfLinkOH = new OrganisationHierarchy()
                        {
                            AncestorOrganisationCode = destination.Code,
                            Code = Guid.NewGuid(),
                            HopsBetweenOrgAndAncestor = 0,
                            ImmediateParent = false,
                            IsActive = true,
                            OrganisationCode = destination.Code
                        };
                        organisationHierarchyRepository.Add(SelfLinkOH);

                        // Commit unit of work
                        uow.Commit();

                        dc = Mapper.Map<Organisation, OrganisationDC>(destination);
                    }
                    else
                    {
                        OrganisationAlreadyExists = true;
                    }

                }

                // Create new data contract to return - BIG CHEAT JUST CALL THE GET WITH PARENT SERVICE METHOD AND ALLOW IT TO USE NEW CONTEXT - DON@T CARE ABOUT PERFORMANCE ON RELATIVELY LIGHTLY USED FUNCTION
                ApplicationOrganisationAdminVMDC returnObject = new ApplicationOrganisationAdminVMDC();

                if (OrganisationAlreadyExists)
                {
                    returnObject.Message = "Organisation already exists";
                }
                else
                {
                    returnObject = GetOrganisationWithParent(currentUser, user, appID, overrideID, dc.Code, applicationCode);
                }
                return returnObject;

     
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete a Organisation
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="lockID"></param>
        public void DeleteOrganisationForApplication(string currentUser, string user, string appID, string overrideID, Guid code, string lockID)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Organisation> organisationRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<OrganisationHierarchy> organisationHierarchyRepository = new Repository<OrganisationHierarchy>(uow.ObjectContext, currentUser, user, appID, overrideID);
            // Call overload with injected objects
            DeleteOrganisationForApplication(currentUser, user, appID, overrideID, code, lockID, organisationRepository, organisationHierarchyRepository, uow);
        }

        /// <summary>
        /// Update a Organisation
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="lockID"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public void DeleteOrganisationForApplication(string currentUser, string user, string appID, string overrideID, Guid code, string lockID, IRepository<Organisation> organisationRepository, IRepository<OrganisationHierarchy> organisationHierarchyRepository, IUnitOfWork uow)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(lockID)) throw new ArgumentOutOfRangeException("lockID");
                if (null == organisationRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {
                    
                    //Find Current Parent Organisation
                    Organisation CurrentParent = organisationRepository.Single(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == code && y.HopsBetweenOrgAndAncestor == 1));

                    // Find item based on ID
                    Organisation dataEntity = organisationRepository.Single(x => x.Code == code);

                    // Delete the item
                    organisationRepository.Delete(dataEntity);

                    //Get the ancestor hierarchies of the organisation this will contain the Codes of all organisations above in the path
                    var ExistingAncestorHierarchies = organisationHierarchyRepository.Find(x => x.OrganisationCode == code && x.AncestorOrganisationCode != code); //Find all oh where organisationcode is current org

                    //Removes all links between the org,its children with it's ancestors.
                    UnlinkExistingOrganisations(organisationHierarchyRepository, dataEntity, CurrentParent, ExistingAncestorHierarchies, null);

                    //Get the ancestor hierarchies of the organisation this will contain the Codes of all organisations above in the path
                    var SelfLinkHierarchy = organisationHierarchyRepository.Find(x => x.OrganisationCode == code && x.AncestorOrganisationCode == code).SingleOrDefault(); //Find all oh where organisationcode is current org

                    if (SelfLinkHierarchy != null)
                    {
                        organisationHierarchyRepository.Delete(SelfLinkHierarchy);
                    }

                    // Commit unit of work
                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);
            }
        }

        #endregion

        #region Update Staff Application Administration And retreive new version

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentUserName"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public StaffApplicationAdminVMDC UpdateStaffApplicationAdministration(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode, StaffDC staffItem, StaffAdminChangeSetDC staffChangeSet)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(userName);

            // Create repository
            // Create repositories for lookup data
            IRepository<Application> applicationRepository = new Repository<Application>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<StaffAttributes> staffAttributesRepository = new Repository<StaffAttributes>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<StaffOrganisation> staffOrganisationsRepository = new Repository<StaffOrganisation>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            IRepository<Organisation> organisationsRepository = new Repository<Organisation>(uow.ObjectContext, userName, currentUserName, appID, overrideID);
            // Get ApplicationAttribute
            return UpdateStaffApplicationAdministration(userName, currentUserName, appID, overrideID, applicationCode, staffItem, staffChangeSet, uow, applicationRepository
            , staffRepository, staffAttributesRepository, staffOrganisationsRepository, organisationsRepository
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="currentUserName"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public StaffApplicationAdminVMDC UpdateStaffApplicationAdministration(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode, StaffDC staffItem, StaffAdminChangeSetDC staffChangeSet, IUnitOfWork uow
            , IRepository<Application> applicationRepository, IRepository<Staff> staffRepository, IRepository<StaffAttributes> staffAttributesRepository, IRepository<StaffOrganisation> staffOrganisationsRepository, IRepository<Organisation> organisationsRepository
            )
        {
            try
            { 
                using (uow)
                {
                    //Delete the deleted staff organisations
                    foreach (StaffOrganisationDC deletedItem in staffChangeSet.DeletedStaffOrganisations)
                    {
                        staffOrganisationsRepository.Delete(Mapper.Map<StaffOrganisation>(deletedItem));
                    }

                    //Update the updated staff organisations
                    foreach (StaffOrganisationDC updatedItem in staffChangeSet.UpdatedStaffOrganisations)
                    {
                        staffOrganisationsRepository.Update(Mapper.Map<StaffOrganisation>(updatedItem));
                    }

                    //Inserte the inserted staff organisations
                    foreach (StaffOrganisationDC insertedItem in staffChangeSet.InsertedStaffOrganisations)
                    {
                        staffOrganisationsRepository.Add(Mapper.Map<StaffOrganisation>(insertedItem));
                    }

                    //Delete the deleted staff attributes
                    foreach (StaffAttributesDC deletedItem in staffChangeSet.DeletedStaffAttributes)
                    {
                        staffAttributesRepository.Delete(Mapper.Map<StaffAttributes>(deletedItem));
                    }
                    
                    //Updated the updated staff attributes
                    foreach (StaffAttributesDC updatedItem in staffChangeSet.UpdatedStaffAttributes)
                    {
                        staffAttributesRepository.Update(Mapper.Map<StaffAttributes>(updatedItem));
                    }

                    //Insert the inserted staff attributes
                    foreach (StaffAttributesDC insertedItem in staffChangeSet.InsertedStaffAttributes)
                    {
                        var itemToInsert = Mapper.Map<StaffAttributes>(insertedItem);
                        itemToInsert.SecurityLabel = Guid.Empty;
                        staffAttributesRepository.Add(Mapper.Map<StaffAttributes>(insertedItem));
                    }

                    staffRepository.Update(Mapper.Map<Staff>(staffItem));

                    //Commit the changes
                    uow.Commit();

                    //retreive the latest staff application admin info
                    return GetStaffApplicationAdministrationByStaffCodeAndApplicationCode(userName, currentUserName, appID, overrideID, applicationCode, staffItem.Code);
                    
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }
        #endregion

        #region UpdateStaffCurrentOrganisation

        /// <summary>
        /// Changes the organisation which is deemed as being the current user's organisation for the specified application
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="applicationName"></param>
        /// <param name="organisationID"></param>
        public void UpdateStaffCurrentOrganisation(string currentUser, string user, string appID, string overrideID, string applicationName, int organisationID)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create instance of Staff Organisation repository
            IRepository<StaffOrganisation> staffOrgRepository = new Repository<StaffOrganisation>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Call overload with injected objects
            UpdateStaffCurrentOrganisation(currentUser, user, appID, overrideID, applicationName, organisationID, staffOrgRepository, uow);
        }

        /// <summary>
        /// Changes the organisation which is deemed as being the current user's organisation for the specified application
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="applicationName"></param>
        /// <param name="organisationID"></param>
        /// <param name="staffOrgRepository"></param>
        /// <param name="uow"></param>
        public void UpdateStaffCurrentOrganisation(string currentUser, string user, string appID, string overrideID, string applicationName, int organisationID,
            IRepository<StaffOrganisation> staffOrgRepository, IUnitOfWork uow)
        {
            try
            {
                // Convert to guid
                Guid userGuid = Guid.Parse(currentUser);

                // Specification to retrive Staff Org record for specificied user for the specified application
                ISpecification<StaffOrganisation> specificationCurrent =
                    new Specification<StaffOrganisation>(x => x.Application.ApplicationName == applicationName && x.StaffCode == userGuid && x.IsCurrent == true);

                // Retrieve the Staff Org record as specified above
                StaffOrganisation staffOrgCurrent = staffOrgRepository.Single(specificationCurrent);

                // This will no longer be the current organisation for this user for the specified application
                staffOrgCurrent.IsCurrent = false;

                // Update the Staff Org record
                //staffOrgRepository.Update(staffOrgCurrent);

                // Specification to retrive Staff Org record for specificied user, Organisation and Application
                ISpecification<StaffOrganisation> specificationCurrentNew =
                    new Specification<StaffOrganisation>(x => x.Application.ApplicationName == applicationName && x.StaffCode == userGuid && x.Organisation.ID == organisationID);

                // Retrieve the Staff Org record as specified above
                StaffOrganisation staffOrgCurrentNew = staffOrgRepository.Single(specificationCurrentNew);

                // This organisation is now the current one for this user for the specified application
                staffOrgCurrentNew.IsCurrent = true;

                // Update the Staff Org record
                //staffOrgRepository.Update(staffOrgCurrentNew);

                // Commit the changes
                uow.Commit();
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);
            }
        }

        #endregion

        #region EvaluateStaffSearchCriteria

        partial void EvaluateStaffSearchCriteria(StaffSearchCriteriaDC searchCriteria, ref ISpecification<Staff> specification)
        {
            //Initialise specifications
            ISpecification<Staff> staffFirstnameSpecification = new Specification<Staff>(x => x.FirstName.StartsWith(searchCriteria.FirstName));
            ISpecification<Staff> staffLastnameSpecification = new Specification<Staff>(x => x.LastName.StartsWith(searchCriteria.LastName));
            ISpecification<Staff> staffNumberSpecification = new Specification<Staff>(x => x.StaffNumber.StartsWith(searchCriteria.StaffNumber));
            ISpecification<Staff> staffSpecification = null; new Specification<Staff>();

            #region Build specification based on the search criteria

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                staffSpecification = staffFirstnameSpecification;
            }

            if (!string.IsNullOrEmpty(searchCriteria.LastName))
            {
                staffSpecification = staffSpecification == null ? staffLastnameSpecification : staffSpecification.And(staffLastnameSpecification);
            }

            if (!string.IsNullOrEmpty(searchCriteria.StaffNumber))
            {
                staffSpecification = staffSpecification == null ? staffNumberSpecification : staffSpecification.And(staffNumberSpecification);
            }

            if (null == staffSpecification) staffSpecification = new Specification<Staff>();

            specification = staffSpecification;

            #endregion

        }

        #endregion

        #region EvaluateStaffDetailsSearchCriteria
        partial void EvaluateSearchCriteria(StaffDetailsSearchCriteriaDC searchCriteria, ref ISpecification<StaffDetails> specification)
        {
            //Initialise specifications
            ISpecification<StaffDetails> staffCodeSpecification = new Specification<StaffDetails>(x => x.StaffCode == searchCriteria.StaffCode);

            ISpecification<StaffDetails> staffDetailsSpecification = null;

            #region Build specification based on the search criteria

            if (searchCriteria.StaffCode != Guid.Empty)
            {
                staffDetailsSpecification = staffCodeSpecification;
            }
            
            if (staffDetailsSpecification == null)
            {
                staffDetailsSpecification = new Specification<StaffDetails>();
            }

            specification = staffDetailsSpecification;

            #endregion

        }
        #endregion
    }
}
