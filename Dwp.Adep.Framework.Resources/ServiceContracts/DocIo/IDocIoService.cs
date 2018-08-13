using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

using Dwp.Adep.Framework.Resources.DataContracts;

using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocIo
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDocIoService" in both code and config file together.
    
    [ServiceContract]
    public interface IDocIoService
    {
        [OperationContract]
        byte[] GenerateDocument(byte[] fileByte, List<DocContent> documentContent, string outputFileType);
    }
}
