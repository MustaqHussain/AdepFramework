using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Dwp.Adep.Framework.Resources.DataContracts
{
      [DataContract]
    public class ADGroup
    {
         [DataMember]
        public string DisplayName { get; set; }

         [DataMember]
         public string SamAccountName { get; set; }
    }
}