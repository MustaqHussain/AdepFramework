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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DocIoService" in code, svc and config file together.
    
    public class DocIoService : IDocIoService
    {
        public byte[] GenerateDocument(byte[] fileByte, List<DocContent> documentContent,string outputFileType)
        {
            byte[] docIoFileByte = null;
            try
            {
                DocIoHelper docIoHelper = new DocIoHelper();
                docIoFileByte = docIoHelper.GenerateDocument(fileByte, documentContent, outputFileType);
                if (docIoFileByte == null)
                {
                    throw new Exception("Byte array is null");
                }
            }
            catch (Exception ex)
            {
                string message = "Service Exception : " + ex.ToString();
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);
            }
            return docIoFileByte;
        }
    }
}
