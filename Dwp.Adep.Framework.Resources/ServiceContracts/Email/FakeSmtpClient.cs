using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
namespace Dwp.Adep.Framework.Resources.Email
{
    public class FakeSmtpClient : ISmtpClient
    {
        public bool MailSent { get; set; }
        public int AttachmentCount { get; set; }
        public string FirstAttachmentName { get; set; }
        public string SecondAttachmentName { get; set; }

        public FakeSmtpClient()
        {
            MailSent = false;
        }
        public void Send(MailMessage message)
        {
            // Done as Attachment object is disposed of after message sent.
            if (message.Attachments.Count > 0)
            {
                AttachmentCount = message.Attachments.Count;
                FirstAttachmentName = message.Attachments[0].Name;
                if (AttachmentCount > 1)
                {
                    SecondAttachmentName = message.Attachments[1].Name;
                }
            }
            

            if (message.To.Count == 0 && message.CC.Count == 0)
            {
                throw new ArgumentException();
            }
            MailSent = true;
        }
    }

}
