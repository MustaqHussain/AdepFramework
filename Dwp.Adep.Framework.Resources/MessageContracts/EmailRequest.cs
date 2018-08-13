using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.ServiceModel;
using System.Web;

namespace Dwp.Adep.Framework.Resources.MessageContracts
{
    [MessageContract()]
    public sealed class EmailRequest
    {
        private string _fromEmail;
        private string _toEmail;
        private string _ccEmail;
        private string _emailSubject;
        private string _emailBody;
//        private Stream _attachment;
        private string _attachment;
        private string _attachmentFilename;

        /// <summary>
        /// From Email item
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FromEmail
        {
            get
            {
                return this._fromEmail;
            }
            set
            {
                this._fromEmail = value;
            }
        }

        /// <summary>
        /// To Email item
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string ToEmail
        {
            get
            {
                return this._toEmail;
            }
            set
            {
                this._toEmail = value;
            }
        }

        /// <summary>
        /// CC Email item
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string CcEmail
        {
            get
            {
                return this._ccEmail;
            }
            set
            {
                this._ccEmail = value;
            }
        }

        /// <summary>
        /// Email Subject item
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string EmailSubject
        {
            get
            {
                return this._emailSubject;
            }
            set
            {
                this._emailSubject = value;
            }
        }

        /// <summary>
        /// Email Body item
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string EmailBody
        {
            get
            {
                return this._emailBody;
            }
            set
            {
                this._emailBody = value;
            }
        }

        /// <summary>
        /// Attachment filename item
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string AttachmentFilename
        {
            get
            {
                return this._attachmentFilename;
            }
            set
            {
                this._attachmentFilename = value;
            }
        }

        /// <summary>
        /// Attachment item
        /// </summary>
        [MessageBodyMember(Order = 1)]
//        public Stream Attachment
        public string Attachment
        {
            get
            {
                return this._attachment;
            }
            set
            {
                this._attachment = value;
            }
        }

    }
}