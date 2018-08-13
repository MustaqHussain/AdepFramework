using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class AuthorisationDC
    {
        [DataMember]
        public IEnumerable<string> Roles { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}