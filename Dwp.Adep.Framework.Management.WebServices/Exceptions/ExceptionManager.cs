using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.IO;
using Dwp.Adep.Framework.Management.WebServices.FaultContracts;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Dwp.Adep.Framework.Management.WebServices.Exceptions
{
    public class ExceptionManager
    {
        public static void ShieldException(Exception e)
        {
            // Publish the exception information
            PublishException(e);

            // If this exception has been caused by an optimistic concurrency error then raise a DataConcurrenct fault
            if (e is OptimisticConcurrencyException)
            {
                throw new FaultException<DataConcurrencyFault>(new DataConcurrencyFault(), "The record has been modified since it was retrieved.");
            }

            // If this exception has been caused by an authorisation failure then raise an AuthorisationFailure fault
            if (e is FailedAuthorisationException)
            {
                if (e.Message == "Failed to find User ID in Active Directory")
                {
                    throw new FaultException<AuthorisationFailureFault>(new AuthorisationFailureFault(), "Failed to find User ID in Active Directory");
                }
                else
                {
                    throw new FaultException<AuthorisationFailureFault>(new AuthorisationFailureFault(), "Authorisation failure");
                }
            }

           
            if (e is UpdateException)
            {
                if (null != e.InnerException && e.InnerException is SqlException)
                {
                    // If this exception has been caused by a data unique key constrain issue then raise a UniqueConstraint fault
                    if (e.InnerException.Message.Contains("UNIQUE KEY constraint"))
                    {
                        throw new FaultException<UniqueConstraintFault>(new UniqueConstraintFault(), "It is not possible to perform this action on the date item.");
                    }
                    // If this exception has been caused by a data referential integrity issue then raise a DataIntegrity fault
                    else if(e.InnerException.Message.Contains("REFERENCE constraint"))
                    {
                        throw new FaultException<DataIntegrityFault>(new DataIntegrityFault(), "It is not possible to perform this action on the data item.");      
                    }
    

                }
            }

            // Throw default fault exception
            throw new FaultException<ServiceErrorFault>(new ServiceErrorFault(), "The service experienced a serious error.");
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