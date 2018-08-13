using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net.Mail;
using Dwp.Adep.Framework.Resources.FaultContracts;
using System.Configuration;
using System.IO;

using Dwp.Adep.Framework.Resources.MessageContracts;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.Email
{
    public class SmtpEmailService : ISmtpEmailService
    {

        public void SendEmailWithAttachments(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody, List<EmailAttachment> attachments)
        {
            SMTPClient emailClient = new SMTPClient();
            try
            {
                EmailHelper emailHelper = new EmailHelper(emailClient);
                emailHelper.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody, attachments);
            }

            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  public void SendEmail(string fromEmail:{0}, string toEmail:{1}, string ccEmail:{2}, string emailSubject:{3}, string emailBody:{4}); Detailed exception:{5}",
                    fromEmail, toEmail, ccEmail, emailSubject, emailBody, ex.ToString());
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Send Email";
                fault.ProblemType = "Error sending email";

                throw new FaultException<ServiceErrorFault>(fault);
            }
        }


        public void SendEmail(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody)
        {
            SMTPClient emailClient = new SMTPClient();
            try
            {
                EmailHelper emailHelper = new EmailHelper(emailClient);
                emailHelper.SendMail(fromEmail, toEmail, ccEmail, emailSubject, emailBody);
            }

            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  public void SendEmail(string fromEmail:{0}, string toEmail:{1}, string ccEmail:{2}, string emailSubject:{3}, string emailBody:{4}); Detailed exception:{5}", 
                    fromEmail, toEmail, ccEmail, emailSubject, emailBody, ex.ToString());
                Exception custom = new Exception(message);                
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Send Email";
                fault.ProblemType = "Error sending email";

                throw new FaultException<ServiceErrorFault>(fault);
            }
        }

//        public void SendEmailWithAttachment(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody, Stream attachment, string attachmentFilename)
        public void SendEmailWithAttachment(EmailRequest request)
        {
            SMTPClient emailClient = new SMTPClient();

            try
            {
                EmailHelper emailHelper = new EmailHelper(emailClient);
                emailHelper.SendMail(request.FromEmail, request.ToEmail, request.CcEmail, request.EmailSubject, request.EmailBody, request.Attachment, request.AttachmentFilename);
            }

            catch (Exception ex)
            {
                /*log error locally */
                string message = string.Format(
                    "Error in  public void SendEmail(string fromEmail:{0}, string toEmail:{1}, string ccEmail:{2}, string emailSubject:{3}, string emailBody:{4}); string attachment filename: {6}; Detailed exception:{5}",
                    request.FromEmail, request.ToEmail, request.CcEmail, request.EmailSubject, request.EmailBody, ex.ToString(), request.AttachmentFilename);
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Send Email";
                fault.ProblemType = "Error sending email";

                throw new FaultException<ServiceErrorFault>(fault);
            }
        }
    }
}
