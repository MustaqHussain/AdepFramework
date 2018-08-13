using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;
using System.DirectoryServices.AccountManagement;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts.AD
{
    public interface IActiveDirectoryHelper
    {
        string[] GetADGroups(string userID);
    }
}
