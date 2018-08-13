using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class ApplicationOrganisationAdminVMDC
    {
        /// <summary>
        /// The organisation being edited
        /// </summary>
        [DataMember]
        public OrganisationDC OrganisationItem { get; set; }

        /// <summary>
        /// Flag indicating if the organisation has child organisations
        /// </summary>
        [DataMember]
        public int MaximumHopsToChildOrganisation { get; set; }

        /// <summary>
        /// The organisation parent
        /// </summary>
        [DataMember]
        public OrganisationDC ParentOrganisation { get; set; }

        /// <summary>
        /// The root node for application
        /// </summary>
        [DataMember]
        public OrganisationDC RootNodeOrganisation { get; set; }

        /// <summary>
        /// The organisations that can be set to parent org
        /// </summary>
        [DataMember]
        public List<OrganisationByTypeVMDC> OrganisationsByTypesList { get; set; }

        /// <summary>
        /// All types in the application organisation type group
        /// </summary>
        [DataMember]
        public List<OrganisationTypeDC> AllTypesForApplication { get; set; }

        /// <summary>
        /// Property for holding messages to be displayed to the user
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}