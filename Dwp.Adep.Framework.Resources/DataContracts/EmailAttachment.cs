using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.DataContracts
{
    [DataContract]
    public class EmailAttachment
    {
        [DataMember]
        public byte[] Data { get; set; }

        [DataMember]
        public string Name { get; set; }

    }
}