using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.AD
{
    public interface IMyPrincipalSearcher
    {
        List<string> FindAll(string firstName, string lastName);

        ADUser FindEmail(string samAccountName);

        ADUser SearchByLoginId(string samAccountName);
        List<ADUser> SearchByName(string firstName, string lastName);
        List<ADUser> SearchUsersByLoginId(string samAccountName);
        List<ADUser> GetUsersInGroupMembership(string groupName);
    }
}
