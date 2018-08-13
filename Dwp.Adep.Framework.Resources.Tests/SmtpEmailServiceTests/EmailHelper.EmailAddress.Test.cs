using Dwp.Adep.Framework.Resources.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using System;

namespace Dwp.Adep.Framework.EmailService.Tests.SmtpEmailServiceTests
{
    [TestClass]
    public partial class EmailHelperTest
    {
        private FakeSmtpClient _fakeClient;
        private EmailHelper _emailHelper;
        private string subject;
        private string body;

        [TestInitialize]
        public void Before()
        {
            _fakeClient = new FakeSmtpClient();
            _emailHelper = new EmailHelper(_fakeClient);
            subject = "Email Subject";
            body = "Email Body";
        }

        #region Positive Tests

        #region Length

        [TestMethod, Timeout(1000)]
        public void EmailAddress_ShortLength_IsValid()
        {
            string emailToTest = "aa@cc.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_MediumLength_IsValid()
        {
            string emailToTest = "firstnamelastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_LongLength_IsValid()
        {
            string emailToTest = "glasgowcityodcadisputestrialdmacrnotification@adv.itsst.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        #endregion

        #region Domains

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithComDomain_IsValid()
        {
            string emailToTest = "firstnamelastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithCoUkDomain_IsValid()
        {
            string emailToTest = "firstnamelastname@pensions.dwp.co.uk";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        #endregion
        
        #region Characters

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithExclamation_IsValid()
        {
            string emailToTest = "firstname!lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithDollar_IsValid()
        {
            string emailToTest = "firstname$lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithPercentage_IsValid()
        {
            string emailToTest = "firstname%lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithCarat_IsValid()
        {
            string emailToTest = "firstname^lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithAmpersand_IsValid()
        {
            string emailToTest = "firstname&lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithAsterisk_IsValid()
        {
            string emailToTest = "firstname*lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithBracketsFacingToward_IsValid()
        {
            string emailToTest = "(firstname)lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithHyphen_IsValid()
        {
            string emailToTest = "firstname-lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]   
        public void EmailAddress_WithUnderscore_IsValid()
        {
            string emailToTest = "firstname_lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithEquals_IsValid()
        {
            string emailToTest = "firstname=lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithPlus_IsValid()
        {
            string emailToTest = "firstname+lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithLeftCurlyBracket_IsValid()
        {
            string emailToTest = "firstname{lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithRightCurlyBracket_IsValid()
        {
            string emailToTest = "firstname}lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithCurlyBracketsFacingAway_IsNotValid()
        {
            string emailToTest = "}firstname{lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithCurlyBracketsFacingToward_IsValid()
        {
            string emailToTest = "{firstname}lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithSemicolonSpaceAndEmail_IsValid()
        {
            string emailToTest = "firstname@pensions.dwp.com; lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithColonSpaceAndText_IsValid()
        {
            string emailToTest = "firstname: lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithColonSpaceAndEmail_IsValid()
        {
            string emailToTest = "firstname@pensions.dwp.com: lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithApostrophe_IsValid()
        {
            string emailToTest = "firstname'lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithHash_IsValid()
        {
            string emailToTest = "firstname#lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithTilde_IsValid()
        {
            string emailToTest = "firstname~lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithPipe_IsValid()
        {
            string emailToTest = "firstname|lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithForwardSlash_IsValid()
        {
            string emailToTest = "firstname/lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithQuestionMark_IsValid()
        {
            string emailToTest = "firstname?lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithDotInMiddle_IsValid()
        {
            string emailToTest = "firstname.lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        // Note: according to email specifications this should not be valid - but the library allows it
        public void EmailAddress_WithConsecutiveDotsInMiddle_IsValid()
        {
            string emailToTest = "firstname..lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_NumerousDotsInMiddle_IsValid()
        {
            string emailToTest = "f.i.r.s.t.n.a.m.e.l.a.s.t.n.a.m.e@p.e.n.s.i.o.n.s.d.w.p.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        // Note: according to email specifications this should not be valid - but the library allows it
        public void EmailAddress_WithDotAtEnd_IsValid()
        {
            string emailToTest = "firstnamelastname@pensions.dwp.com.";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        // Note: according to email specifications this should not be valid - but the library allows it
        public void EmailAddress_WithConsecutiveDotsAtEnd_IsValid()
        {
            string emailToTest = "firstnamelastname@pensions.dwp.com..";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }
        
        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithSpace_IsValid()
        {
            // This will send to lastname@pensions.dwp.com using firstname as the 'name'
            string emailToTest = "firstname lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        public void EmailAddress_WithAccentedLettersInAlias_IsValid()
        {
            string emailToTest = "cortès marta.cortes@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        #endregion

        #endregion

        #region Negative Tests

        #region Characters

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithDoubleQuote_IsNotValid()
        {
            // This will send to lastname@pensions.dwp.com using firstname as the 'name'
            string emailToTest = "firstname\"lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithPoundSigns_IsNotValid()
        {
            string emailToTest = "firstname£lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithLeftBracket_IsNotValid()
        {
            string emailToTest = "firstname(lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithRightBracket_IsNotValid()
        {
            string emailToTest = "firstname)lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithBracketsFacingAway_IsNotValid()
        {
            string emailToTest = ")firstname(lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithLeftSquareBracket_IsNotValid()
        {
            string emailToTest = "firstname[lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithRightSquareBracket_IsNotValid()
        {
            string emailToTest = "firstname]lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithSquareBracketsFacingAway_IsNotValid()
        {
            string emailToTest = "]firstname[lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithSquareBracketsFacingToward_IsNotValid()
        {
            string emailToTest = "[firstname]lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }
        
        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithSemicolonAndText_IsNotValid()
        {
            string emailToTest = "firstname;lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithSemicolonSpaceAndText_IsNotValid()
        {
            string emailToTest = "firstname; lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithSemicolonAndEmail_IsNotValid()
        {
            string emailToTest = "firstname@pensions.dwp.com;lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }
        
        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithColonAndText_IsNotValid()
        {
            string emailToTest = "firstname:lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithColonAndEmail_IsNotValid()
        {
            string emailToTest = "firstname@pensions.dwp.com:lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithBackslash_IsNotValid()
        {
            string emailToTest = "firstname\\lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithComma_IsNotValid()
        {
            string emailToTest = "firstname,lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithLeftAngleBracket_IsNotValid()
        {
            string emailToTest = "firstname<lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithRightAngleBracket_IsNotValid()
        {
            string emailToTest = "firstname>lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithAngleBracketsFacingAway_IsNotValid()
        {
            string emailToTest = ">firstname<lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithAngleBracketsFacingToward_IsNotValid()
        {
            string emailToTest = "<firstname>lastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithDotAtBeginning_IsNotValid()
        {
            string emailToTest = ".firstnamelastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithConsecutiveDotsAtBeginning_IsNotValid()
        {
            string emailToTest = "..firstnamelastname@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }
        
        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithAccentedLettersInAddress_IsNotValid()
        {
            string emailToTest = "marta.cortès@pensions.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }

        [TestMethod, Timeout(1000)]
        [ExpectedException(typeof(FormatException), "The MailAddress object did not throw an exception - this email is valid!")]
        public void EmailAddress_WithAccentedLettersInDomain_IsNotValid()
        {
            string emailToTest = "marta.cortes@pensìons.dwp.com";
            _emailHelper.SendMail(emailToTest, emailToTest, emailToTest, subject, body);
        }       

        #endregion

        #endregion
    }
}
