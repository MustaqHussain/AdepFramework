using System;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Dwp.Adep.Framework.Resources.DataContracts;
using Dwp.Adep.Framework.Resources.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dwp.Adep.Framework.EmailService.Tests.SmtpEmailServiceTests
{
    public partial class EmailHelperTest
    {
        [TestMethod]
        public void SendEmailMultipleAttachments_EmailInIsSameAsEmailOut()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody, null);

            Assert.AreEqual(fromEmail, resultMessage.From.Address);
            Assert.AreEqual(toEmail, resultMessage.To[0].Address);
            Assert.AreEqual(ccEmail, resultMessage.CC[0].Address);
            Assert.AreEqual(emailSubject, resultMessage.Subject);
            Assert.AreEqual(emailBody, resultMessage.Body);
        }

        #region 'From' tests
        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple invalid 'from' email specified")]
        public void SendEmailMultipleAttachments_EmailWithInvalidFrom_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(invalidEmail, toEmail, ccEmail, emailSubject, emailBody, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Exception not thrown when a null 'from' value is passed in")]
        //  Null Exception ok, as service clients will see the same service fault whatever (Best Practice should be "Invalid Email Though").
        public void SendEmailMultipleAttachments_EmailWithNullFromValue_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(null, toEmail, ccEmail, emailSubject, emailBody);
        }
        #endregion

        #region 'To' tests
        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple invalid 'to' email specified")]
        public void SendEmailMultipleAttachments_EmailWithInvalidTo_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, invalidEmail, ccEmail, emailSubject, emailBody, null);
        }

        [TestMethod]
        public void SendEmailMultipleAttachments_EmailWithMultipleTos_SendsToMultipleUsers()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail + "; abc.bcd@pensions.dwp.com", ccEmail, emailSubject, emailBody, null);

            Assert.AreEqual(toEmail, resultMessage.To[0].Address);
            Assert.AreEqual("abc.bcd@pensions.dwp.com", resultMessage.To[1].Address);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple emails passed in with one been invalid")]
        public void SendEmailMultipleAttachments_EmailWithMultipleTosWithInvalid_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, "firstname.lastname@pensions.dwp.com; " + invalidEmail, ccEmail, emailSubject, emailBody, null);
        }

        [TestMethod]
        public void SendEmailMultipleAttachments_EmailWithNullToValue_NoException_EntirelyPlausible()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, null, ccEmail, emailSubject, emailBody, null);

            Assert.AreEqual(fromEmail, resultMessage.From.Address);
            Assert.AreEqual(ccEmail, resultMessage.CC[0].Address);
            Assert.AreEqual(emailSubject, resultMessage.Subject);
            Assert.AreEqual(emailBody, resultMessage.Body);
        }
        #endregion

        #region 'CC' tests
        [TestMethod]
        public void SendEmailMultipleAttachments_EmailWithNullCCValue_NoException_EntirelyPlausible()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail, null, emailSubject, emailBody, null);

            Assert.AreEqual(fromEmail, resultMessage.From.Address);
            Assert.AreEqual(toEmail, resultMessage.To[0].Address);
            Assert.AreEqual(emailSubject, resultMessage.Subject);
            Assert.AreEqual(emailBody, resultMessage.Body);

        }

        [TestMethod]
        public void SendEmailMultipleAttachments_EmailWithMultipleCCs_SendsToMultiple()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail + "; abc.bcd@pensions.dwp.com", emailSubject, emailBody, null);

            Assert.AreEqual(ccEmail, resultMessage.CC[0].Address);
            Assert.AreEqual("abc.bcd@pensions.dwp.com", resultMessage.CC[1].Address);
        }

        [TestMethod, Timeout(10000)]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple 'CC'd' emails passed in with one been invalid")]
        public void SendEmailMultipleAttachments_EmailWithMultipleCCsOneBeenInvalid_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, null, "firstname.lastname@pensions.dwp.com; " + invalidEmail, emailSubject, emailBody);
        }


        #endregion

        #region Other tests
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Exception not thrown when a null 'to & cc' values is passed in")]
        public void SendEmailMultipleAttachments_EmailWithNullToAndCCValue_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, null, null, emailSubject, emailBody, null);
        }


        [TestMethod, Timeout(10000)]
        public void SendEmailMultipleAttachments_EmailWithSingleAttachmentSends()
        {

            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody, new List<EmailAttachment>()
                {
                    new EmailAttachment()
                        {
                            Name = "email.txt",
                            Data = new byte[1]
                        }
                });
            Assert.IsTrue(_fakeClient.AttachmentCount == 1);
            Assert.IsTrue(_fakeClient.FirstAttachmentName == "email.txt");
        }

        [TestMethod]
        public void SendEmailMultipleAttachments_EmailWithMultipleAttachmentSends()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody, new List<EmailAttachment>()
                {
                    new EmailAttachment()
                        {
                            Name = "email.txt",
                            Data = new byte[1]
                        },
                    new EmailAttachment()
                        {
                            Name = "email2.txt",
                            Data = new byte[1]   
                        }
                });
            Assert.IsTrue(_fakeClient.AttachmentCount == 2);
            Assert.AreEqual("email.txt", _fakeClient.FirstAttachmentName);
            Assert.AreEqual("email2.txt", _fakeClient.SecondAttachmentName);
        }
        #endregion

    }
}
