using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts.AD
{
    public interface IPrincipalManager
    {
        /// <summary>
        /// Get Active Directory groups for user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>String array of Active Directory groups</returns>urns>
        string[] GetGroups(string userId);
    }
}
