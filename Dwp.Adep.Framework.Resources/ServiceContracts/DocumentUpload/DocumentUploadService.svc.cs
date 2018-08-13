using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Configuration;

using System.ServiceModel.Activation;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DocumentUploadService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DocumentUploadService : IDocumentUploadService
    {
        public readonly static string TEMP_FILE_LOCATION = "TemporaryFileLocation";

        private ExceptionHandler exceptionHandler;

        #region Constructors

        public DocumentUploadService() : this (new ExceptionHandler())
        {
        }

        public DocumentUploadService(ExceptionHandler exceptionHandler_)
        {
            this.exceptionHandler = exceptionHandler_;
        }

        #endregion

        #region UploadFile

        public bool UploadFile(String document, String replacementFileName)
        {
            if (replacementFileName.IndexOf("\\") == -1)
            {
                return UploadFileToDirectory(document, ConfigurationManager.AppSettings[TEMP_FILE_LOCATION], replacementFileName);
            }
            else
            {
                return UploadFileToDirectory(document,
                    replacementFileName.Substring(0, replacementFileName.LastIndexOf("\\")),
                    replacementFileName.Substring(replacementFileName.LastIndexOf("\\")));
            }
        }

        public bool UploadFileToDirectory(String document, String directoryName, String fileName)
        {

            String replacementFileName = directoryName.EndsWith("\\") ? directoryName + fileName : directoryName + "\\" + fileName;
            //this implementation places the uploaded file in a fileshare as replacementFileName
            if (File.Exists(replacementFileName))
            {
                return false;
            }

            FileStream outstream = null;

            try
            {
                FileInfo fileInfo = new FileInfo(document);

                using (FileStream documentStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    outstream = File.Open(replacementFileName, FileMode.Create, FileAccess.Write);
                    //read from the input stream in 4K chunks and save to output stream
                    const int bufferLen = 4096;
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = documentStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        outstream.Write(buffer, 0, count);
                    }
                }
                return true;
            }
            catch (IOException ex)
            {
                exceptionHandler.ShieldException(ex);
            }
            finally
            {
                if (outstream != null) { outstream.Close(); }              
            }
            return false;
        }

        #endregion

        #region GetFileName

        public bool FileExists(string fileName)
        {          
            return File.Exists(fileName) || (GetFullFileName(fileName) != null);
        }

        public string GetFullFileName(string fileName)
        {
            try
            {

                string fullFilename = ConfigurationManager.AppSettings[TEMP_FILE_LOCATION].ToString() + fileName;

                if (File.Exists(fullFilename))
                    return fullFilename;
                else
                    return null;
            }
            catch (Exception e)
            {
                exceptionHandler.ShieldException(e);
            }
            return null;
        }

        #endregion

        #region DeleteFile

        public bool DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                if (GetFullFileName(fileName) != null)
                    File.Delete(GetFullFileName(fileName));

            }
            catch (Exception e)
            {
                exceptionHandler.ShieldException(e);
            }
            return true;

        }

        public bool DeleteFile(string fileName, string fileLocation)
        {
            try
            {
                string[] files = Directory.GetFiles(fileLocation);
                foreach (string file in files)
                {
                    if (file.Substring(file.LastIndexOf('\\') + 1) == fileName)
                    {
                        File.Delete(file);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                exceptionHandler.ShieldException(e);
            }
            return true;

        }

        #endregion
    }
}
