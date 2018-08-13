using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.DocumentUpload.Batch
{
    public interface IDataStoreFactory
    {
        IDataStore CreateDataStore();
    }
}
