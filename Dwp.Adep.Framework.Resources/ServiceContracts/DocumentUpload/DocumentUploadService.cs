using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Configuration;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    public class DocumentUploadService : IDocumentUploadService
    {

        public readonly static string TEMP_FILE_LOCATION = "TemporaryFileLocation";

        #region UploadFile
    
        public bool UploadFile(Stream documentStream, String replacementFileName)
        {
            if (replacementFileName.IndexOf("\\") == -1)
            {
                return UploadFile(documentStream, ConfigurationManager.AppSettings[TEMP_FILE_LOCATION], replacementFileName);
            }
            else
            {
                return UploadFile(documentStream, 
                    replacementFileName.Substring(0, replacementFileName.LastIndexOf("\\")), 
                    replacementFileName.Substring(replacementFileName.LastIndexOf("\\")));
            }
        }

        public bool UploadFile(Stream documentStream, String directoryName, String fileName)
        {
        
            String replacementFileName = directoryName.EndsWith("\\") ? directoryName+fileName : directoryName+"\\"+fileName;
            //this implementation places the uploaded file in a fileshare as replacementFileName
            if (File.Exists(replacementFileName))
            {
                return false;
            }

            FileStream outstream = null;

            try
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

                return true;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {

                if (outstream != null) { outstream.Close(); }
                if (documentStream != null) { documentStream.Close(); }

            }
        }

        #endregion

        #region GetFileName

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName) || (GetFullFileName(fileName) != null);
        }

        public string GetFullFileName(string fileName)
        {
            string fullFilename = ConfigurationManager.AppSettings[TEMP_FILE_LOCATION].ToString() + fileName;

            if (File.Exists(fullFilename))
                return fullFilename;
            else
                return null;
        }

        #endregion

        #region DeleteFile

        public bool DeleteFile(string fileName)
        {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                if (GetFullFileName(fileName) != null)
                    File.Delete(GetFullFileName(fileName));

                return true;

        }

        public bool DeleteFile(string fileName, string fileLocation)
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

                return true;
          
        }

        #endregion
    }
    
}
