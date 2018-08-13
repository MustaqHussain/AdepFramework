using Dwp.Adep.Framework.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Dwp.Adep.Framework.Resources.Email;
using System.Timers;
using System.Threading;

namespace Dwp.Adep.Framework.EmailService.Tests.SmtpEmailServiceTests
{


    /// <summary>
    ///This is a test class for SmtpEmailServiceTest and is intended
    ///to contain all SmtpEmailServiceTest Unit Tests
    ///</summary>
    public partial class EmailHelperTest
    {
        private string fromEmail = "support@adv.itsst.com";
        private string toEmail = "dwpadmin@adv.itsst.com";
        private string ccEmail = "admin@adv.itsst.com";
        private string invalidEmail = "£email@adv.itsst.com";
        private string emailSubject = "subject";
        private string emailBody = "some body";

        private string xmlAttachment = @"<?xml-stylesheet type=""text/xsl"" href=""stylesheets/dbd623.xsl""?>
<dbd623 xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""DITM-ITD-Norcross-DBD623"">
  <document_reference>TODO: reference</document_reference>
  <sent_date>2012-12-12</sent_date>
  <mota_section_details>
    <contact />
    <section>TODO: section</section>
    <room>TODO: room</room>
    <contact_telephone>TODO telephone</contact_telephone>
  </mota_section_details>
  <part_one subject=""Customer Details"">
    <surname_status new=""false"" />
    <previous_surname new=""false"" />
    <customer_details>
      <CitizenName xmlns=""http://www.govtalk.gov.uk/people/AddressAndPersonalDetails"">
        <CitizenNameForename>test</CitizenNameForename>
        <CitizenNameSurname>test</CitizenNameSurname>
      </CitizenName>
    </customer_details>
    <address_status new=""false"" />
    <customer_address>
      <Line xmlns=""http://www.govtalk.gov.uk/people/bs7666"">test</Line>
      <Line xmlns=""http://www.govtalk.gov.uk/people/bs7666"">test</Line>
    </customer_address>
    <change_of_address_date>0001-01-01</change_of_address_date>
    <telephone_number_not_known selected=""true"" />
    <current_crn new=""false"">98563247</current_crn>
    <current_arn new=""false"" />
  </part_one>
  <part_five subject=""Overcashment"" />
  <part_six subject=""Payment/Recovery"" />
  <part_eleven subject=""Other"">
    <other_text>testing</other_text>
  </part_eleven>
</dbd623>";
        private string attachmentName = "Test.xml";
    }
}