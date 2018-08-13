using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dwp.Adep.Framework.Management.Web.ViewModels;
using Dwp.Adep.Framework.Management.Web.AdminService;
using AutoMapper;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using Dwp.Adep.Framework.Management.Web.Helpers;

namespace Dwp.Adep.Framework.Management.Web.Controllers
{
    public class ApplicationOrganisationController : BaseController
    {
        //
        // GET: /ApplicationOrganisation/

        public ActionResult ApplicationOrganisationSelect()
        {
            var model = new ApplicationOrganisationSelectVM();

            //Initialize lists
            model.OrganisationsByTypesList = new List<OrganisationByTypeVM>();
            model.ApplicationList = new List<ApplicationModel>();
            
            //Retreive all applications that are available to the current user.
            //Currently working in ********************GOD MODE********************* - If you have access to this screen you can maintain staff in all apps in the framework.
            try
            {
                var sc = new AdminServiceClient();
                var ApplicationList = sc.GetAllApplication(User.Identity.Name, User.Identity.Name, "Framework", "", false);
                model.ApplicationList = Mapper.Map<IEnumerable<ApplicationDC>, IEnumerable<ApplicationModel>>(ApplicationList).ToList();
            }
            catch (Exception ex)
            {
                model.Message = FixedResources.MESSAGE_RETRIEVAL_FAILED;
                return View(model);
            }

            

            //Initialize lists
            model.OrganisationsByTypesList = new List<OrganisationByTypeVM>();
            
            SessionManager.ApplicationList = model.ApplicationList;
            SessionManager.OrganisationByTypeList = model.OrganisationsByTypesList;
            
            return View(model);
        }

        #region Application Staff Admin post backs now split by button pressed

