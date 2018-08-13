using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using Dwp.Adep.Framework.Management.Web.ViewModels;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.Helpers;
using Dwp.Adep.Framework.Management.Web.AdminService;
using Dwp.Adep.Framework.Management.Web.AuthorisationService;

namespace Dwp.Adep.Framework.Management.Web.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        #region ApplicationStaffAdmin

        //
        // GET: /Admin/ApplicationStaffAdmin/5
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        public ActionResult ApplicationStaffAdmin(Guid? staffCode)
        {
            var model = new StaffApplicationAdminVM();

            //Initialize lists
            model.OrganisationsByTypesList = new List<OrganisationByTypeVM>();
            model.StaffApplicationAttributeList = new List<StaffApplicationAttributeVM>();
            model.StaffOrganisationList = new List<StaffOrganisationModel>();
            model.ApplicationList = new List<ApplicationModel>();
            model.RoleList = new List<string>();


            //Retreive staff item and application list
            model.StaffItem = SessionManager.CurrentStaff;

            //Retreive all applications that are available to the current user.
            try
            {
                string[] userRoles = SessionManager.UserRoles;              

                var sc = new AdminServiceClient();
                var ApplicationList = sc.GetApplicationsWithStaffAdmin(CurrentUser, CurrentUser, "Framework", "", userRoles,true);
                model.ApplicationList = Mapper.Map<IEnumerable<ApplicationDC>, IEnumerable<ApplicationModel>>(ApplicationList).ToList();

                /* The following has been added to allow BCAS ADMINS to manage staff - limit the applications that be can be used - to be removed when generic solution is found
                    if (User.IsInRole(FrameworkRoles.BCASADMIN) && !User.IsInRole(FrameworkRoles.ADMIN) && !User.IsInRole(FrameworkRoles.STAFFMAINTENANCE))
                    {
                        model.ApplicationList = model.ApplicationList.Where(x => x.ApplicationName == "BCAS").ToList();
                    }
                 */

            }
            catch (Exception ex)
            {
                model.Message = FixedResources.MESSAGE_RETRIEVAL_FAILED;
                return View(model);
            }

            try
            {
                var sc1 = new AdminServiceClient();
                var StaffItem = sc1.GetStaff(User.Identity.Name, User.Identity.Name, "Framework", "", SessionManager.StaffCode);
                model.StaffItem = Mapper.Map<StaffModel>(StaffItem.StaffItem);
                SessionManager.CurrentStaff = model.StaffItem;
            }
            catch (Exception ex)
            {
                model.Message = FixedResources.MESSAGE_RETRIEVAL_FAILED;
                return View(model);
            }

            //Initialize lists
            model.OrganisationsByTypesList = new List<OrganisationByTypeVM>();
            model.StaffApplicationAttributeList = new List<StaffApplicationAttributeVM>();
            model.StaffOrganisationList = new List<StaffOrganisationModel>();

            SessionManager.CurrentStaffForAdmin = model.StaffItem;
            SessionManager.ApplicationList = model.ApplicationList;
            SessionManager.OrganisationByTypeList = model.OrganisationsByTypesList;
            SessionManager.StaffApplicationAttributeList = model.StaffApplicationAttributeList;
            SessionManager.StaffOrganisationList = model.StaffOrganisationList;
            SessionManager.StaffRoleList = model.RoleList;


            return View(model);
        }

        #region Application Staff Admin post backs now split by button pressed

        #region Update Application
        // POST: /Admin/ApplicationStaffAdmin with UpdateApplication button submitting
        [HttpPost]
        [HttpParamAdmin(Prefix = "ApplicationStaffAdmin::")]
        public ActionResult UpdateApplication(Guid? staffCode, FormCollection form)
        {
            // Get the updated model
            var model = GetUpdatedModel();

            //Set flags false
            SetFlagsFalse(model);

            // Create instance of Authorisation service
            var authSc = new AuthorisationServiceClient();

            try
            {
                List<string> roleList = authSc.GetUserRoles(CurrentUser, CurrentUser, appID, "", model.StaffItem.Code.ToString(), model.SelectedApplicationCode).ToList();
                model.RoleList = roleList;
            }
            catch (Exception e)
            {
                // Handle exception
                if (e.Message == "Failed to find User ID in Active Directory")
                {
                    model.Message = e.Message;
                   // ModelState.AddModelError("SelectedApplicationCode", string.Format("Failed to find User ID in Active Directory"));
                }
                ExceptionManager.HandleException(e, authSc);
                //return null;
            }

            if (model.SelectedApplicationCode.HasValue)
            {
                // Create instance of Admin service
                AdminServiceClient sc = new AdminServiceClient();

                try
                {
                    StaffApplicationAdminVMDC returnedObject = sc.GetStaffApplicationAdministrationByStaffCodeAndApplicationCode(User.Identity.Name, User.Identity.Name, "Framework", "", model.SelectedApplicationCode.Value, model.StaffItem.Code);

                    //Merge the returned DC to the model
                    MergeReturnedObjectToModel(model, returnedObject);

                }
                catch (Exception e)
                {
                    // Handle exception
                    ExceptionManager.HandleException(e, sc);

                    return null;
                }
            }
            else
            {
                //remove the current values from session
                SessionManager.OrganisationByTypeList = null;
                SessionManager.StaffApplicationAttributeList = null;
                SessionManager.StaffOrganisationList = null;
                //remove the current values from session
                model.OrganisationsByTypesList = null;
                model.StaffApplicationAttributeList = null;
                model.StaffOrganisationList = null;
                return RedirectToAction("ApplicationStaffAdmin", "Admin");
                // /Admin/ApplicationStaffAdmin
            }

            DetermineIsDirty(model);
            ModelState.Clear();
            return View(model);
        }

        #endregion

        #region AddButton
        // POST: /Admin/ApplicationStaffAdmin with Add button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        [HttpPost]
        [HttpParamAdmin(Prefix = "ApplicationStaffAdmin::")]
        public ActionResult AddOrganisation(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);

            //Establish which organisation is to be added to the staff list
            //find lowest level with an organistion selected
            OrganisationModel selectedOrganisation = null;
            List<string> path = new List<string>();
            foreach (var item in model.OrganisationsByTypesList)
            {
                if (!String.IsNullOrEmpty(item.SelectedOrganisationCode))
                {
                    selectedOrganisation = item.OrganisationList.Single(x => x.Code == Guid.Parse(item.SelectedOrganisationCode));
                    path.Add(selectedOrganisation.Name);
                }
            }
            if (selectedOrganisation != null)
            {
                if (!model.StaffOrganisationList.Any(x => x.OrganisationCode == selectedOrganisation.Code && x.StaffCode == model.StaffItem.Code))
                {
                    model.StaffOrganisationList.Add(new StaffOrganisationModel()
                    {
                        ApplicationCode = model.SelectedApplicationCode.Value,
                        Code = Guid.NewGuid(),
                        IsCurrent = false,
                        IsDefault = false,
                        OrganisationCode = selectedOrganisation.Code,
                        OrganisationPath = path,
                        StaffCode = model.StaffItem.Code
                    });
                }
            }

            DetermineIsDirty(model);

            return View(model);
        }

        #endregion


        #region SaveStaffAdmin
        // POST: /Admin/ApplicationStaffAdmin with SaveStaffAdmin button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        [HttpPost]
        [HttpParamAdmin(Prefix = "ApplicationStaffAdmin::")]
        public ActionResult SaveStaffAdmin(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            //boolean variable that is set to false by default. It is used to check if an application requires a default application.
            bool isDefaultOrgRequiredApplication = false;
            var defaultradio = Request.Form["defaultradio"];
            foreach (var item in model.StaffApplicationAttributeList)
            {
                //ApplicationAttrbiute contains rows to indicate applications that require a default organisation set for a application.
                if (item.ApplicationAttributeItem.AttributeName == "IsDefaultOrgRequired")
                {
                    isDefaultOrgRequiredApplication = true;
                }
            }

            //Below code added for checking to see if user has chosen a default appllication based on whether it is required.
            if (isDefaultOrgRequiredApplication == true)
            {
                if (defaultradio == null)
                {
                    if (model.StaffOrganisationList.Count() > 1)
                    {
                        //if application organisation list contains more than one value, show user error message and do not save.
                        model.Message = "This application requires a default staff organisation, please select a default.";
                        return View(model);
                    }
                    else if(model.StaffOrganisationList.Count() == 1)
                    {
                        //if only one application organisation exists, then set that as a default organistion.
                        model.StaffOrganisationList[0].IsDefault = true;
                    }
                }
            }

            //Save the changes by calling the service

            var changeSet = EstablishAdminChangeSet(model);

            StaffDC staffItem = Mapper.Map<StaffDC>(model.StaffItem);

            var sc = new AdminServiceClient();

            //IF SelectedApplicationCode is not null
            if (model.SelectedApplicationCode != null)
            {
                
                //Save the changeset
                try
                {
                    var returnedObject = sc.UpdateStaffApplicationAdministration(User.Identity.Name, User.Identity.Name, "Framework", "", model.SelectedApplicationCode.Value, staffItem, changeSet);
                    MergeReturnedObjectToModel(model, returnedObject);
                    model.Message = FixedResources.MESSAGE_UPDATE_SUCCEEDED;
                }
                catch (Exception e)
                {
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;
                    //model.Message = FixedResources.MESSAGE_UPDATE_FAILED;
                    return View(model);
                }

            }
            else //model.SelectedApplicationCode IS null
            {
                model.Message = FixedResources.MESSAGE_UPDATE_NOCHANGE;
            }

            DetermineIsDirty(model);

            return View(model);
        }

        private static StaffAdminChangeSetDC EstablishAdminChangeSet(StaffApplicationAdminVM model)
        {
            var changeSet = new StaffAdminChangeSetDC()
            {
                DeletedStaffAttributes = new StaffAttributesDC[0],
                DeletedStaffOrganisations = new StaffOrganisationDC[0],
                InsertedStaffAttributes = new StaffAttributesDC[0],
                InsertedStaffOrganisations = new StaffOrganisationDC[0],
                UpdatedStaffAttributes = new StaffAttributesDC[0],
                UpdatedStaffOrganisations = new StaffOrganisationDC[0],
            };

            //Build a list of deleted organisations
            var StaffOrgsDB = SessionManager.StaffOrganisationListDBVersion;

            if (StaffOrgsDB != null && model.StaffOrganisationList != null)
            {
                var deletedStaffOrgs = new List<StaffOrganisationModel>();
                foreach (var item in StaffOrgsDB)
                {
                    if (!model.StaffOrganisationList.Any(x => x.Code == item.Code))
                    {
                        deletedStaffOrgs.Add(item);
                    }
                }
                //Build a list of changed organisations a new default org may have been set.
                var changedStaffOrgs = new List<StaffOrganisationModel>();
                foreach (var item in model.StaffOrganisationList)
                {
                    if (StaffOrgsDB.Any(x => x.Code == item.Code && x.IsDefault != item.IsDefault))
                    {
                        changedStaffOrgs.Add(item);
                    }
                }
                //Build list of added organisations
                var addedStaffOrgs = new List<StaffOrganisationModel>();
                foreach (var item in model.StaffOrganisationList)
                {
                    if (!StaffOrgsDB.Any(x => x.Code == item.Code))
                    {
                        addedStaffOrgs.Add(item);
                    }
                }
                changeSet.DeletedStaffOrganisations = Mapper.Map<IEnumerable<StaffOrganisationModel>, IEnumerable<StaffOrganisationDC>>(deletedStaffOrgs).ToArray();
                changeSet.UpdatedStaffOrganisations = Mapper.Map<IEnumerable<StaffOrganisationModel>, IEnumerable<StaffOrganisationDC>>(changedStaffOrgs).ToArray();
                changeSet.InsertedStaffOrganisations = Mapper.Map<IEnumerable<StaffOrganisationModel>, IEnumerable<StaffOrganisationDC>>(addedStaffOrgs).ToArray();
            }

            //Build a list of deleted staff attributes
            var StaffAttributesDB = SessionManager.StaffApplicationAttributeListDBVersion;

            if (StaffAttributesDB != null && model.StaffApplicationAttributeList != null)
            {
                var deletedStaffAttrributes = new List<StaffAttributesModel>();
                foreach (var item in StaffAttributesDB)
                {
                    if (item.StaffAttributeItem != null)
                    {
                        if (!model.StaffApplicationAttributeList.Any(x => x.StaffAttributeItem != null && x.StaffAttributeItem.Code == item.StaffAttributeItem.Code))
                        {
                            deletedStaffAttrributes.Add(item.StaffAttributeItem);
                        }
                    }
                }
                //Build a list of changed staff attributes
                var changedStaffAttrributes = new List<StaffAttributesModel>();
                foreach (var item in model.StaffApplicationAttributeList)
                {
                    if (item.StaffAttributeItem != null)
                    {
                        if (StaffAttributesDB.Any(x => x.StaffAttributeItem != null && x.StaffAttributeItem.Code == item.StaffAttributeItem.Code && x.StaffAttributeItem.LookupValue != item.StaffAttributeItem.LookupValue))
                        {
                            changedStaffAttrributes.Add(item.StaffAttributeItem);
                        }
                    }
                }
                //Build a list of inserted staff attributes
                var addedStaffAttrributes = new List<StaffAttributesModel>();
                foreach (var item in model.StaffApplicationAttributeList)
                {
                    //var removeLater = item.
                    if (item.StaffAttributeItem != null)
                    {
                        if (!StaffAttributesDB.Any(x => x.StaffAttributeItem != null && x.StaffAttributeItem.Code == item.StaffAttributeItem.Code))
                        {
                            addedStaffAttrributes.Add(item.StaffAttributeItem);                                                     
                        }
                    }
                }
                changeSet.DeletedStaffAttributes = Mapper.Map<IEnumerable<StaffAttributesModel>, IEnumerable<StaffAttributesDC>>(deletedStaffAttrributes).ToArray();
                changeSet.UpdatedStaffAttributes = Mapper.Map<IEnumerable<StaffAttributesModel>, IEnumerable<StaffAttributesDC>>(changedStaffAttrributes).ToArray();
                changeSet.InsertedStaffAttributes = Mapper.Map<IEnumerable<StaffAttributesModel>, IEnumerable<StaffAttributesDC>>(addedStaffAttrributes).ToArray();

            }



            return changeSet;
        }

        #endregion

        #region UpdateOrganisationDropDowns
        // POST: /Admin/ApplicationStaffAdmin with one of the UpdateOrganisationDropDowns buttons submitting
        [HttpPost]
        [HttpParamAdmin(Prefix = "ApplicationStaffAdmin::")]
        public ActionResult UpdateOrganisation(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);

            //Establish which drop downs need updating
            Guid OrganisationByTypeValue = Guid.Empty;
            Guid PrefixValue = Guid.Empty;
            foreach (string KeyItem in form)
            {
                if (KeyItem.StartsWith("ApplicationStaffAdmin::UpdateOrganisation"))
                {
                    PrefixValue = Guid.Parse(KeyItem.Substring(42));
                    var HiddenFieldKey = "OrganisationsByRows[" + PrefixValue.ToString() + "].OrganisationTypeItem.Code";
                    OrganisationByTypeValue = Guid.Parse(form[HiddenFieldKey]);
                    break;
                }
            }

            var AllOrganisationsForApplicationByTypesList = SessionManager.AllOrganisationsForApplicationByTypesList;
            var clickedItem = model.OrganisationsByTypesList.Single(x => x.OrganisationTypeItem.Code == OrganisationByTypeValue);
            if (clickedItem.SelectedOrganisationCode != null)
            {
                // Modified to handle multiple organisation types for the same level of the organisation hierarchy.
                var AllOrgsListsForNextLevel = AllOrganisationsForApplicationByTypesList.FindAll(x => (x.OrganisationTypeItem.LevelNumber == (clickedItem.OrganisationTypeItem.LevelNumber + 1)) && (x.OrganisationTypeItem.ParentOrganisationTypeCode == clickedItem.OrganisationTypeItem.Code));
                if (AllOrgsListsForNextLevel != null && AllOrgsListsForNextLevel.Count > 0)
                {
                    //determine which organisation list matches both the organisation type and the selected parent
                    OrganisationByTypeVM AllOrgsForNextLevel = null;
                    foreach (OrganisationByTypeVM item in AllOrgsListsForNextLevel)
                    {
                        if (item.OrganisationList != null && item.OrganisationList.Count > 0)
                        {
                            foreach (OrganisationModel organisationItem in item.OrganisationList)
                            {
                                if (organisationItem.ParentID == Guid.Parse(clickedItem.SelectedOrganisationCode))
                                {
                                    AllOrgsForNextLevel = item;
                                    break;
                                }
                            }
                            if (AllOrgsForNextLevel != null)
                            {
                                break;
                            }
                        }
                    }

                    if (AllOrgsForNextLevel != null)
                    {
                        //find correct org list model for the selected parent and the organisation type
                        var ChildVMList = model.OrganisationsByTypesList.FindAll(x => x.OrganisationTypeItem.ParentOrganisationTypeCode == clickedItem.OrganisationTypeItem.Code);

                        OrganisationByTypeVM ChildVM = null;
                        if (ChildVMList != null && ChildVMList.Count > 0)
                        {
                            foreach (OrganisationByTypeVM listItem in ChildVMList)
                            {
                                OrganisationByTypeVM ChildVMtemp = null;
                                if (listItem.OrganisationTypeItem.Code == AllOrgsForNextLevel.OrganisationTypeItem.Code)
                                {

                                    ChildVM = listItem;
                                    ChildVMtemp = listItem;
                                    ChildVM.OrganisationList = AllOrgsForNextLevel.OrganisationList.Where(x => x.ParentID == Guid.Parse(clickedItem.SelectedOrganisationCode)).ToList();
                                }
                                else
                                {
                                    ChildVMtemp = listItem;
                                }
                                //empty the lists below the child of the clicked item
                                var BelowChildVMs = model.OrganisationsByTypesList.Where(x => x.OrganisationTypeItem.LevelNumber >= ChildVMtemp.OrganisationTypeItem.LevelNumber);
                                foreach (var item in BelowChildVMs)
                                {
                                    if (item.OrganisationTypeItem.LevelNumber != ChildVMtemp.OrganisationTypeItem.LevelNumber)
                                    {
                                        item.OrganisationList = new List<OrganisationModel>();
                                    }
                                    else
                                    {
                                        // clear the other lists at the same level i.e. handle multiple organisationTypes with the same parent 
                                        if (item.OrganisationList.Count > 0 && Guid.Parse(clickedItem.SelectedOrganisationCode) != item.OrganisationList[0].ParentID)
                                        {
                                            item.OrganisationList = new List<OrganisationModel>();
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                //Empty the lists below the empty clicked item
                var BelowChildVMs = model.OrganisationsByTypesList.Where(x => x.OrganisationTypeItem.LevelNumber > clickedItem.OrganisationTypeItem.LevelNumber);
                foreach (var item in BelowChildVMs)
                {
                    item.OrganisationList = new List<OrganisationModel>();
                }
            }

            DetermineIsDirty(model);
            return View(model);
        }

        #endregion

        #region DeleteStaffOrganisation
        // POST: /Admin/ApplicationStaffAdmin with one of the UpdateOrganisationDropDowns buttons submitting
        [HttpPost]
        [HttpParamAdmin(Prefix = "ApplicationStaffAdmin::")]
        public ActionResult DeleteStaffOrganisation(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);

            //Establish which staff organisation to remove
            Guid StaffOrganisationValue = Guid.Empty;
            foreach (string KeyItem in form)
            {
                if (KeyItem.StartsWith("ApplicationStaffAdmin::DeleteStaffOrganisation"))
                {
                    StaffOrganisationValue = Guid.Parse(KeyItem.Substring(47));
                    break;
                }
            }

            var StaffOrganisationItem = model.StaffOrganisationList.Single(x => x.Code == StaffOrganisationValue);

            model.StaffOrganisationList.Remove(StaffOrganisationItem);

            DetermineIsDirty(model);
            return View(model);
        }

        #endregion

        #region Exit Staff Administration
        // POST: /Admin/ApplicationStaffAdmin with one of the UpdateOrganisationDropDowns buttons submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        [HttpPost]
        [HttpParamAdmin(Prefix = "ApplicationStaffAdmin::")]
        public ActionResult Exit(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || model.IsViewDirty == false)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";

                //remove the current values from session
                ClearSesssionObjects(model);

                return RedirectToAction("Search", "Staff");

            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = FixedResources.MESSAGE_EXITCONFIRMATION;
                model.IsExitConfirmed = "True";
            }

            return View(model);
        }

        #endregion

        #endregion

        #region private methods for edit check

        private static void ClearSesssionObjects(StaffApplicationAdminVM model)
        {
            //remove the current values from session
            SessionManager.OrganisationByTypeList = null;
            SessionManager.StaffApplicationAttributeList = null;
            SessionManager.StaffOrganisationList = null;
            SessionManager.ApplicationList = null;
            SessionManager.StaffRoleList = null;

        }
        private void DetermineIsDirty(StaffApplicationAdminVM model)
        {
            //Compare the attributes and organisations check to the original session db

            var changeSet = EstablishAdminChangeSet(model);

            if (changeSet.DeletedStaffAttributes.Count() +
                changeSet.DeletedStaffOrganisations.Count() +
                changeSet.InsertedStaffAttributes.Count() +
                changeSet.InsertedStaffOrganisations.Count() +
                changeSet.UpdatedStaffAttributes.Count() +
                changeSet.UpdatedStaffOrganisations.Count() == 0)
            {
                model.IsViewDirty = false;
            }
            else
            {
                model.IsViewDirty = true;
            }

        }

        private void SetFlagsFalse(StaffApplicationAdminVM model)
        {
            model.IsExitConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsExitConfirmed");

        }

        private static void MergeReturnedObjectToModel(StaffApplicationAdminVM model, StaffApplicationAdminVMDC returnedObject)
        {
            model.StaffItem = Mapper.Map<StaffModel>(returnedObject.StaffItem);
            SessionManager.CurrentStaffForAdmin = model.StaffItem;
            //Clear the organisations list and repopulate from the returned object
            model.OrganisationsByTypesList.Clear();
            List<OrganisationByTypeVM> AllOrganisationsForApplicationByTypesList = new List<OrganisationByTypeVM>();
            foreach (var item in returnedObject.OrganisationsByTypesList)
            {
                AllOrganisationsForApplicationByTypesList.Add(new OrganisationByTypeVM()
                {
                    OrganisationList = Mapper.Map<IEnumerable<OrganisationDC>, IEnumerable<OrganisationModel>>(item.OrganisationList).ToList(),
                    OrganisationTypeItem = Mapper.Map<OrganisationTypeModel>(item.OrganisationTypeItem)
                });
            }

            SessionManager.AllOrganisationsForApplicationByTypesList = AllOrganisationsForApplicationByTypesList;

            //initialise the available orgs, the first drop down should have THE ROOT ORGANISATION selected
            if (AllOrganisationsForApplicationByTypesList.Count > 1)
            {
                int i = 0;
                foreach (var item in AllOrganisationsForApplicationByTypesList)
                {
                    if (i == 0)
                    {
                        model.OrganisationsByTypesList.Add(new OrganisationByTypeVM()
                        {
                            OrganisationList = item.OrganisationList,
                            OrganisationTypeItem = item.OrganisationTypeItem,
                            SelectedOrganisationCode = item.OrganisationList[0].Code.ToString()
                        });
                    }
                    else
                    {
                        model.OrganisationsByTypesList.Add(new OrganisationByTypeVM()
                        {
                            OrganisationList = !string.IsNullOrEmpty(model.OrganisationsByTypesList[i - 1].SelectedOrganisationCode) ? item.OrganisationList.Where(x => x.ParentID == Guid.Parse(model.OrganisationsByTypesList[i - 1].SelectedOrganisationCode)).ToList() : new List<OrganisationModel>(),
                            OrganisationTypeItem = item.OrganisationTypeItem,
                            SelectedOrganisationCode = ""
                        });
                    }
                    i++;

                }

            }

            //RoleList comes from the other service so don't merge it!!!!!!!!!!!!!!!!!!
            //model.RoleList = returnedObject.RoleList.ToList();

            model.RootNodeOrganisation = Mapper.Map<OrganisationModel>(returnedObject.RootNodeOrganisation);

            model.SelectedApplicationCode = returnedObject.SelectedApplicationCode;

            //Clear the staff application attribute list and repopulate from the returned object
            model.StaffApplicationAttributeList.Clear();
            foreach (var item in returnedObject.StaffApplicationAttributeList)
            {
                model.StaffApplicationAttributeList.Add(new StaffApplicationAttributeVM()
                {
                    ApplicationAttributeItem = Mapper.Map<ApplicationAttributeModel>(item.ApplicationAttributeItem),
                    StaffAttributeItem = Mapper.Map<StaffAttributesModel>(item.StaffAttributeItem)
                });
            }


            model.StaffOrganisationList = Mapper.Map<IEnumerable<StaffOrganisationDC>, IEnumerable<StaffOrganisationModel>>(returnedObject.StaffOrganisationList).ToList();

            //Add the values to session
            SessionManager.OrganisationByTypeList = model.OrganisationsByTypesList;

            List<StaffApplicationAttributeVM> SortedList = model.StaffApplicationAttributeList.OrderBy(x => x.ApplicationAttributeItem.AttributeName).ToList();
            model.StaffApplicationAttributeList = SortedList;
            SessionManager.StaffApplicationAttributeList = model.StaffApplicationAttributeList;

            //Need to clone the staff attribute lists
            var AttributeDBList = new List<StaffApplicationAttributeVM>();
            foreach (StaffApplicationAttributeVM item in model.StaffApplicationAttributeList)
            {
                AttributeDBList.Add(new StaffApplicationAttributeVM()
                {
                    StaffAttributeItem = item.StaffAttributeItem.Clone(),
                    ApplicationAttributeItem = item.ApplicationAttributeItem
                });
            }
            SessionManager.StaffApplicationAttributeListDBVersion = AttributeDBList;


            SessionManager.StaffOrganisationList = model.StaffOrganisationList;

            //Need to clone all the items in the list and store them away as the current db version of the list
            var DBList = new List<StaffOrganisationModel>();
            foreach (StaffOrganisationModel item in model.StaffOrganisationList)
            {
                DBList.Add(item.Clone());
            }
            SessionManager.StaffOrganisationListDBVersion = DBList;
            SessionManager.StaffRoleList = model.RoleList;
            SessionManager.RootOrganisation = model.RootNodeOrganisation;
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(StaffApplicationAdminVM model)
        {
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private StaffApplicationAdminVM GetUpdatedModel()
        {
            StaffApplicationAdminVM model = new StaffApplicationAdminVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (SessionManager.RootOrganisation != null)
            {
                model.RootNodeOrganisation = SessionManager.RootOrganisation;
            }
            if (SessionManager.CurrentApplicationCode != null)
            {
                model.SelectedApplicationCode = SessionManager.CurrentApplicationCode;
            }

            model.StaffItem = SessionManager.CurrentStaffForAdmin;


            //THERE IS A BUG IN THE WAY THE DEFAULT MODEL BINDER HANDLES COLLECTIONS IT DOESN'T UPDATE THE COLLECTION IT REPLACES IT WITH A NEW COLLECTION CONTAINING ONLY THE POSTED VALUES
            //MICROSOFT SHOULD HAVE CALLED IT TryCreateModel and not TryUpdateModel NOW I AM GOING TO HAVE TO RECONSTRUCT MY COLLECTION AGAIN
            //
            //SOLUTION:
            //i) Deliberately misname the collection in BeginCollectionItem in the partial view. The default binder will not be able to map this for the child view models
            //ii) Call the TryUpdate Model for each child item individually using the misnamed collection - this will map.
            //iii) Call the main TryUpdateModel Later and it won't overwrite what it can't map - 
            //************************************************************************************************************************************************************
            int i = 0;
            foreach (var item in model.OrganisationsByTypesList)
            {
                if (i != 0)
                {
                    TryUpdateModel(item, "OrganisationsByRows[" + Request.Form["OrganisationsByRows.Index"].Split(',')[i - 1] + "]");
                }
                i++;
            }
            //************************************************************************************************************************************************************8

            //Need to loop around application attributes
            foreach (var item in model.StaffApplicationAttributeList)
            {
                //If there is an entry in the staff attributes
                string StaffAttributeValue = Request.Form["StaffAttributeValue_" + item.ApplicationAttributeItem.Code.ToString()];
                if (!string.IsNullOrEmpty(StaffAttributeValue))
                {
                    if (item.ApplicationAttributeItem.AttributeType == "Bool")
                    {
                        //Convert 'on' to 'Yes'
                        StaffAttributeValue = StaffAttributeValue == "on" ? "Yes" : "No";
                    }
                    if (item.StaffAttributeItem != null)
                    {
                        item.StaffAttributeItem.LookupValue = StaffAttributeValue;
                    }
                    else
                    {
                        item.StaffAttributeItem = new StaffAttributesModel()
                        {
                            ApplicationAttributeCode = item.ApplicationAttributeItem.Code,
                            ApplicationCode = item.ApplicationAttributeItem.ApplicationCode,
                            Code = Guid.NewGuid(),
                            IsActive = true,
                            LookupValue = StaffAttributeValue,
                            StaffCode = model.StaffItem.Code
                        };
                    }
                }
                else
                //No entry in the staff attributes
                {
                    if (item.StaffAttributeItem != null)
                    {
                        item.StaffAttributeItem = null;
                    }
                }
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************

            #region Set the default organisation
            //NEED TO WORK OUT WHICH RADIO BUTTON IS SET FOR THE DEFAULT ORGANISATION
            Guid? selectedDefaultOrganisation = null;
            var defaultradio = Request.Form["defaultradio"];
            if (defaultradio != null)
            {
                string DefaultStaffOrgCode = Request.Form["defaultradio"].Substring(32);
                selectedDefaultOrganisation = Guid.Parse(DefaultStaffOrgCode);
            }
            foreach (StaffOrganisationModel currentItem in model.StaffOrganisationList)
            {
                if (currentItem.Code == selectedDefaultOrganisation)
                {
                    currentItem.IsDefault = true;
                }
                else
                {
                    currentItem.IsDefault = false;
                }
            }
            #endregion

            return model;
        }
        #endregion

        #region private methods for create check

        private StaffApplicationAdminVM ConvertSecurityCheckDC(StaffApplicationAdminVMDC returnedObject)
        {
            StaffApplicationAdminVM model = new StaffApplicationAdminVM();

            RepopulateListsFromCacheSession(model);

            return model;
        }

        private void AddListsToSession(StaffApplicationAdminVM model)
        {

            //*********************************
            //*PLACE HOLDER FOR SESSION LISTS * 
            //*********************************
            SessionManager.ApplicationList = model.ApplicationList;
            SessionManager.OrganisationByTypeList = model.OrganisationsByTypesList;
            SessionManager.StaffRoleList = model.RoleList;
        }
        private void RepopulateListsFromCacheSession(StaffApplicationAdminVM model)
        {
            //*********************************
            //*PLACE HOLDER FOR SESSION LISTS * 
            //*********************************
            model.ApplicationList = SessionManager.ApplicationList;
            model.OrganisationsByTypesList = SessionManager.OrganisationByTypeList;
            model.RoleList = SessionManager.StaffRoleList;
            model.StaffApplicationAttributeList = SessionManager.StaffApplicationAttributeList;
            model.StaffOrganisationList = SessionManager.StaffOrganisationList;

            //*********************************
            //*     POPULATE CACHED LISTS     *
            //*********************************

        }


        #endregion


        #endregion

        #region AdminMenu
        //

        //
        // GET: /Admin/AdminMenu
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult AdminMenu()
        {
            // Retrieve model containing all Admin menu items
            var model = GetModel();

            return View(model);
        }

        // POST: /Admin/AdminMenu
        [HttpPost]
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult AdminMenu(bool isUseless = false)
        {
            // Search request object keys
            foreach (string Key in Request.Form.Keys)
            {
                // Find Select button which was clicked
                if (Key.StartsWith("SelectButton_"))
                {
                    // Retrieve the ID assoicated with this button
                    int value = int.Parse(Key.Substring(13));

                    // Retrieve model containing all Admin menu items
                    var model = GetModel();

                    // Find item selected
                    AdminMenuModel selectedMenuItem = model.AdminList.Find(x => x.ID == value);

                    string[] location = selectedMenuItem.Location.Split('/');

                    // Redirect to Admin screen of item selected
                    return RedirectToAction(location[1], location[0]);

                }
            }

            return RedirectToAction("Index");
        }

        private AdminMenuVM GetModel()
        {
            var model = new AdminMenuVM();
            model.AdminList = new List<AdminMenuModel>();

            model.AdminList.Add(new AdminMenuModel { ID = 1, Name = FixedResources.ENTITYNAME_COUNTRY, Location = "Country/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 2, Name = FixedResources.ENTITYNAME_GRADE, Location = "Grade/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 3, Name = FixedResources.ENTITYNAME_HOLIDAY, Location = "Holiday/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 4, Name = FixedResources.ENTITYNAME_NONSTANDARDHOLIDAY, Location = "NonStandardHoliday/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 5, Name = FixedResources.ENTITYNAME_ORGANISATION, Location = "ApplicationOrganisation/ApplicationOrganisationSelect" });
            model.AdminList.Add(new AdminMenuModel { ID = 6, Name = FixedResources.ENTITYNAME_STAFF, Location = "Staff/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 7, Name = FixedResources.ENTITYNAME_STAFFDETAILS, Location = "StaffDetails/Search" });
            model.AdminList.Add(new AdminMenuModel { ID = 8, Name = FixedResources.ENTITYNAME_STAFFOFFICES, Location = "StaffOffices/Search" });

            return model;
        }
        #endregion

    }
}
