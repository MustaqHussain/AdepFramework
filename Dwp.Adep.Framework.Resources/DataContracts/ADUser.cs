using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.DataContracts
{
    [DataContract]
    public class ADUser
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public string ProfilePath { get; set; }

        [DataMember]
        public string DistinguishedName { get; set; }

        [DataMember]
        public string GivenName { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string UserPrincipalName { get; set; }
        
        [DataMember]
        public string DNSHostname { get; set; }

        [DataMember]
        public string SN { get; set; }

        [DataMember]
        public string OfficeLocation { get; set; }

        [DataMember]
        public string EmployeeId { get; set; }
        

        [DataMember]
        public List<ADGroup> Groups { get; set; }
    }
}