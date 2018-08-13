using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Management.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace Dwp.Adep.Framework.Management.Web.ViewModels
{
    public class StaffApplicationAdminVM
    {
        /// <summary>
        /// The staff member who is being administered application permissions
        /// </summary>
        public StaffModel StaffItem { get; set; }

        /// <summary>
        /// List of applications that the administrator can administer
        /// </summary>
        public List<ApplicationModel> ApplicationList { get; set; }

        [Display(Name="Selected Application")]
        public Guid? SelectedApplicationCode { get; set; }

        /// <summary>
        /// Flattened list of available organisations for use with application, starting with the root node
        /// </summary>
        public List<OrganisationByTypeVM> OrganisationsByTypesList { get; set; }

        /// <summary>
        /// List of applicationattributes aggregated with staff attributes
        /// </summary>
        public List<StaffApplicationAttributeVM> StaffApplicationAttributeList { get; set; }

        /// <summary>
        /// List of Roles for the application that the staff member has
        /// </summary>
        public List<string> RoleList { get; set; }

        /// <summary>
        /// List of the staff members current organisations for this application.
        /// </summary>
        public List<StaffOrganisationModel> StaffOrganisationList { get; set; }

        /// <summary>
        /// The application root node for picking organisations.
        /// </summary>
        public OrganisationModel RootNodeOrganisation { get; set; }

        /// <summary>
        /// Flag used to mark the view values as having changed
        /// </summary>
        public bool IsViewDirty { get; set; }

        /// <summary>
        /// Flag used to ensure the user confirms a exit action
        /// </summary>
        public string IsExitConfirmed { get; set; }

        /// <summary>
        /// Property for holding messages to be displayed to the user
        /// </summary>
        public string Message { get; set; }
    }
}