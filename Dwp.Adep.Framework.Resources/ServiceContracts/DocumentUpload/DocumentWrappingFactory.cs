using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    class DocumentWrappingFactory
    {

        public static IFileWrapper CreateDocumentWrapper(String fileName)
        {
            if (fileName != null && fileName.ToLower().EndsWith(".xls"))
            {
                return new ExcelWrapper(fileName);
            }
            else
            {
                throw new NotImplementedException("Only Excel (.xls) file types supported");
            }
        }
    }
}
