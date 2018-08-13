using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class ApplicationOrganisationSelectVMDC
    {
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
        /// The application root node for picking organisations.
        /// </summary>
        [DataMember]
        public OrganisationDC RootNodeOrganisation { get; set; }
    }
}