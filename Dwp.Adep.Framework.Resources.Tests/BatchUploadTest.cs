using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload;
using Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch;
using Dwp.Adep.Framework.Resources.DataContracts;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Data.Common;
using Moq;
using Dwp.Adep.Framework.Resources.AdminService;

using System.Data.SqlClient;

namespace Dwp.Adep.Framework.EmailService.Tests
{
    [TestClass]
    public class BatchUploadTest
    {
        private readonly string user = "test";
        private Guid appId = Guid.NewGuid();

        // TODO don't have these!! Get from config
        private readonly string dbConnectionString = "Password=SqlAdmin$1;Persist Security Info=True;User ID=sa;Initial Catalog=SasaundeTest;Data Source=10.21.34.166\\ADEPDEV";
        private readonly string tempTableName = "NinoPostcodeStaging";
        private readonly string startingFileName = @"C:\Users\sasaunde\Documents\PostcodeData.xls";
        private readonly string replacementFileName = @"C:\Temp\PostcodeData.xls";

        bool working = true;
        void WaitForStopWorking()
        {
            
            while (working == true)
            {
                Thread.Sleep(5 * 1000);
            }
        }

        void MockException()
        {
            throw new AssertFailedException("Service Fault Exception! Callback method has no details");
        }

        private BatchUploadService GetService(Mock<IAdminService> adminService)
        {
               
                QueueManager qMgr = new QueueManager(adminService.Object);
               
                var mockHandler = new Mock<IExceptionHandler>();
                //mockHandler.Setup(x => x.ShieldException(It.IsAny<Exception>())).Callback(MockException);

                var mockFactory = new Mock<IDataStoreFactory>();
                var mockDataStore = new Mock<IDataStore>();
                mockFactory.Setup(x => x.CreateDataStore())
                    .Returns(mockDataStore.Object);

                BatchUploadService svc = new BatchUploadService(new DocumentUploadService(), qMgr, mockHandler.Object, mockFactory.Object);

                return svc;
        }

        List<UploadQueueVMDC> uploadRecords = new List<UploadQueueVMDC>();
        List<UploadErrorLogVMDC> errors = new List<UploadErrorLogVMDC>(5);
        int i = 0;
       
        void AddToList()
        {           
            uploadRecords[i].UploadQueueItem.Status = "R";
        }

        UploadQueueVMDC GetUploadRecord()
        {
            UploadQueueVMDC uploadRecord  = new UploadQueueVMDC();
            uploadRecord.UploadQueueItem = new UploadQueueDC();
            uploadRecord.UploadQueueItem.Code = Guid.NewGuid();
            
            uploadRecords.Add(uploadRecord);

            return uploadRecord;
        }

        void SetErrorMessage()
        {
            UploadErrorLogVMDC errorRecord = new UploadErrorLogVMDC();
            errorRecord.UploadErrorLogItem = new UploadErrorLogDC();
            errorRecord.UploadErrorLogItem.ErrorMessage = "Test Error Message";
            errors.Add(errorRecord);

        }

        void QueueUploadRecord()
        {
            uploadRecords[i].UploadQueueItem.Status = "Q";
        }

        void Break()
        {
            throw new Exception("Uh oh");
        }

