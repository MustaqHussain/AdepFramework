using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Dwp.Adep.Framework.Resources.Email
{
    public interface ISmtpClient
    {
        void Send(MailMessage mailMessage);
    }
}

