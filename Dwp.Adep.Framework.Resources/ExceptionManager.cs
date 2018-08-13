using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ServiceModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Dwp.Adep.Framework.Resources
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
        /// <returns>String containing message about the error</returns>
        /// <remarks>Error message will be changed if the error is a data integrity or data concurrecny issue so that the user is warned appropriately</remarks>
        public static void HandleException(Exception e)
        {
            // Publish the exception information
            PublishException(e);


        }

        /// <summary>
        /// Publish an exception 
        /// </summary>
        /// <param name="e"></param>
        private static void PublishException(Exception e)
        {
            WriteLog(e.ToString());

            //TODO check if this is working properly before deploying it
            //ExceptionPolicy.HandleException(e, "AdepExceptionPolicy");
        }

        /// <summary>
        /// Writes a log entry 
        /// </summary>
        /// <param name="e"></param>
        private static void WriteLog(string info)
        {
            FileStream fs = File.Open("C:\\Logs\\FrameworkResourcesLog.txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(DateTime.Now + "Dwp.Adep.Framework.Resources ");
            sw.Write(DateTime.Now.ToString() + ": " + info);
            sw.Close();
        }

    }
}