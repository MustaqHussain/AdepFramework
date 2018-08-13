using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    public partial class OrganisationDC
    {
        [DataMember]
        public Guid ParentID { get; set; }
    }
}