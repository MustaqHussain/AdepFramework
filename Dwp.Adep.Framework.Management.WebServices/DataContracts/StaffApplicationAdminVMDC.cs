using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class StaffApplicationAdminVMDC
    {
        /// <summary>
        /// The staff member who is being administered application permissions
        /// </summary>
        [DataMember]
        public StaffDC StaffItem { get; set; }

        /// <summary>
        /// List of applications that the administrator can administer
        /// </summary>
        [DataMember]
        public List<ApplicationDC> ApplicationList { get; set; }

        [DataMember]
        public Guid SelectedApplicationCode { get; set; }

        /// <summary>
        /// Flattened list of available organisations for use with application, starting with the root node
        /// </summary>
        [DataMember]
        public List<OrganisationByTypeVMDC> OrganisationsByTypesList { get; set; }

        /// <summary>
        /// List of applicationattributes aggregated with staff attributes
        /// </summary>
        [DataMember]
        public List<StaffApplicationAttributeVMDC> StaffApplicationAttributeList { get; set; }

        /// <summary>
        /// List of Roles for the application that the staff member has
        /// </summary>
        [DataMember]
        public List<string> RoleList { get; set; }

        /// <summary>
        /// List of the staff members current organisations for this application.
        /// </summary>
        [DataMember]
        public List<StaffOrganisationDC> StaffOrganisationList { get; set; }

        /// <summary>
        /// The application root node for picking organisations.
        /// </summary>
        [DataMember]
        public OrganisationDC RootNodeOrganisation { get; set; }
    }
}