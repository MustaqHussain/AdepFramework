using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;
namespace Dwp.Adep.Framework.Resources.Email
{
    public class SMTPClient : ISmtpClient
    {
        public void Send(MailMessage message)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}