        [TestMethod]
        public void TestFakeStoredProc()
        {
            Reset();
            UploadTestFile();

            var adminService = new Mock<IAdminService>();
            UploadQueueVMDC uploadRecord = GetUploadRecord();
            adminService.Setup(x => x.CreateUploadQueue(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<UploadQueueDC>())).Returns(uploadRecord);
            adminService.Setup(x => x.GetUploadQueue(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(uploadRecord);
            
            QueueManager qMgr = new QueueManager(adminService.Object);

            var mockHandler = new Mock<IExceptionHandler>();

            BatchUploadService svc = new BatchUploadService(new DocumentUploadService(), qMgr, mockHandler.Object, new DataStoreFactory());

            BatchDetails batchDetails = new BatchDetails(dbConnectionString, replacementFileName, tempTableName, appId, "NoSuchProc", user);

            String guid = svc.QueueBatch(batchDetails);

            Assert.IsTrue(svc.CheckStatus(new Guid(guid), user) == UploadStatus.Running);

            Thread.Sleep(1 * 1000); // Give the thread a chance to run

            // Ensure one error logged           
            adminService.Verify(x => x.CreateUploadErrorLog(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<UploadErrorLogDC>()), Times.Once());

            // Ensure queue emptied
            Assert.AreEqual(0, svc.GetQueueSize());

            // Ensure test file deleted
            CheckTestFileDeleted();

        }

        //[TestMethod]
        public void IntegrationTestRealStoredProc()
        {
            Reset();
            UploadTestFile();

            var adminService = new Mock<IAdminService>();
            UploadQueueVMDC uploadRecord = GetUploadRecord();
            adminService.Setup(x => x.CreateUploadQueue(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<UploadQueueDC>())).Returns(uploadRecord);
            adminService.Setup(x => x.GetUploadQueue(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>())).Returns(uploadRecord);

            QueueManager qMgr = new QueueManager(adminService.Object);

            var mockHandler = new Mock<IExceptionHandler>();

            BatchUploadService svc = new BatchUploadService(new DocumentUploadService(), qMgr, mockHandler.Object, new DataStoreFactory());

            BatchDetails batchDetails = new BatchDetails(dbConnectionString, replacementFileName, tempTableName, appId, "sp_TestStuff", user);

            String guid = svc.QueueBatch(batchDetails);

            Assert.IsTrue(svc.CheckStatus(new Guid(guid), user) == UploadStatus.Running);

            Thread.Sleep(1 * 1000); // Give the thread a chance to run

            // Ensure no errors logged           
            adminService.Verify(x => x.CreateUploadErrorLog(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(), It.IsAny<UploadErrorLogDC>()), Times.Never());

            // Ensure updated with status = PROCESSED
            adminService.Verify(x => x.UpdateUploadQueue(user, user, It.IsAny<String>(), "", It.Is<UploadQueueDC>(c => c.Status == "P")), Times.Once());

            // Ensure queue emptied
            Assert.AreEqual(0, svc.GetQueueSize());

            // Ensure test file deleted
            CheckTestFileDeleted();

        }

        [TestMethod]
        public void TestQueueOfThreadsWithFailure()
        {
            Reset();
            var adminService = new Mock<IAdminService>();//new AdminService.AdminServiceClient();

            adminService.Setup(x => x.GetUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Callback(QueueUploadRecord);
            adminService.Setup(x => x.CreateUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<UploadQueueDC>())).Callback(AddToList);
            adminService.Setup(x => x.CreateUploadErrorLog(user, user, It.IsAny<String>(), "", It.IsAny<UploadErrorLogDC>())).Callback(SetErrorMessage);
            BatchUploadService svc = GetService(adminService);

            // Start a number of batch threads
            Guid[] guids = new Guid[5];

            try
            {
                for (i = 0; i < 5; i++)
                {
                    String tempFileName = "C:\\Temp\\Test" + i + "File.xls";
                    UploadTestFile(tempFileName);
                    // Mock the admin service per call
                    UploadQueueVMDC record = GetUploadRecord();
                    adminService.Setup(x => x.CreateUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<UploadQueueDC>())).Returns(record);
                    adminService.Setup(x => x.GetUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Returns(record);

                    BatchDetails batchDetails = new BatchDetails(dbConnectionString, tempFileName, tempTableName, appId, null, user);
                    batchDetails.PostProcessing = Break;
                    guids[i] = new Guid(svc.QueueBatch(batchDetails));
                    Assert.IsTrue(svc.CheckStatus(guids[i], user) == UploadStatus.Running);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
           

            Thread.Sleep(5 * 1000); // Allow a bit of time for threads to all finish

            Assert.AreEqual(errors.Count, 5, "Wrong number of errors stored in queue");

            for (int count = 0; count < 5; count++)
            {
                // OK all the errors are the same but we are at least checking that there are 5
                adminService.Setup(x => x.GetUploadErrorLog(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Returns(errors[count]);

                Assert.AreEqual(svc.GetErrorMessage(guids[count], user), "Test Error Message");

                CheckTestFileDeleted("C:\\Temp\\Test" + count + "File.xls");

            }

            // Ensure all threads removed from queue
            Assert.AreEqual(svc.GetQueueSize(), 0);

        }

        [TestMethod]
        public void TestNoTempFile()
        {
            try
            {
                
                var adminService = new Mock<IAdminService>();
                UploadQueueVMDC uploadRecord = new UploadQueueVMDC();
                uploadRecord.UploadQueueItem = new UploadQueueDC();
                uploadRecord.UploadQueueItem.Status = "C";
                adminService.Setup(x => x.GetUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Returns(uploadRecord);
                adminService.Setup(x => x.CreateUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<UploadQueueDC>())).Returns(uploadRecord);

                var mockHandler = new Mock<IExceptionHandler>();

                var mockBatchUploadFactory = new Mock<IDataStoreFactory>();

                BatchUploadService svc = new BatchUploadService(new DocumentUploadService(), new QueueManager(adminService.Object), mockHandler.Object, mockBatchUploadFactory.Object);

                BatchDetails batchDetails = new BatchDetails(dbConnectionString, "NoSuchFile", tempTableName, appId, null, user);
                
                String guid = svc.QueueBatch(batchDetails);
              
                // Ensure one error logged
                mockHandler.Verify(x => x.ShieldException(It.IsAny<UploadConfigurationException>()), Times.Once());

                // Ensure queue emptied
                Assert.AreEqual(0, svc.GetQueueSize());
            }
            catch (Exception e)
            {
                String sr = e.Message;
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void TestQueueOfThreads()
        {
            Reset();
            working = true;

            var adminService = new Mock<IAdminService>();//new AdminService.AdminServiceClient();

            adminService.Setup(x => x.GetUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Callback(QueueUploadRecord);
            adminService.Setup(x => x.CreateUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<UploadQueueDC>())).Callback(AddToList);
            adminService.Setup(x => x.CreateUploadErrorLog(user, user, It.IsAny<String>(), "", It.IsAny<UploadErrorLogDC>())).Callback(SetErrorMessage);
            BatchUploadService svc = GetService(adminService);

            // Start a number of batch threads, add new DoWork method to each that will wait until we set a local variable to TRUE
            Guid[] guids = new Guid[5];

            try
            {
                for (i = 0; i < 5; i++)
                {
                    String tempFileName = "C:\\Temp\\Test" + i + "File.xls";
                    UploadTestFile(tempFileName);
                    // Mock the admin service per call
                    UploadQueueVMDC record = GetUploadRecord();
                    adminService.Setup(x => x.CreateUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<UploadQueueDC>())).Returns(record);
                    adminService.Setup(x => x.GetUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Returns(record);

                    BatchDetails batchDetails = new BatchDetails(dbConnectionString, tempFileName, tempTableName, appId, null, user);
                    batchDetails.PostProcessing = WaitForStopWorking;
                    guids[i] = new Guid(svc.QueueBatch(batchDetails));
                    Assert.IsTrue(svc.CheckStatus(guids[i], user) == UploadStatus.Running);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                working = false;
            }

            Thread.Sleep(6 * 1000); // Allow time for all waiting threads to wake up and find out that they can stop working

            for (int count = 0; count < 5; count++)
            {

                CheckTestFileDeleted("C:\\Temp\\Test" + count + "File.xls");

            }

            // Ensure all threads removed from queue
            Assert.AreEqual(svc.GetQueueSize(), 0);

        }

        private void CheckTestFileDeleted()
        {
            CheckTestFileDeleted(replacementFileName);
        }

        private void CheckTestFileDeleted(String fileName)
        {
            Assert.IsFalse(File.Exists(fileName), "Temporary file was not deleted");
        }

        private void UploadTestFile()
        {
            UploadTestFile(replacementFileName);
        }

        private void UploadTestFile(String tempFileName)
        {
            DocumentUploadService docUpload = new DocumentUploadService();

            docUpload.UploadFile(startingFileName, tempFileName);
                
        }

        private void Reset()
        {
            errors.Clear();
            i = 0;
            uploadRecords.Clear();
        }

        [TestMethod]
        public void TestBatchUpload()
        {
            try
            {
                Reset();
                UploadTestFile();

                var adminService = new Mock<IAdminService>();
                UploadQueueVMDC uploadRecord = new UploadQueueVMDC();
                uploadRecord.UploadQueueItem = new UploadQueueDC();
                uploadRecord.UploadQueueItem.Status = "C";
                adminService.Setup(x => x.GetUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<String>())).Returns(uploadRecord);
                adminService.Setup(x => x.CreateUploadQueue(user, user, It.IsAny<String>(), "", It.IsAny<UploadQueueDC>())).Returns(uploadRecord);

              
                BatchUploadService svc = GetService(adminService);

                BatchDetails batchDetails = new BatchDetails(dbConnectionString, replacementFileName, tempTableName, appId, null, user);
                working = true;
                batchDetails.PostProcessing = WaitForStopWorking;
                String guid; 
                try
                {
                    guid = svc.QueueBatch(batchDetails);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    working = false;
                }
                // Wait for waiters to wake up
                Thread.Sleep(5 * 1000);

                Assert.IsTrue(svc.CheckStatus(new Guid(guid), user) == UploadStatus.Processed, "File upload did not complete");
                Assert.AreEqual(errors.Count, 0);
                
                // Ensure temporary file has been deleted and queue emptied
                CheckTestFileDeleted();
                Assert.AreEqual(0, svc.GetQueueSize());
            }
            catch (Exception e)
            {
                String sr = e.Message;
                Assert.Fail(e.Message);
            }
            
        }
    }
}
