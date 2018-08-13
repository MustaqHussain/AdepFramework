using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Resources;
using Dwp.Adep.Framework.Resources.FaultContracts;
using System.ServiceModel;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    public class ExceptionHandler : IExceptionHandler
    {
        public void PublishException(Exception e)
        {
            /*log erorr locally */
            
            Exception custom = new Exception(e.Message);
            ExceptionManager.HandleException(custom);

        }

        public void ShieldException(Exception e)
        {
            PublishException(e);
           
            ServiceErrorFault fault = new ServiceErrorFault();
            fault.Operation = "Document Upload Service";
            fault.ProblemType = e.Message;

            throw new FaultException<ServiceErrorFault>(fault);
        }
    }
}