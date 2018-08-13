using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    [Serializable]
    public class UploadConfigurationException : Exception
    {
        protected UploadConfigurationException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }

        public UploadConfigurationException(String message) : base(message) {

        }       
    }
}
