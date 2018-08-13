using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices.AccountManagement;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.AD
{
    public class ActiveDirectoryHelper
    {
        private IMyPrincipalSearcher searcher;

        public ActiveDirectoryHelper(IMyPrincipalSearcher searcher)
        {
            this.searcher = searcher;
        }

        public List<string> GetEmailAddresses(string firstName, string lastName)
        {
            List<string> emailAddresses = new List<string>();

            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException("firstName");
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("lastName");

            emailAddresses = searcher.FindAll(firstName, lastName);
            
            return emailAddresses;
        }

        public ADUser GetEmailAddress(string samAccountName)
        {
            ADUser aduser = new ADUser();

            if (string.IsNullOrEmpty(samAccountName))
                throw new ArgumentNullException("samAccountName");

            aduser = searcher.FindEmail(samAccountName);

            return aduser;
        }

        /// <summary>
        /// Returns AD User for passed in Login Id 
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public ADUser SearchUserByLogin(string samAccountName)
        {
            ADUser aduser = new ADUser();

            if (string.IsNullOrEmpty(samAccountName))
                throw new ArgumentNullException("samAccountName");

            aduser = searcher.SearchByLoginId(samAccountName);

            return aduser;
        }

        /// <summary>
        /// Returns List of AD Users, searched by first and last name, 
        /// Supports wild card.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<ADUser> SearchUserByName(string firstName, string lastName)
        {
            List<ADUser> adUsers = new List<ADUser>();

            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException("firstName, lastName");

            adUsers = searcher.SearchByName(firstName, lastName);

            return adUsers;
        }

        /// <summary>
        /// Returns List of AD Users, searched by login id, 
        /// Supports wild card.
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public List<ADUser> SearchUsersByLoginId(string samAccountName)
        {
            List<ADUser> adUsers = new List<ADUser>();

            if (string.IsNullOrEmpty(samAccountName))
                throw new ArgumentNullException("samAccountName");

            adUsers = searcher.SearchUsersByLoginId(samAccountName);

            return adUsers;
        }

        /// <summary>
        /// Find all users in a group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public List<ADUser> GetUsersInGroupMembership(string groupName)
        {
            List<ADUser> adUsers = new List<ADUser>();

            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentNullException("groupName");

            adUsers = searcher.GetUsersInGroupMembership(groupName);

            return adUsers;
        }
       
    }
}
