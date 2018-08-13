using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ServiceModel;
using System.IO;
using Dwp.Adep.Framework.Management.WebServices.FaultContracts;

namespace Dwp.Adep.Framework.Management.WebServices.Exceptions
{

    [System.Serializable]
    public class FailedAuthorisationException : Exception
    {
        public FailedAuthorisationException() { }
        public FailedAuthorisationException(string message) : base(message) { }
        public FailedAuthorisationException(string message, System.Exception inner) : base(message, inner) { }
        protected FailedAuthorisationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}