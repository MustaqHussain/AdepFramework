using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload;
using Moq;
using System.IO;
using System.Configuration;

namespace Dwp.Adep.Framework.Resources.Tests
{
    [TestClass]
    public class DocumentUploadTest
    {

        [TestMethod]
        public void TestDocumentUploadCopiesFileAndDeletes()
        {

            IDocumentUploadService svc = new DocumentUploadService();

            // Try to create a real file
            string testFileName = "TestFile.txt";
            String directory = ConfigurationManager.AppSettings[DocumentUploadService.TEMP_FILE_LOCATION];

           
            string testFileDuplicate = testFileName + ".duplicate";

            try
            {
                FileInfo fileInfo = new FileInfo(directory + (directory.EndsWith("\\") ? testFileName : "\\" + testFileName));

                String contents = DateTime.Today.ToLongDateString();

                using (StreamWriter file = fileInfo.CreateText())
                {
                    file.WriteLine(contents);
                }

                    // Should return false for existing file                
                Assert.IsFalse(svc.UploadFile(directory + (directory.EndsWith("\\") ? testFileName : "\\" + testFileName), testFileName));
                Assert.IsFalse(svc.UploadFile(directory + (directory.EndsWith("\\") ? testFileName : "\\" + testFileName), directory + testFileName));

                Assert.IsTrue(svc.UploadFile(directory + (directory.EndsWith("\\") ? testFileName : "\\" + testFileName), testFileDuplicate), String.Format("Failed to upload file = check {0} does not already exist", directory + testFileDuplicate));
                

                FileInfo duplicate = new FileInfo(directory + (directory.EndsWith("\\") ? testFileDuplicate : "\\" + testFileDuplicate));

                using (StreamReader reader = duplicate.OpenText())
                {
                    string s = reader.ReadLine();
                    Assert.IsNotNull(s);
                    Assert.AreEqual(s, contents);
                }

            }
            catch (IOException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                svc.DeleteFile(testFileName, directory);
                svc.DeleteFile(testFileDuplicate, directory);

                Assert.IsFalse(File.Exists(directory + testFileName));               
                Assert.IsFalse(File.Exists(directory + testFileDuplicate));
            }
        }

    }
}
