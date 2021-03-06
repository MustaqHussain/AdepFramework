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
    public partial class CountryController : BaseController
    {
		#region Edit
		
        // GET: /Country/Edit
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Edit()
        {
			// Retrieve ID from session
			string code = SessionManager.CountryCode;
			
            CountryVM model = new CountryVM();
			
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {
                //If session has lists then use them
                RepopulateListsFromCacheSession(model);

                //Assume we are in create mode as no code passed
                model.CountryItem = new CountryModel(){IsActive = true};
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
				// Create service instance
				AdminServiceClient sc = new AdminServiceClient();
				
                try
                {
					// Call service to get Country item and any associated lookups    
                    CountryVMDC returnedObject = sc.GetCountry(CurrentUser, CurrentUser, appID, "", code);

					// Close service communication
					sc.Close();

                    //Get view model from service
                    model = ConvertCountryDC(returnedObject);

                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    //Store the service version
                    SessionManager.CountryServiceVersion = model.CountryItem;
                }
				catch (Exception e)
				{
					// Handle the exception
					string message = ExceptionManager.HandleException(e, sc);
					model.Message = message;
					
					return View(model);
				}
            }

            //Adds current retrieved Country to session
            SessionManager.CurrentCountry = model.CountryItem;
            SetAccessContext(model);
            
            return View(model);
        }    
		
		#endregion
		
		#region Create/Update

        // POST: /Country/Edit with Create button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult CreateCountry(FormCollection collection)
        {
            return UpdateCountry();
        }

        // POST: /Country/Edit with Save button submitting
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveCountry(FormCollection collection)
        {
            return UpdateCountry();
        }

        //This method is shared between create and save
        private ActionResult UpdateCountry()
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
                    CountryDC CountryItem = Mapper.Map<CountryDC>(model.CountryItem);

					CountryVMDC returnedObject = null;

                    if (null == model.CountryItem.Code || model.CountryItem.Code == Guid.Empty)
                    {
						// Call service to create new Country item
                        returnedObject = sc.CreateCountry(CurrentUser, CurrentUser, appID, "", CountryItem);
                    }
                    else
                    {
						// Call service to update Country item
                        returnedObject = sc.UpdateCountry(CurrentUser, CurrentUser, appID, "", CountryItem);
                    }

					// Close service communication
					sc.Close();
					
					// Retrieve item returned by service
                    var createdCountry = returnedObject.CountryItem;

					// Map data contract to model
                    model.CountryItem = Mapper.Map<CountryModel>(createdCountry);
					
                    //After creation some of the fields are display only so we need the resolved look up nmames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

					// Set access context to Edit mode
                    model.AccessContext = CountryAccessContext.Edit;

					// Save version of item returned by service into session
                    SessionManager.CountryServiceVersion = model.CountryItem;
                    SessionManager.CurrentCountry = model.CountryItem;
                    
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

        // POST: /Country/Edit with Exit button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitCountry(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";
                
                //remove the current values from session
                SessionManager.CurrentCountry = null;
                SessionManager.CountryServiceVersion = null;

                return RedirectToAction("Search", "Country");
                
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

        // POST: /Country/Edit with New button submitting
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewCountry(FormCollection collection)
        {
            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);
            
            //Clear Down Session
			SessionManager.CountryCode = null;
            SessionManager.CurrentCountry = null;
            SessionManager.CountryServiceVersion = null;
            
            //Go to the Edit Screen
            return RedirectToAction("Edit", "Country");
        }
		
		#endregion

		#region Search

        // GET: /Country/Search
        //This is called when first entering search Country screen or when paging
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {   
			// Create service instance
			AdminServiceClient sc = new AdminServiceClient();
			
			// Create model
			CountrySearchVM model = new CountrySearchVM();
			
			try
			{
				CountrySearchVMDC response = sc.SearchCountry(CurrentUser, CurrentUser, appID, "", null, page, PageSize, true);

				// Close service communication
				sc.Close();

				//Map response back to view model
				model.MatchList = Mapper.Map<IEnumerable<CountrySearchMatchDC>, List<CountrySearchMatchModel>>(response.MatchList);

				// Set paging values
				model.TotalRows = response.RecordCount;
				model.PageSize = SessionManager.PageSize;
				model.PageNumber = page;
				
				// Store the page number we were on
				SessionManager.CountryPageNumber = model.PageNumber;
	        
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

        // POST: /Country/Search
        //This is called when clicking search button
		[CustomAuthorize(Roles = FrameworkRoles.ADMIN)]
		[HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult SearchPost(CountrySearchVM model, int page = 1)
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
					SessionManager.CountryCode = Value.ToString();
					
					// Call out to Edit screen
                    return RedirectToAction("Edit", "Country", new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        #endregion
		
		#region Private methods
		
        private void SetFlagsFalse(CountryVM model)
        {
            model.IsExitConfirmed = "False";
            model.IsNewConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsNewConfirmed");
        }

        private void ResolveFieldCodesToFieldNamesUsingLists(CountryVM model)
        {
			//TODO:
        }

        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private CountryVM GetUpdatedModel()
        {
            CountryVM model = new CountryVM();
            RepopulateListsFromCacheSession(model);
            model.Message = "";

            if (SessionManager.CurrentCountry != null)
            {
                model.CountryItem = SessionManager.CurrentCountry;
            }

            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            bool result = TryUpdateModel(model);//This also validates and sets ModelState
            //*******************************************************************************************************************************
            if (SessionManager.CurrentCountry != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session
                MergeNewValuesWithOriginal(model.CountryItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);

            return model;
        }
		
        private CountryVM ConvertCountryDC(CountryVMDC returnedObject)
        {
            CountryVM model = new CountryVM();
            
			// Map Country Item
			model.CountryItem = Mapper.Map<CountryDC, CountryModel>(returnedObject.CountryItem);
            
			// Map lookup data lists
        
            return model;
        }
		
		private void RepopulateListsFromCacheSession(CountryVM model)
        {
			// Populate cached lists if they are empty. Will invoke service call
            CountryLookupListsCacheObject CachedLists = CacheManager.CountryListCache;

			// Retrieve any cached lists to model
   
        }

        private void MergeNewValuesWithOriginal(CountryModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            CountryModel OriginalValuesFromSession = SessionManager.CurrentCountry;

        }
		
		private void SetAccessContext(CountryVM model)
        {
            //Decide on access context
            if (null == model.CountryItem || model.CountryItem.Code == Guid.Empty)
            {
				// Create context
                model.AccessContext = CountryAccessContext.Create;
            }
            else
            {
				// Edit context
                model.AccessContext = CountryAccessContext.Edit;
            }
        }
		
		private void DetermineIsDirty(CountryVM model)
        {
            //Compare the Country to the original session
            if (model.CountryItem.PublicInstancePropertiesEqual(SessionManager.CountryServiceVersion, "RowIdentifier"))
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
