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
    public partial class ApplicationAttributeController : BaseController
    {
		#region Edit
		
        // GET: /ApplicationAttribute/Edit
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = SessionManager.ApplicationAttributeCode;
			
            ApplicationAttributeVM model = new ApplicationAttributeVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.ApplicationAttributeItem = new ApplicationAttributeModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				AdminServiceClient sc = new AdminServiceClient();
				
                try
                {
					// Call service to get ApplicationAttribute item and any associated lookups    
                    ApplicationAttributeVMDC returnedObject = sc.GetApplicationAttribute(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					sc.Close();

                    //Get view model from service
                    model = ConvertApplicationAttributeDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    SessionManager.ApplicationAttributeServiceVersion = model.ApplicationAttributeItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved ApplicationAttribute to session
            SessionManager.CurrentApplicationAttribute = model.ApplicationAttributeItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /ApplicationAttribute/Edit with Create button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateApplicationAttribute(FormCollection collection)
        {
            return UpdateApplicationAttribute();
        }

        // POST: /ApplicationAttribute/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveApplicationAttribute(FormCollection collection)
        {
            return UpdateApplicationAttribute();
        }

        //This method is shared between create and save
        private ActionResult UpdateApplicationAttribute()
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
                    ApplicationAttributeDC ApplicationAttributeItem = Mapper.Map<ApplicationAttributeDC>(model.ApplicationAttributeItem);

					ApplicationAttributeVMDC returnedObject = null;

                    if (null == model.ApplicationAttributeItem.Code || model.ApplicationAttributeItem.Code == Guid.Empty)
                    {
						// Call service to create new ApplicationAttribute item
                        returnedObject = sc.CreateApplicationAttribute(CurrentUser, CurrentUser, appID, "", ApplicationAttributeItem);
                    }
                    else
                    {
						// Call service to update ApplicationAttribute item
                        returnedObject = sc.UpdateApplicationAttribute(CurrentUser, CurrentUser, appID, "", ApplicationAttributeItem);
                    }

					// Close service communication
					sc.Close();
					
					// Retrieve item returned by service
                    var createdApplicationAttribute = returnedObject.ApplicationAttributeItem;

					// Map data contract to model
                    model.ApplicationAttributeItem = Mapper.Map<ApplicationAttributeModel>(createdApplicationAttribute);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = ApplicationAttributeAccessContext.Edit;

					// Save version of item returned by service into session
                    SessionManager.ApplicationAttributeServiceVersion = model.ApplicationAttributeItem;
                    SessionManager.CurrentApplicationAttribute = model.ApplicationAttributeItem;
                    
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

        // POST: /ApplicationAttribute/Edit with Exit button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitApplicationAttribute(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                SessionManager.CurrentApplicationAttribute = null;
                SessionManager.ApplicationAttributeServiceVersion = null;

                return RedirectToAction("Search", "ApplicationAttribute");
                
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

        // POST: /ApplicationAttribute/Edit with New button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewApplicationAttribute(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			SessionManager.ApplicationAttributeCode = null;
            SessionManager.CurrentApplicationAttribute = null;
            SessionManager.ApplicationAttributeServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "ApplicationAttribute");
        }
		
		#endregion

		#region Search

        // GET: /ApplicationAttribute/Search
        //This is called when first entering search ApplicationAttribute screen or when paging
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			AdminServiceClient sc = new AdminServiceClient();
			
			// Create model
			ApplicationAttributeSearchVM model = new ApplicationAttributeSearchVM();
			
			try
			{
				ApplicationAttributeSearchVMDC response = sc.SearchApplicationAttribute(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				sc.Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<ApplicationAttributeSearchMatchDC>, List<ApplicationAttributeSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = SessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				SessionManager.ApplicationAttributePageNumber = model.PageNumber;
	        
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

        // POST: /ApplicationAttribute/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(ApplicationAttributeSearchVM model, int page = 1)
        {
		
			// Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
				// Test if Select button was clicked...
                if (Key.StartsWith("Search::SearchPost_"))
                {
					// Retrieve ID for entity which was selected
                    Guid Value = Guid.Parse(Key.Substring(19));
									
					// Store ID for Edit screen
					SessionManager.ApplicationAttributeCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "ApplicationAttribute", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(ApplicationAttributeVM model)
        {
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(ApplicationAttributeVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private ApplicationAttributeVM GetUpdatedModel()
        {
            ApplicationAttributeVM model = new ApplicationAttributeVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (SessionManager.CurrentApplicationAttribute != null)
            {
                model.ApplicationAttributeItem = SessionManager.CurrentApplicationAttribute;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (SessionManager.CurrentApplicationAttribute != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.ApplicationAttributeItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private ApplicationAttributeVM ConvertApplicationAttributeDC(ApplicationAttributeVMDC returnedObject)
        {
            ApplicationAttributeVM model = new ApplicationAttributeVM();
            
			// Map ApplicationAttribute Item
			model.ApplicationAttributeItem = Mapper.Map<ApplicationAttributeDC, ApplicationAttributeModel>(returnedObject.ApplicationAttributeItem);
            
			// Map lookup data lists
			model.ApplicationList = Mapper.Map<IEnumerable<ApplicationDC>, List<ApplicationModel>>(returnedObject.ApplicationList);
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(ApplicationAttributeVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            ApplicationAttributeLookupListsCacheObject CachedLists = CacheManager.ApplicationAttributeListCache;

			// Retrieve any cached lists to model
			model.ApplicationList = CachedLists.ApplicationList;
   
        }

        private void MergeNewValuesWithOriginal(ApplicationAttributeModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            ApplicationAttributeModel OriginalValuesFromSession = SessionManager.CurrentApplicationAttribute;

        }
		
		private void SetAccessContext(ApplicationAttributeVM model)
        {
            //Decide on access context
            if (null == model.ApplicationAttributeItem || model.ApplicationAttributeItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = ApplicationAttributeAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = ApplicationAttributeAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(ApplicationAttributeVM model)
        {
            //Compare the ApplicationAttribute to the original session
            if (model.ApplicationAttributeItem.PublicInstancePropertiesEqual(SessionManager.ApplicationAttributeServiceVersion, "RowIdentifier"))
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
