using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class OrganisationByTypeVMDC
    {
        [DataMember]
        public string SelectedOrganisationCode { get; set; }
        [DataMember]
        public OrganisationTypeDC OrganisationTypeItem { get; set; }
        [DataMember]
        public List<OrganisationDC> OrganisationList { get; set; }
    }
}