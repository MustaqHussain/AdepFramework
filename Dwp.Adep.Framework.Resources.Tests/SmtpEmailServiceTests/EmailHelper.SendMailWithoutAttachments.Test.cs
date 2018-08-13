using System;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Dwp.Adep.Framework.Resources.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Dwp.Adep.Framework.EmailService.Tests.SmtpEmailServiceTests
{
    public partial class EmailHelperTest
    {
        [TestMethod]
        public void SendEmail_EmailInIsSameAsEmailOut()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody);

            Assert.AreEqual(fromEmail, resultMessage.From.Address);
            Assert.AreEqual(toEmail, resultMessage.To[0].Address);
            Assert.AreEqual(ccEmail, resultMessage.CC[0].Address);
            Assert.AreEqual(emailSubject, resultMessage.Subject);
            Assert.AreEqual(emailBody, resultMessage.Body);
        }

        #region 'From' Tests
        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple invalid 'from' email specified")]
        public void SendEmail_EmailWithInvalidFrom_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(invalidEmail, toEmail, ccEmail, emailSubject, emailBody);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Exception not thrown when a null 'from' value is passed in")]
        //  Null Exception ok, as service clients will see the same service fault whatever (Best Practice should be "Invalid Email Though").
        public void SendEmail_EmailWithNullFromValue_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(null, toEmail, ccEmail, emailSubject, emailBody);
        }
        #endregion

        #region 'To' Tests
        [TestMethod]
        public void SendEmail_EmailWithNullToValue_NoException_EntirelyPlausible()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, null, ccEmail, emailSubject, emailBody);

            Assert.AreEqual(fromEmail, resultMessage.From.Address);
            Assert.AreEqual(ccEmail, resultMessage.CC[0].Address);
            Assert.AreEqual(emailSubject, resultMessage.Subject);
            Assert.AreEqual(emailBody, resultMessage.Body);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple invalid 'to' email specified")]
        public void SendEmail_EmailWithInvalidTo_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, invalidEmail, ccEmail, emailSubject, emailBody);
        }

        [TestMethod]
        public void SendEmail_EmailWithMultipleTos_SendsToMultipleUsers()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail + "; abc.bcd@pensions.dwp.com", ccEmail, emailSubject, emailBody);

            Assert.AreEqual(toEmail, resultMessage.To[0].Address);
            Assert.AreEqual("abc.bcd@pensions.dwp.com", resultMessage.To[1].Address);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple emails passed in with one been invalid")]
        public void SendEmail_EmailWithMultipleTosWithInvalid_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, "firstname.lastname@pensions.dwp.com; " + invalidEmail, ccEmail, emailSubject, emailBody);
        }
        #endregion

        #region 'CC' Tests
        [TestMethod]
        public void SendEmail_EmailWithNullCCValue_NoException_EntirelyPlausible()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail, null, emailSubject, emailBody);

            Assert.AreEqual(fromEmail, resultMessage.From.Address);
            Assert.AreEqual(toEmail, resultMessage.To[0].Address);
            Assert.AreEqual(emailSubject, resultMessage.Subject);
            Assert.AreEqual(emailBody, resultMessage.Body);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple 'CC'd' emails passed in with one been invalid")]
        public void SendEmail_EmailWithInvalidCC_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, null, invalidEmail, emailSubject, emailBody);
        }

        [TestMethod]
        public void SendEmail_EmailWithMultipleCCs_SendsToMultiple()
        {
            MailMessage resultMessage = null;
            var mockHelper = new Mock<ISmtpClient>();
            mockHelper.Setup(x => x.Send(It.IsAny<MailMessage>()))
                      .Callback<MailMessage>((message) => resultMessage = message);
            var emailHelperWithMock = new EmailHelper(mockHelper.Object);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail + "; abc.bcd@pensions.dwp.com", emailSubject, emailBody);

            Assert.AreEqual(ccEmail, resultMessage.CC[0].Address);
            Assert.AreEqual("abc.bcd@pensions.dwp.com", resultMessage.CC[1].Address);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Exception not thrown when multiple 'CC'd' emails passed in with one been invalid")]
        public void SendEmail_EmailWithMultipleCCsOneBeenInvalid_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, null, "firstname.lastname@pensions.dwp.com; " + invalidEmail, emailSubject, emailBody);
        }
        #endregion

        #region Other Tests (attachments)
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Exception not thrown when a null 'to & cc' values is passed in")]
        public void SendEmail_EmailWithNullToAndCCValue_ThrowsException()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, null, null, emailSubject, emailBody);
        }

        [TestMethod, Timeout(10000)]
        public void SendEmail_EmailWithAttachmentSends()
        {
            var emailHelperWithMock = new EmailHelper(_fakeClient);
            emailHelperWithMock.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody, xmlAttachment, attachmentName);
            Assert.IsTrue(_fakeClient.AttachmentCount == 1);
            Assert.AreEqual(attachmentName, _fakeClient.FirstAttachmentName);
        }
        #endregion

    }
}

