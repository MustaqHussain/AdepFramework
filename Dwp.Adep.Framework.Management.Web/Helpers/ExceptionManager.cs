using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using Dwp.Adep.Framework.Management.Web.AdminService;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Dwp.Adep.Framework.Management.Web.Helpers
{
    /// <summary>
    /// Class to centrally manage exceptions
    /// </summary>
    public class ExceptionManager
    {

        /// <summary>
        /// Handle exception
        /// </summary>
        /// <param name="e">Exception to handle</param>
        /// <param name="serviceObject">Service communication object to abort if not already closed</param>
        /// <returns>String containing message about the error</returns>
        /// <remarks>Error message will be changed if the error is a data integrity or data concurrecny issue so that the user is warned appropriately</remarks>
        public static string HandleException(Exception e, ICommunicationObject serviceObject)
        {
            // If service isn't already closed then abort communication
            if (null != serviceObject && serviceObject.State != CommunicationState.Closed)
            {
                // Abort the service
                serviceObject.Abort();
            }

            // Publish the exception information
            PublishException(e);

            // Message to pass back to user
            string returnMessage = null;

            // Data integrity problem
            if (e is FaultException<DataIntegrityFault>)
            {
                // Set messaage
                returnMessage = FixedResources.MESSAGE_DATACANNOTDELETE;
            }
            // Data concurrency problem
			else if (e is FaultException<DataConcurrencyFault>)
            {
                // Set messaage
                returnMessage = FixedResources.MESSAGE_DATACONCURRENCYFAILURE;
            }
            // Data unique constraint problem
            else if (e is FaultException<UniqueConstraintFault>)
            {
                // Set message
                returnMessage = FixedResources.MESSAGE_UNIQUEKEYCONSTRAINT;
            }

            else if (e is FaultException)
            {
                returnMessage = FixedResources.MESSAGE_DATAEXCEPTION;
            }
            // General problem
            else
            {
                // Rethrow as this is an unexpected problem
                throw e;
            }

            return returnMessage;

        }

        /// <summary>
        /// Publish an exception 
        /// </summary>
        /// <param name="e"></param>
        public static void PublishException(Exception e)
        {
            bool rethrow = ExceptionPolicy.HandleException(e, "AdepExceptionPolicy");
        }

    }
}