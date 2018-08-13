using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class StaffApplicationAttributeVMDC
    {
        [DataMember]
        public ApplicationAttributeDC ApplicationAttributeItem { get; set; }
        [DataMember]
        public StaffAttributesDC StaffAttributeItem { get; set; }
    }
}