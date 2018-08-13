using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using AutoMapper;
using Dwp.Adep.Framework.Management.Web.Helpers;
using Dwp.Adep.Framework.Management.Web.ViewModels;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.AdminService;


namespace Dwp.Adep.Framework.Management.Web.Controllers
{
    public partial class StaffController : BaseController
    {
        #region Edit

        // GET: /Staff/Edit
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Edit()
        {
            // Retrieve ID from session
            string code = SessionManager.StaffCode;

            StaffVM model = new StaffVM();

            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.StaffItem = new StaffModel();
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
                // Create service instance
                AdminServiceClient sc = new AdminServiceClient();

                try
                {
                    // Call service to get Staff item and any associated lookups    
                    StaffVMDC returnedObject = sc.GetStaff(CurrentUser, CurrentUser, appID, "", code);

                    // Close service communication
                    sc.Close();

                    //Get view model from service
                    model = ConvertStaffDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    SessionManager.StaffServiceVersion = model.StaffItem;
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }
            }

            //Adds current retrieved Staff to session
            SessionManager.CurrentStaff = model.StaffItem;
            SetAccessContext(model);

            return View(model);
        }

        #endregion

        #region Create/Update

        // POST: /Staff/Edit with Create button submitting
        [CustomAuthorize(Roles =  FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateStaff(FormCollection collection)
        {
            return UpdateStaff();
        }

        // POST: /Staff/Edit with Save button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveStaff(FormCollection collection)
        {
            return UpdateStaff();
        }

        //This method is shared between create and save
        private ActionResult UpdateStaff()
        {
            // Get the updated model
            var model = GetUpdatedModel();

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
                    // Map model to data contract
                    StaffDC StaffItem = Mapper.Map<StaffDC>(model.StaffItem);

                    StaffVMDC returnedObject = null;

                    if (null == model.StaffItem.Code || model.StaffItem.Code == Guid.Empty)
                    {
                        // Call service to create new Staff item
                        returnedObject = sc.CreateStaff(CurrentUser, CurrentUser, appID, "", StaffItem);
                    }
                    else
                    {
                        // Call service to update Staff item
                        returnedObject = sc.UpdateStaff(CurrentUser, CurrentUser, appID, "", StaffItem);
                    }

                    // Close service communication
                    sc.Close();

                    // Retrieve item returned by service
                    var createdStaff = returnedObject.StaffItem;

                    // Map data contract to model
                    model.StaffItem = Mapper.Map<StaffModel>(createdStaff);

                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    // Set access context to Edit mode
                    model.AccessContext = StaffAccessContext.Edit;

                    // Save version of item returned by service into session
                    SessionManager.StaffServiceVersion = model.StaffItem;
                    SessionManager.CurrentStaff = model.StaffItem;

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
                    // the POSTED values and not the changed ones.
                    ModelState.Clear();
                    model.Message = FixedResources.MESSAGE_UPDATE_SUCCEEDED;
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

        // POST: /Staff/Edit with Exit button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitStaff(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";

                //remove the current values from session
                SessionManager.CurrentStaff = null;
                SessionManager.StaffServiceVersion = null;

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

        #region New

        // POST: /Staff/Edit with New button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewStaff(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);

            //Clear Down Session
            SessionManager.StaffCode = null;
            SessionManager.CurrentStaff = null;
            SessionManager.StaffServiceVersion = null;

            //Go to the Edit Screen
            return RedirectToAction("Edit", "Staff");
        }

        #endregion

        #region Search

        // GET: /Staff/Search
        //This is called when first entering search Staff screen or when paging
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        public ActionResult Search(int page = 1)
        {
            // Create service instance
            AdminServiceClient sc = new AdminServiceClient();

            // Create model
            StaffSearchVM model = new StaffSearchVM();

            try
            {
                #region Setup search citeria

                //Create search criteria data conatract
                StaffSearchCriteriaDC searchCriteriaDC = Mapper.Map<StaffSearchCriteriaDC>(model.SearchCriteria);

                #endregion

                // Call service
                StaffSearchVMDC response = sc.SearchStaff(CurrentUser, CurrentUser, appID, "", searchCriteriaDC, page, PageSize, true);

                // Close service communication
                sc.Close();

                //Map response back to view model
                model.MatchList = Mapper.Map<IEnumerable<StaffSearchMatchDC>, List<StaffSearchMatchModel>>(response.MatchList);

                // Set paging values
                model.TotalRows = response.RecordCount;
                model.PageSize = SessionManager.PageSize;
                model.PageNumber = page;

                // Store the page number we were on
                SessionManager.StaffPageNumber = model.PageNumber;

                return View(model);
            }
            catch (Exception e)
            {
                // Handle the exception
                string message = ExceptionManager.HandleException(e, sc);
                model.Message = message;

                return View(model);
            }

        }

        // POST: /Staff/Search
        //This is called when clicking search button
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(StaffSearchVM model, int page = 1)
        {
            // Create service instance
            AdminServiceClient sc = new AdminServiceClient();

            #region Setup search citeria

            //Repopulate search criteria if already entered
            if (null == model.SearchCriteria && SessionManager.StaffSearchCritera != null)
            {
                model.SearchCriteria = SessionManager.StaffSearchCritera;
            }

            //Save search criteria to session
            SessionManager.StaffSearchCritera = model.SearchCriteria;

            //Create search criteria data conatract
            StaffSearchCriteriaDC searchCriteriaDC = Mapper.Map<StaffSearchCriteriaDC>(model.SearchCriteria);

            #endregion

            // Call service
            StaffSearchVMDC response = sc.SearchStaff(CurrentUser, CurrentUser, appID, "", searchCriteriaDC, page, PageSize, true);

            // Close service communication
            sc.Close();

            //Map response back to view model
            model.MatchList = Mapper.Map<IEnumerable<StaffSearchMatchDC>, List<StaffSearchMatchModel>>(response.MatchList);

            // Set paging values
            model.TotalRows = response.RecordCount;
            model.PageSize = SessionManager.PageSize;
            model.PageNumber = page;

            // Store the page number we were on
            SessionManager.StaffPageNumber = model.PageNumber;

            return View(model);
        }

        // POST: /Staff/Search
        //This is called when clicking Select button
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchSelect(FormCollection collection)
        {
            // Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
                // Test if Select button was clicked...
                if (Key.StartsWith("Search::SearchSelect_"))
                {
                    // Retrieve ID for entity which was selected
                    Guid Value = Guid.Parse(Key.Substring(21));

                    // Store ID for Edit screen
                    SessionManager.StaffCode = Value.ToString();

                    // Call out to Edit screen
                    return RedirectToAction("Edit", "Staff", new { code = Value });

                }
            }

            return View();
        }

        #endregion

        #region Private methods

        private void SetFlagsFalse(StaffVM model)
        {
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(StaffVM model)
        {
            //TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private StaffVM GetUpdatedModel()
        {
            StaffVM model = new StaffVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (SessionManager.CurrentStaff != null)
            {
                model.StaffItem = SessionManager.CurrentStaff;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (SessionManager.CurrentStaff != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.StaffItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }

        private StaffVM ConvertStaffDC(StaffVMDC returnedObject)
        {
            StaffVM model = new StaffVM();

            // Map Staff Item
            model.StaffItem = Mapper.Map<StaffDC, StaffModel>(returnedObject.StaffItem);

            // Map lookup data lists
            model.GradeList = Mapper.Map<IEnumerable<GradeDC>, List<GradeModel>>(returnedObject.GradeList);

            return model;
        }

        private void RepopulateListsFromCacheSession(StaffVM model)
        {
            // Populate cached lists if they are empty. Will invoke service call
            StaffLookupListsCacheObject CachedLists = CacheManager.StaffListCache;

            // Retrieve any cached lists to model
            model.GradeList = CachedLists.GradeList;
        }

        private void MergeNewValuesWithOriginal(StaffModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            StaffModel OriginalValuesFromSession = SessionManager.CurrentStaff;

        }

        private void SetAccessContext(StaffVM model)
        {
            //Decide on access context
            if (null == model.StaffItem || model.StaffItem.Code == Guid.Empty)
            {
                // Create context
                model.AccessContext = StaffAccessContext.Create;
            }
            else
            {
                // Edit context
                model.AccessContext = StaffAccessContext.Edit;
            }
        }

        private void DetermineIsDirty(StaffVM model)
        {
            //Compare the Staff to the original session
            if (model.StaffItem.PublicInstancePropertiesEqual(SessionManager.StaffServiceVersion, "RowIdentifier"))
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
