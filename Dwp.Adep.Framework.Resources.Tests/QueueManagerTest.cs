using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch;
using Dwp.Adep.Framework.Resources.DataContracts;
using Dwp.Adep.Framework.Resources.AdminService;
using Moq;

namespace Dwp.Adep.Framework.EmailService.Tests
{
    [TestClass]
    public class QueueManagerTest
    {
        private readonly string user = "test";

        private UploadQueueVMDC uploadRecord = new UploadQueueVMDC();

        private void UpdateToStatus()
        {
            uploadRecord.UploadQueueItem.Status = "F";
        }

        private QueueManager GetQueueManager()
        {
            var adminService = new Mock<IAdminService>();
           
            uploadRecord.UploadQueueItem = new UploadQueueDC();
            uploadRecord.UploadQueueItem.Status = "C";
            adminService.Setup(x => x.GetUploadQueue(user, user, "FrameworkAdmin", "", It.IsAny<String>())).Returns(uploadRecord);

            adminService.Setup(x => x.UpdateUploadQueue(user, user, "FrameworkAdmin", "", uploadRecord.UploadQueueItem)).Callback(UpdateToStatus);

            return new QueueManager(adminService.Object);
        }

        [TestMethod]
        public void TestAddToQueue()
        {
       
            QueueManager qMgr = GetQueueManager();
            uploadRecord.UploadQueueItem.Status = "P";
            Assert.AreEqual(qMgr.GetStatus(new Guid(), user), UploadStatus.Processed);

            uploadRecord.UploadQueueItem.Status = "X";
            Assert.AreEqual(qMgr.GetStatus(new Guid(), user), UploadStatus.Cancelled);
        

        }

        [TestMethod]
        public void TestUpdateQueue()
        {
            QueueManager qMgr = GetQueueManager();

            qMgr.UpdateQueue(new Guid(), UploadStatus.Failed, user);

            Assert.AreEqual(uploadRecord.UploadQueueItem.Status, "F");
        }


        [TestMethod]
        public void TestDeleteFromQueue()
        {

            QueueManager qMgr = GetQueueManager();

            qMgr.Dequeue(new Guid(), user);

            Assert.AreEqual(uploadRecord.UploadQueueItem.Status, "F");
        }

        [TestMethod]
        public void TestQueryQueue()
        {
            var adminService = new Mock<IAdminService>();

            uploadRecord.UploadQueueItem = new UploadQueueDC();
            uploadRecord.UploadQueueItem.Status = "C";
            adminService.Setup(x => x.GetUploadQueue(user, user, "FrameworkAdmin", "", It.IsAny<String>())).Returns(uploadRecord);

            adminService.Setup(x => x.UpdateUploadQueue(user, user, "FrameworkAdmin", "", uploadRecord.UploadQueueItem)).Callback(UpdateToStatus);

            adminService.Setup(x => x.SearchUploadQueue(user, user, "FrameworkAdmin", "", It.IsAny<UploadQueueSearchCriteriaDC>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(new UploadQueueSearchVMDC() { RecordCount = 0 });

            QueueManager qMgr = new QueueManager(adminService.Object);
            List<Guid> items = qMgr.GetItems(UploadStatus.Processed, user);

            adminService.Verify(x => x.SearchUploadQueue(user, user, "FrameworkAdmin", "", It.Is<UploadQueueSearchCriteriaDC>(query => query.Status == "C"), 1, 100, true), Times.Once());

        }
    }
}
