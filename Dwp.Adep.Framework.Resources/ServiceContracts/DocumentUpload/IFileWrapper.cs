using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload
{
    public interface IFileWrapper
    {
        DataTable GetDataTable();

        List<string> GetColumnNames();
    }
}
