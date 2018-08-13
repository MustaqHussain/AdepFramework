using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Resources.DataContracts
{
    [DataContract]
    public enum UploadStatus { [EnumMember]Queued,  [EnumMember] Running, [EnumMember]Uploaded,  [EnumMember] Processed,  [EnumMember] Failed,  [EnumMember] Cancelled,  [EnumMember] Invalid }
}