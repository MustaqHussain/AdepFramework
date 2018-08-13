using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Dwp.Adep.Framework.Resources.FaultContracts;
using Dwp.Adep.Framework.Resources.MessageContracts;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.Email
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISmtpEmailService" in both code and config file together.
    [ServiceContract]
    public interface ISmtpEmailService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void SendEmail(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void SendEmailWithAttachments(string fromEmail, string toEmail, string ccEmail, string emailSubject, string emailBody, List<EmailAttachment> attachments);


        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void SendEmailWithAttachment(EmailRequest emailRequest);

    }
}
