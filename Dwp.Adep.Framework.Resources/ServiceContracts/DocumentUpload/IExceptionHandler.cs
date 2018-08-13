using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    public interface IExceptionHandler
    {
        void PublishException(Exception e);
        void ShieldException(Exception e);
    }
}
