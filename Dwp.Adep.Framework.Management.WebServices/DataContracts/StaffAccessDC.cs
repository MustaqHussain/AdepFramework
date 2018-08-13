using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class StaffAccessDC
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ApplicationName { get; set; }

        [DataMember]
        public bool IsSpecificOrganisationAccessRequired { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string OrganisationName { get; set; }

        [DataMember]
        public int OrganisationID { get; set; }

    }
}