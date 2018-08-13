using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.DataContracts
{
    [DataContract]
    public class DocContent
    {
        [DataMember]
        public string ContentKey { get; set; }

        [DataMember]
        public string ContentText { get; set; }
    }
}