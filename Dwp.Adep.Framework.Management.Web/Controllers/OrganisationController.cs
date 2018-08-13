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
    public partial class OrganisationController : BaseController
    {
		#region Edit
		
        // GET: /Organisation/Edit
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = SessionManager.OrganisationCode;
			
            OrganisationVM model = new OrganisationVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.OrganisationItem = new OrganisationModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				AdminServiceClient sc = new AdminServiceClient();
				
                try
                {
					// Call service to get Organisation item and any associated lookups    
                    OrganisationVMDC returnedObject = sc.GetOrganisation(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					sc.Close();

                    //Get view model from service
                    model = ConvertOrganisationDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    SessionManager.OrganisationServiceVersion = model.OrganisationItem;
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

        //This method is shared between create and save
        private ActionResult UpdateOrganisation()
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
                    OrganisationDC OrganisationItem = Mapper.Map<OrganisationDC>(model.OrganisationItem);

					OrganisationVMDC returnedObject = null;

                    if (null == model.OrganisationItem.Code || model.OrganisationItem.Code == Guid.Empty)
                    {
						// Call service to create new Organisation item
                        returnedObject = sc.CreateOrganisation(CurrentUser, CurrentUser, appID, "", OrganisationItem);
                    }
                    else
                    {
						// Call service to update Organisation item
                        returnedObject = sc.UpdateOrganisation(CurrentUser, CurrentUser, appID, "", OrganisationItem);
                    }

					// Close service communication
					sc.Close();
					
					// Retrieve item returned by service
                    var createdOrganisation = returnedObject.OrganisationItem;

					// Map data contract to model
                    model.OrganisationItem = Mapper.Map<OrganisationModel>(createdOrganisation);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = OrganisationAccessContext.Edit;

					// Save version of item returned by service into session
                    SessionManager.OrganisationServiceVersion = model.OrganisationItem;
                    SessionManager.CurrentOrganisation = model.OrganisationItem;
                    
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

        // POST: /Organisation/Edit with Exit button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitOrganisation(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                SessionManager.CurrentOrganisation = null;
                SessionManager.OrganisationServiceVersion = null;

                return RedirectToAction("Search", "Organisation");
                
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
            var model = GetUpdatedModel();
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

		#region Search

        // GET: /Organisation/Search
        //This is called when first entering search Organisation screen or when paging
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			AdminServiceClient sc = new AdminServiceClient();
			
			// Create model
			OrganisationSearchVM model = new OrganisationSearchVM();
			
			try
			{
				OrganisationSearchVMDC response = sc.SearchOrganisation(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				sc.Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<OrganisationSearchMatchDC>, List<OrganisationSearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = SessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				SessionManager.OrganisationPageNumber = model.PageNumber;
	        
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

        // POST: /Organisation/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(OrganisationSearchVM model, int page = 1)
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
					SessionManager.OrganisationCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "Organisation", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(OrganisationVM model)
        {
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(OrganisationVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private OrganisationVM GetUpdatedModel()
        {
            OrganisationVM model = new OrganisationVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (SessionManager.CurrentOrganisation != null)
            {
                model.OrganisationItem = SessionManager.CurrentOrganisation;
            }

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

            return model;
        }
		
        private OrganisationVM ConvertOrganisationDC(OrganisationVMDC returnedObject)
        {
            OrganisationVM model = new OrganisationVM();
            
			// Map Organisation Item
			model.OrganisationItem = Mapper.Map<OrganisationDC, OrganisationModel>(returnedObject.OrganisationItem);
            
			// Map lookup data lists
			model.OrganisationTypeList = Mapper.Map<IEnumerable<OrganisationTypeDC>, List<OrganisationTypeModel>>(returnedObject.OrganisationTypeList);
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(OrganisationVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            OrganisationLookupListsCacheObject CachedLists = CacheManager.OrganisationListCache;

			// Retrieve any cached lists to model
			model.OrganisationTypeList = CachedLists.OrganisationTypeList;
   
        }

        private void MergeNewValuesWithOriginal(OrganisationModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            OrganisationModel OriginalValuesFromSession = SessionManager.CurrentOrganisation;

        }
		
		private void SetAccessContext(OrganisationVM model)
        {
            //Decide on access context
            if (null == model.OrganisationItem || model.OrganisationItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = OrganisationAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = OrganisationAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(OrganisationVM model)
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