        #region Update Application
        // POST: /Admin/ApplicationStaffAdmin with UpdateApplication button submitting
        [HttpPost]
        [HttpParamApplicationOrganisationSelect(Prefix = "ApplicationOrganisationSelect::")]
        public ActionResult UpdateApplication(Guid? staffCode, FormCollection form)
        {
            // Get the updated model
            var model = GetUpdatedModel();

            if (model.SelectedApplicationCode.HasValue)
            {
                // Create instance of Admin service
                AdminServiceClient sc = new AdminServiceClient();

                try
                {
                    ApplicationOrganisationSelectVMDC returnedObject = sc.GetApplicationOrganisationsByApplicationCode(User.Identity.Name, User.Identity.Name, "Framework", "", model.SelectedApplicationCode.Value);
                    model.OrganisationsByTypesList = new List<OrganisationByTypeVM>();
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
                //remove the current values from session
                model.OrganisationsByTypesList = null;
                // Redirect to Admin screen of item selected  
                return RedirectToAction("ApplicationOrganisationSelect","ApplicationOrganisation");
                
            }
            ModelState.Clear();
            return View(model);
        }

        #endregion

        #region UpdateOrganisationDropDowns
        // POST: /Admin/ApplicationStaffAdmin with one of the UpdateOrganisationDropDowns buttons submitting
        [HttpPost]
        [HttpParamApplicationOrganisationSelect(Prefix = "ApplicationOrganisationSelect::")]
        public ActionResult UpdateOrganisation(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false
            
            //Establish which drop downs need updating
            Guid OrganisationByTypeValue = Guid.Empty;
            Guid PrefixValue = Guid.Empty;
            foreach (string KeyItem in form)
            {
                if (KeyItem.StartsWith("ApplicationOrganisationSelect::UpdateOrganisation"))
                {
                    PrefixValue = Guid.Parse(KeyItem.Substring(50));
                    var HiddenFieldKey = "OrganisationsByRows[" + PrefixValue.ToString() + "].OrganisationTypeItem.Code";
                    OrganisationByTypeValue = Guid.Parse(form[HiddenFieldKey]);
                    break;
                }
            }

            var AllOrganisationsForApplicationByTypesList = SessionManager.AllOrganisationsForApplicationByTypesList;
            
            // Modified to handle multiple organisation types for the same level of the organisation hierarchy.
            var clickedItem = model.OrganisationsByTypesList.Single(x => x.OrganisationTypeItem.Code == OrganisationByTypeValue);
            if (clickedItem.SelectedOrganisationCode != null)
            {
                var AllOrgsListsForNextLevel = AllOrganisationsForApplicationByTypesList.FindAll(x => x.OrganisationTypeItem.LevelNumber == clickedItem.OrganisationTypeItem.LevelNumber + 1);
                if (AllOrgsListsForNextLevel != null && AllOrgsListsForNextLevel.Count > 0)
                {
                    OrganisationByTypeVM AllOrgsForNextLevel = null;
                    foreach (OrganisationByTypeVM organisationTypeVMitem in AllOrgsListsForNextLevel)
                    {
                        //int levelNum = organisationTypeVMitem.OrganisationTypeItem.LevelNumber;
                        if (organisationTypeVMitem.OrganisationList.Count > 0 )
                        {
                            foreach (var organisationItem in organisationTypeVMitem.OrganisationList)
                            {
                                if(organisationItem.ParentID == Guid.Parse(clickedItem.SelectedOrganisationCode ))
                                {
                                    AllOrgsForNextLevel = organisationTypeVMitem;
                                    break;
                                }
                                //var ChildVM = model.OrganisationsByTypesList.SingleOrDefault(x => x.OrganisationTypeItem.ParentOrganisationTypeCode == clickedItem.OrganisationTypeItem.Code);
                            }
                            if(AllOrgsForNextLevel != null)
                            {
                                break;
                            }
                        }

                    }
                    if(AllOrgsForNextLevel != null)
                    {
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

            return View(model);
        }

        #endregion

        #region ViewButton
        // POST: /Admin/ApplicationStaffAdmin with Add button submitting
        [HttpPost]
        [HttpParamApplicationOrganisationSelect(Prefix = "ApplicationOrganisationSelect::")]
        public ActionResult ViewOrganisation(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false
         
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
                SessionManager.OrganisationCode = selectedOrganisation.Code.ToString();
                SessionManager.ApplicationCode = model.SelectedApplicationCode.ToString();
                return RedirectToAction("Edit");
                //Redirect to OrganisationEditScreen using path
            }

            return View(model);
        }

        #endregion
        #region AddButton
        // POST: /Admin/ApplicationStaffAdmin with Add button submitting
        [HttpPost]
        [HttpParamApplicationOrganisationSelect(Prefix = "ApplicationOrganisationSelect::")]
        public ActionResult AddOrganisation(Guid? staffCode, FormCollection form)
        {
            var model = GetUpdatedModel();
            //Set flags false

            SessionManager.OrganisationCode = null;
            SessionManager.ApplicationCode = model.SelectedApplicationCode.ToString();
            return RedirectToAction("Edit");
            //Redirect to OrganisationEditScreen using path
            
        }

        #endregion
        #endregion


        #region private methods for application organisation 
        private void AddListsToSession(ApplicationOrganisationAdminVM model)
        {

            //*********************************
            //*PLACE HOLDER FOR SESSION LISTS * 
            //*********************************
            //SessionManager.OrganisationByTypeList = model.OrganisationsByTypesList;
            SessionManager.AllTypesForApplication = model.AllTypesForApplication;
            
        }
        private void RepopulateListsFromCacheSession(ApplicationOrganisationSelectVM model)
        {
            //*********************************
            //*PLACE HOLDER FOR SESSION LISTS * 
            //*********************************
            model.ApplicationList = SessionManager.ApplicationList;
            model.OrganisationsByTypesList = SessionManager.OrganisationByTypeList;
      
            //*********************************
            //*     POPULATE CACHED LISTS     *
            //*********************************

        }

        private static void ClearSesssionObjects(StaffApplicationAdminVM model)
        {
            //remove the current values from session
            SessionManager.OrganisationByTypeList = null;
            SessionManager.ApplicationList = null;
           
        }


        private static void MergeReturnedObjectToModel(ApplicationOrganisationSelectVM model, ApplicationOrganisationSelectVMDC returnedObject)
        {
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

            
            model.RootNodeOrganisation = Mapper.Map<OrganisationModel>(returnedObject.RootNodeOrganisation);

            model.SelectedApplicationCode = returnedObject.SelectedApplicationCode;

            //Add the values to session
            SessionManager.OrganisationByTypeList = model.OrganisationsByTypesList;
            
            SessionManager.RootOrganisation = model.RootNodeOrganisation;
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(ApplicationOrganisationSelectVM model)
        {
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private ApplicationOrganisationSelectVM GetUpdatedModel()
        {
            ApplicationOrganisationSelectVM model = new ApplicationOrganisationSelectVM();
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

            

            //THERE IS A BUG IN THE WAY THE DEFAULT MODEL BINDER HANDLES COLLECTIONS IT DOESN'T UPDATE THE COLLECTION IT REPLACES IT WITH A NEW COLLECTION CONTAINING ONLY THE POSTED VALUES
            //MICROSOFT SHOULD HAVE CALLED IT TryCreateModel and not TryUpdateModel NOW I AM GOING TO HAVE TO RECONSTRUCT MY COLLECTION AGAIN
            //
            //SOLUTION:
            //i) Deliberately misname the collection in BeginCollectionItem in the partial view. The default binder will not be able to map this for the child view models
            //ii) Call the TryUpdate Model for each child item individually using the misnamed collection - this will map.
            //iii) Call the main TryUpdateModel Later and it won't overwrite what it can't map - 
            //************************************************************************************************************************************************************
            if (model.OrganisationsByTypesList != null)
            {
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
            }
            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************

            return model;
        }
        #endregion

        #region Edit

        // GET: /Organisation/Edit
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Edit()
        {
            // Retrieve ID from session
            string OrganisationCode = SessionManager.OrganisationCode;
            string ApplicationCode = SessionManager.ApplicationCode;
            ApplicationOrganisationAdminVM model = new ApplicationOrganisationAdminVM();

            // Not from staff or error
            if (String.IsNullOrEmpty(OrganisationCode))
            {
                //If session has lists then use them
                //RepopulateListsFromCacheSession(model);
                // Create service instance
                AdminServiceClient sc = new AdminServiceClient();
                try
                {
                    // Call service to get Organisation item and any associated lookups    
                    ApplicationOrganisationAdminVMDC returnedObject = sc.GetOrganisationWithParent(CurrentUser, CurrentUser, appID, "", Guid.Empty, Guid.Parse(ApplicationCode));

                    // Close service communication
                    sc.Close();
                    //Get view model from service
                    model = ConvertApplicationOrganisationDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);
                    AddListsToSession(model);
                    SessionManager.RootOrganisation = model.RootNodeOrganisation;
                    SessionManager.OrganisationServiceVersion = model.OrganisationItem.Clone();
                    SessionManager.MaximumHopsToChildOrganisation = model.MaximumHopsToChildOrganisation;
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }
                //Assume we are in create mode as no code passed
                model.OrganisationItem = new OrganisationModel() { IsActive = true };
               
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
                // Create service instance
                AdminServiceClient sc = new AdminServiceClient();

                try
                {
                    // Call service to get Organisation item and any associated lookups    
                    ApplicationOrganisationAdminVMDC returnedObject = sc.GetOrganisationWithParent(CurrentUser, CurrentUser, appID, "", Guid.Parse(OrganisationCode),Guid.Parse(ApplicationCode));

                    // Close service communication
                    sc.Close();
                    //Get view model from service
                    model = ConvertApplicationOrganisationDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);
                    AddListsToSession(model);
                    //Store the service version
                    SessionManager.RootOrganisation = model.RootNodeOrganisation;
                    SessionManager.OrganisationServiceVersion = model.OrganisationItem.Clone();
                    SessionManager.MaximumHopsToChildOrganisation = model.MaximumHopsToChildOrganisation;
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }
            }

            //Adds current retrieved Organisation to session
            SessionManager.CurrentOrganisation = model.OrganisationItem;
            SetAccessContext(model);

            return View(model);
        }

        #endregion

        #region Create/Update

        // POST: /Organisation/Edit with Create button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateOrganisation(FormCollection collection)
        {
            return UpdateOrganisation();
        }

        // POST: /Organisation/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveOrganisation(FormCollection collection)
        {
            return UpdateOrganisation();
        }

        
        
            // POST: /Organisation/Edit with Create button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateOrganisationType(FormCollection collection)
        {
            // Get the updated model
            var model = GetUpdatedModelOrg();

            // Test to see if there are any errors
            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToArray();

            //Set flags false
            SetFlagsFalse(model);

            foreach (var item in model.OrganisationsByTypesList)
            {
                item.SelectedOrganisationCode = item.OrganisationList.SingleOrDefault(x=>x.Code == model.ParentOrganisationCode) != null ? model.ParentOrganisationCode.ToString() : "";
            }
            
            return View(model);
        }
            
            //This method is shared between create and save
        private ActionResult UpdateOrganisation()
        {
            // Get the updated model
            var model = GetUpdatedModelOrg();

            // Test to see if there are any errors
            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToArray();

            //Set flags false
            SetFlagsFalse(model);

            // Test to see if the model has validated correctly
            if (ModelState.IsValid)
            {
                // Create service instance
                AdminServiceClient sc = new AdminServiceClient();

                //Attempt update
                try
                {
                    //FindParentOrganisation
                    var SelectedOrganisationTypeCode = model.OrganisationItem.OrganisationTypeCode;
                    var SelectedOrganisationTypeParentLevelNumber = model.AllTypesForApplication.Single(x => x.Code == SelectedOrganisationTypeCode).LevelNumber - 2;
                    //OrganisationByTypeVM ParentOrgsList = SessionManager.AllOrganisationsForApplicationByTypesList[SelectedOrganisationTypeParentLevelNumber];
                    //var ParentOrganisation = ParentOrgsList.OrganisationList.Single(x => x.Code == model.ParentOrganisationCode);
                    

                    // Map model to data contract
                    OrganisationDC OrganisationItem = Mapper.Map<OrganisationDC>(model.OrganisationItem);

                    ApplicationOrganisationAdminVMDC returnedObject = null;

                    if (null == model.OrganisationItem.Code || model.OrganisationItem.Code == Guid.Empty)
                    {
                        // Call service to create new Organisation item
                        returnedObject = sc.CreateOrganisationForApplication(CurrentUser, CurrentUser, appID, "", OrganisationItem, model.ParentOrganisationCode, Guid.Parse(SessionManager.ApplicationCode));
                    }
                    else
                    {
                        // Call service to update Organisation item
                        returnedObject = sc.UpdateOrganisationForApplication(CurrentUser, CurrentUser, appID, "", OrganisationItem,model.ParentOrganisationCode,Guid.Parse(SessionManager.ApplicationCode));
                    }

                    // Close service communication
                    sc.Close();
                                       
                    //not saved because organisation already existed
                    if (!string.IsNullOrEmpty(returnedObject.Message))
                    {
                        if (returnedObject.Message.Contains("Update failed"))
                        {

                            // Set access context to Edit mode
                            model.AccessContext = ApplicationOrganisationAccessContext.Edit;
                            model.Message = FixedResources.MESSAGE_UPDATEFAILED_DUE_TO_ORGANISATIONALREADY_EXISTS;
                           
                        }
                        else
                        {
                            // Set access context to Create mode
                            model.AccessContext = ApplicationOrganisationAccessContext.Create;
                            model.Message = FixedResources.MESSAGE_CREATEFAILED_DUE_TO_ORGANISATIONALREADY_EXISTS;
                        }
                    }

                    else
                    {

                        //Get view model from service
                        model = ConvertApplicationOrganisationDC(returnedObject);

                        SessionManager.RootOrganisation = model.RootNodeOrganisation;
                        SessionManager.OrganisationServiceVersion = model.OrganisationItem.Clone();
                        SessionManager.MaximumHopsToChildOrganisation = model.MaximumHopsToChildOrganisation;
                        // Save version of item returned by service into session
                        SessionManager.OrganisationServiceVersion = model.OrganisationItem.Clone();
                        SessionManager.CurrentOrganisation = model.OrganisationItem;


                        // Set access context to Edit mode
                        model.AccessContext = ApplicationOrganisationAccessContext.Edit;

                        ResolveFieldCodesToFieldNamesUsingLists(model);
                        AddListsToSession(model);
                        //Store the service version
                        SessionManager.OrganisationServiceVersion = model.OrganisationItem;
                        // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
                        // the POSTED values and not the changed ones.
                        ModelState.Clear();
                        model.Message = FixedResources.MESSAGE_UPDATE_SUCCEEDED;
                    }
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }
            }

            return View(model);
        }

        #endregion

        #region Exit

        // POST: /Organisation/Edit with Exit button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitOrganisation(FormCollection collection)
        {
            var model = GetUpdatedModelOrg();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";

                //remove the current values from session
                SessionManager.CurrentOrganisation = null;
                SessionManager.OrganisationServiceVersion = null;

                return RedirectToAction("ApplicationOrganisationSelect", "ApplicationOrganisation");

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

        #region New

        // POST: /Organisation/Edit with New button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewOrganisation(FormCollection collection)
        {
            var model = GetUpdatedModelOrg();
            //Set flags false
            SetFlagsFalse(model);

            //Clear Down Session
            SessionManager.OrganisationCode = null;
            SessionManager.CurrentOrganisation = null;
            SessionManager.OrganisationServiceVersion = null;

            //Go to the Edit Screen
            return RedirectToAction("Edit", "Organisation");
        }

        #endregion

        #region Private methods

        private void SetFlagsFalse(ApplicationOrganisationAdminVM model)
        {
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(ApplicationOrganisationAdminVM model)
        {
            //TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private ApplicationOrganisationAdminVM GetUpdatedModelOrg()
        {
            ApplicationOrganisationAdminVM model = new ApplicationOrganisationAdminVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (SessionManager.CurrentOrganisation != null)
            {
                model.OrganisationItem = SessionManager.CurrentOrganisation;
            }
            if (SessionManager.RootOrganisation != null)
            {
                model.RootNodeOrganisation = SessionManager.RootOrganisation;
            }
            
            model.MaximumHopsToChildOrganisation = SessionManager.MaximumHopsToChildOrganisation;
            
            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (SessionManager.CurrentOrganisation != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.OrganisationItem);
                //***************************************************************************************************************************
            }
            
            SetAccessContext(model);

            SetApplicationOrganisationContext(model);

            return model;
        }

        private ApplicationOrganisationAdminVM ConvertApplicationOrganisationDC(ApplicationOrganisationAdminVMDC returnedObject)
        {
            ApplicationOrganisationAdminVM model = new ApplicationOrganisationAdminVM();

            // Map Organisation Item
            model.OrganisationItem = Mapper.Map<OrganisationDC, OrganisationModel>(returnedObject.OrganisationItem);
            model.ParentOrganisationCode = returnedObject.ParentOrganisation == null ? Guid.Empty : returnedObject.ParentOrganisation.Code;
            // Map lookup data lists
            model.MaximumHopsToChildOrganisation = returnedObject.MaximumHopsToChildOrganisation;
            model.AllTypesForApplication = Mapper.Map<IEnumerable<OrganisationTypeDC>, List<OrganisationTypeModel>>(returnedObject.AllTypesForApplication);
            model.OrganisationsByTypesList = new List<OrganisationByTypeVM>();
            MergeReturnedObjectToModel(model, returnedObject);
            return model;
        }

        private void RepopulateListsFromCacheSession(ApplicationOrganisationAdminVM model)
        {
            //*********************************
            //*PLACE HOLDER FOR SESSION LISTS * 
            //*********************************
            model.OrganisationsByTypesList = SessionManager.AllOrganisationsForApplicationByTypesList;
            model.AllTypesForApplication = SessionManager.AllTypesForApplication;
        
            //*********************************
            //*     POPULATE CACHED LISTS     *
            //*********************************
            // Populate cached lists if they are empty. Will invoke service call
            
        }
        private static void MergeReturnedObjectToModel(ApplicationOrganisationAdminVM model, ApplicationOrganisationAdminVMDC returnedObject)
        {
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

                foreach (var item in AllOrganisationsForApplicationByTypesList)
                {
                    
                    model.OrganisationsByTypesList.Add(new OrganisationByTypeVM()
                    {
                        OrganisationList = item.OrganisationList,
                        OrganisationTypeItem = item.OrganisationTypeItem,
                        SelectedOrganisationCode = item.OrganisationList.SingleOrDefault(x=>x.Code==model.ParentOrganisationCode) != null ? model.ParentOrganisationCode.ToString() : ""
                    });
                }

            }

            model.RootNodeOrganisation = Mapper.Map<OrganisationModel>(returnedObject.RootNodeOrganisation);

            //model.SelectedApplicationCode = returnedObject.SelectedApplicationCode;

        }
        private void MergeNewValuesWithOriginal(OrganisationModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            OrganisationModel OriginalValuesFromSession = SessionManager.CurrentOrganisation;

        }

        private void SetAccessContext(ApplicationOrganisationAdminVM model)
        {
            //Decide on access context
            if (null == model.OrganisationItem || model.OrganisationItem.Code == Guid.Empty)
            {
                // Create context
                model.AccessContext = ApplicationOrganisationAccessContext.Create;
            }
            else
            {
                // Edit context
                model.AccessContext = ApplicationOrganisationAccessContext.Edit;
            }
        }
        private void SetApplicationOrganisationContext(ApplicationOrganisationAdminVM model)
        {
            if (model.OrganisationItem.OrganisationTypeCode != Guid.Empty)
            {
                if (model.AllTypesForApplication.Single(x => x.Code == model.OrganisationItem.OrganisationTypeCode).LevelNumber == 1)
                {
                    ModelState.Remove("ParentOrganisationCode");
                }
            }
        }
        private void DetermineIsDirty(ApplicationOrganisationAdminVM model)
        {
            //Compare the Organisation to the original session
            if (model.OrganisationItem.PublicInstancePropertiesEqual(SessionManager.OrganisationServiceVersion, "RowIdentifier"))
            {
                model.IsViewDirty = false;
            }
            else
            {
                model.IsViewDirty = true;
            }

        }
        #endregion
    }
}
