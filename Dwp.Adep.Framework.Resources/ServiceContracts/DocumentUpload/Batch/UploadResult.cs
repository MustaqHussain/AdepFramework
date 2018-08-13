using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    public class UploadResult
    {
        public UploadResult(Guid processId, String userId)
        {
            ProcessId = processId;
            UserId = userId;
        }
        public String ErrorMessage { get; set; }

        public UploadStatus Status { get; set; }

        public Guid ProcessId { get; private set; }

        public String UserId { get; private set; }
    }
}