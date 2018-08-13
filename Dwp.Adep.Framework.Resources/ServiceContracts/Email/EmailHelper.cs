using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net.Mime;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.Email
{

    public class EmailHelper
    {
        public ISmtpClient smtpClient;

        public EmailHelper(ISmtpClient smptpClient)
        {
            this.smtpClient = smptpClient;
        }

        public void SendMail(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody, List<EmailAttachment> attachments)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                MailAddress fromAddress = new MailAddress(fromEmail.Trim());

                if (null != toEmail && toEmail.Contains(";"))
                {
                    foreach (var email in toEmail.Split(';'))
                    {
                        mailMessage.To.Add(new MailAddress(email.Trim()));
                    }
                }
                else if (null != toEmail)
                {
                    mailMessage.To.Add(new MailAddress(toEmail.Trim()));
                }

                mailMessage.From = fromAddress;

                if (null != ccEmail && ccEmail.Trim() != "")
                {
                    foreach (var email in ccEmail.Split(';'))
                    {
                        mailMessage.CC.Add(new MailAddress(email.Trim()));
                    }
                }
                mailMessage.Subject = emailSubject.Trim();
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = emailBody.Trim();

                List<MemoryStream> streams = new List<MemoryStream>();

                if (attachments != null && attachments.Count > 0)
                {
                    foreach (var attachment in attachments)
                    {
                        var stream = new MemoryStream(attachment.Data);
                        streams.Add(stream);
                        mailMessage.Attachments.Add(new Attachment(stream, attachment.Name));
                    }
                }



                smtpClient.Send(mailMessage);

                foreach (var s in streams)
                {
                    try
                    {
                        s.Dispose();
                    }
                    catch { }
                }
            }
        }

        public void SendMail(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody)
        {
            //using (MailMessage mailMessage = new MailMessage())
            //{
            //    if (IsEmailValid(fromEmail))
            //    {
            //        MailAddress fromAddress = new MailAddress(fromEmail.Trim());
            //        if (null != toEmail && toEmail.Contains(";"))
            //        {
            //            foreach (var email in toEmail.Split(';'))
            //            {
            //                if (IsEmailValid(email.Trim()))
            //                    mailMessage.To.Add(new MailAddress(email.Trim()));
            //                else
            //                    throw new Exception("Invalid ToEmail address");
            //            }
            //        }
            //        else if (null != toEmail)
            //        {
            //            if (IsEmailValid(toEmail.Trim()))
            //                mailMessage.To.Add(new MailAddress(toEmail.Trim()));
            //            else
            //                throw new Exception("Invalid ToEmail Address");
            //        }


            //        mailMessage.From = fromAddress;

            //        if (null != ccEmail && ccEmail.Trim() != "")
            //        {

            //            foreach (var email in ccEmail.Split(';'))
            //            {
            //                if (IsEmailValid(email.Trim()))
            //                    mailMessage.CC.Add(new MailAddress(email.Trim()));
            //                else
            //                    throw new Exception("Invalid CCemail address");
            //            }
            //        }
            //        mailMessage.Subject = emailSubject.Trim();
            //        mailMessage.IsBodyHtml = true;
            //        mailMessage.Body = emailBody.Trim();

            //        smtpClient.Send(mailMessage);
            //    }
            //    else
            //        throw new Exception("Invalid email address");
            //}

            SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody, null, null);
        }

        public void SendMail(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody, string attachmentXml, string attachmentFilename)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                MailAddress fromAddress = new MailAddress(fromEmail.Trim());

                if (null != toEmail && toEmail.Contains(";"))
                {
                    foreach (var email in toEmail.Split(';'))
                    {
                        mailMessage.To.Add(new MailAddress(email.Trim()));
                    }
                }
                else if (null != toEmail)
                {
                    mailMessage.To.Add(new MailAddress(toEmail.Trim()));
                }

                mailMessage.From = fromAddress;

                if (null != ccEmail && ccEmail.Trim() != "")
                {

                    foreach (var email in ccEmail.Split(';'))
                    {
                        mailMessage.CC.Add(new MailAddress(email.Trim()));
                    }
                }

                mailMessage.Subject = emailSubject.Trim();
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = emailBody.Trim();

                // process attachment
                MemoryStream ms = new MemoryStream();

                if (!string.IsNullOrEmpty(attachmentXml) && !string.IsNullOrEmpty(attachmentFilename))
                {
                    ms = new MemoryStream(UTF8Encoding.UTF8.GetBytes(attachmentXml));
                    mailMessage.Attachments.Add(new Attachment(ms, GetContentType(attachmentFilename)));
                }
                smtpClient.Send(mailMessage);

                ms.Close();
                ms.Dispose();
            }
        }

        private ContentType GetContentType(string filename)
        {
            ContentType contentType = new ContentType();

            string fileExtension = filename.Substring(filename.LastIndexOf('.') + 1);

            switch (fileExtension)
            {
                case "html":
                    contentType.MediaType = MediaTypeNames.Text.Html;
                    break;

                case "pdf":
                    contentType.MediaType = MediaTypeNames.Application.Pdf;
                    break;

                case "rtf":
                    contentType.MediaType = MediaTypeNames.Text.RichText;
                    break;

                case "zip":
                    contentType.MediaType = MediaTypeNames.Application.Zip;
                    break;

                case "xml":
                    contentType.MediaType = MediaTypeNames.Text.Xml;
                    break;

                default:
                    contentType.MediaType = MediaTypeNames.Text.Plain;
                    break;
            }

            contentType.Name = filename;

            return contentType;
        }
    }
}