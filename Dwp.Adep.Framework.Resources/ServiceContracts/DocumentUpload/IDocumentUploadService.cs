using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.IO;

using Dwp.Adep.Framework.Resources.FaultContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    [ServiceContract]
    public interface IDocumentUploadService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        bool UploadFile(String documentLocation, String replacementFileName);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        bool UploadFileToDirectory(String documentLocation, String directoryName, String replacementFileName);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        string GetFullFileName(string fileName);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        bool DeleteFile(string fileName, string fileLocation);

        bool DeleteFile(string fileName);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        bool FileExists(string fileName);
    }
}
