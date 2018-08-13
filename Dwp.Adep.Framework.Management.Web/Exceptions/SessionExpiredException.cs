using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Management.Web.Exceptions
{
    [Serializable]
    public class SessionExpiredException : Exception
    {
        public SessionExpiredException() { }
        public SessionExpiredException(string message) : base(message) { }
        public SessionExpiredException(string message, Exception inner) : base(message, inner) { }
        protected SessionExpiredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}