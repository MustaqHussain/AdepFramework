using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using AutoMapper;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dwp.Adep.Framework.Management.Web.Helpers;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.Controllers;
using Dwp.Adep.Framework.Management.Web.AuthorisationService;
using System.Web.Security;
using System.Globalization;
using Dwp.Adep.Framework.Management.Web.AdminService;

namespace Dwp.Adep.Framework.Management.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            AuthorisationServiceClient sc = new AuthorisationServiceClient();

            try
            {
                Mapper.CreateMap<StaffAccessDC, StaffAccessModel>();
                Mapper.CreateMap<StaffAccessModel, StaffAccessDC>();
                               
                string[] adGroups = Roles.GetRolesForUser();

#if DEBUG
                //####################################################TEMPORALY HARD CODED FOR DEVELOPMENT#################################################
                //#                                               ADCSEC GROUPS CANNOT BE ALTERED                                                         #
                adGroups = adGroups.Concat(new List<string>() { "DWP-FW-ADMIN", "DWP-FW-APPLICATION", "DWP-UCB-APPLICATION" }).ToArray();                   //#
                //#                                                                                                                                       #
                //#########################################################################################################################################
#endif
                List<StaffAccessModel> Destinations = new List<StaffAccessModel>();

                // If this is an authorised user then find out what Applications they have access to
                Guid outGuid;
                if (Guid.TryParse(CurrentUser, out outGuid))
                {
                    List<StaffAccessDC> staffAccessList = sc.GetUserApplicationInfo(CurrentUser, CurrentUser, appID, "", CurrentUser, adGroups).ToList();
                    Destinations = Mapper.Map<List<StaffAccessModel>>(staffAccessList);

                    // Save staff access list
                    SessionManager.StaffAccessList = Destinations;
                }

                return View(Destinations);
            }
            catch (Exception e)
            {
                // Handle exception
                ExceptionManager.HandleException(e, sc);

                return null;
            }

        }

        // POST: /Home/Index
        [HttpPost]
        [CustomAuthorize(Roles = FrameworkRoles.APPLICATION)]
        public ActionResult Index(string code, FormCollection collection)
        {
            foreach (string Key in Request.Form.Keys)
            {
                if (Key.StartsWith("SelectButton_"))
                {
                    int Value = int.Parse(Key.Substring(13));

                    // Retrieve the selected application information from session
                    StaffAccessModel staffAccess = SessionManager.StaffAccessList.Find(x => x.ID == Value);

                    // If the selected application has organisation specific context and an organisation has been supplied then make 
                    // the user's current organisation to this organisation
                    if (staffAccess.OrganisationID != 0)
                    {
                        // Create an instance of teh service
                        AdminServiceClient sc = new AdminServiceClient();

                        try
                        {
                            // Update the current organisatino for this user for the specified application
                            sc.UpdateStaffCurrentOrganisation(CurrentUser, CurrentUser, appID, "", staffAccess.ApplicationName, staffAccess.OrganisationID);
                        }
                        catch (Exception e)
                        {
                            // Handle exception
                            ExceptionManager.HandleException(e, sc);

                            return null;
                        }
                    }

                    // redirect the user to the home page of the selected application
                    return Redirect(staffAccess.Location);
                }
            }

            return View();
        }

        public ActionResult About()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case ("cy-GB"):
                    return View("About_CY");
                case ("en-GB"):
                default:
                    return View();
            }
        }

        public ActionResult Accessibility()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case ("cy-GB"):
                    return View("Accessibility_CY");
                case ("en-GB"):
                default:
                    return View();
            }
        }

        public ActionResult ChangeSite()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case ("cy-GB"):
                    return View("ChangeSite_CY");
                case ("en-GB"):
                default:
                    return View();
            }
        }

        public ActionResult Help()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case ("cy-GB"):
                    return View("Help_CY");
                case ("en-GB"):
                default:
                    return View();
            }
        }

        public ActionResult Feedback()
        {
            switch (CultureInfo.CurrentCulture.Name)
            {
                case ("cy-GB"):
                    return View("Feedback_CY");
                case ("en-GB"):
                default:
                    return View();
            }
        }
    }
}
