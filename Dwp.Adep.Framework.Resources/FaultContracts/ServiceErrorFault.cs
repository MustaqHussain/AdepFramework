using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.FaultContracts
{
    [DataContract]
    public class ServiceErrorFault
    {
        [DataMember]
        public string Operation { get; set; }
        
        [DataMember]
        public string ProblemType { get; set; }

    }
}