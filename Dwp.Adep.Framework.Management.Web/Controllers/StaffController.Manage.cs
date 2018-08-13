using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using AutoMapper;
using Dwp.Adep.Framework.Management.Web.Helpers;
using Dwp.Adep.Framework.Management.Web.ViewModels;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.AdminService;


namespace Dwp.Adep.Framework.Management.Web.Controllers
{
    /// <summary>
    /// Partial class for Staff controller
    /// </summary>
    public partial class StaffController
    {
        #region Manage

        // POST: /Staff/Edit with Manage button submitting
        [CustomAuthorize(Roles = FrameworkRoles.ADMIN + "," + FrameworkRoles.STAFF_MAINTAINANCE)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult ManageStaff(FormCollection collection)
        {
            // Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
                // Test if Select button was clicked...
                if (Key.StartsWith("Search::ManageStaff_"))
                {
                    // Retrieve ID for entity which was selected
                    Guid Value = Guid.Parse(Key.Substring(20));

                    // Store ID for Edit screen
                    SessionManager.StaffCode = Value.ToString();

                    // Save the page where we are so that the Staff Admin page can come back here
                    SessionManager.PageFrom = "StaffSearch";

                    // Call out to Edit screen
                    return RedirectToAction("ApplicationStaffAdmin", "Admin", new { code = Value });

                }
            }

            return View();
        }

        #endregion
    }
}