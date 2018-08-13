using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.AD
{
    public class RealPrincipalSearcher : IMyPrincipalSearcher
    {
        public List<string> FindAll(string firstName, string lastName)
        {
            PrincipalSearchResult<Principal> result = null;
            List<string> emailAdresses = new List<string>();

            string username = ConfigurationManager.AppSettings["ADUsername"];
            string password = ConfigurationManager.AppSettings["ADPassword"];

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, null, username, password))
            {
                UserPrincipal filter = new UserPrincipal(pc);
                filter.GivenName = firstName;
                filter.Surname = lastName;
                PrincipalSearcher principalSearcher = new PrincipalSearcher(filter);
                result = principalSearcher.FindAll();

                foreach (UserPrincipal user in result)
                {
                    emailAdresses.Add(user.EmailAddress);
                }

            }

            return emailAdresses;
        }


        public ADUser FindEmail(string samAccountName)
        {
            ADUser aduser = new ADUser();

            string username = ConfigurationManager.AppSettings["ADUsername"];
            string password = ConfigurationManager.AppSettings["ADPassword"];

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, null, username, password))
            {
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, samAccountName);
                if (userPrincipal != null)
                {
                    aduser.Email = userPrincipal.EmailAddress;
                    aduser.FirstName = userPrincipal.GivenName;
                    aduser.LastName = userPrincipal.Surname;
                }
            }

            return aduser;
        }

        /// <summary>
        /// Populate Required fields from AD
        /// </summary>
        /// <param name="userPrincipal"></param>
        /// <returns></returns>
        private ADUser PopulateADUserFields(UserPrincipal userPrincipal)
        {
            var adUser = new ADUser();
            adUser.Email = userPrincipal.EmailAddress;
            adUser.FirstName = userPrincipal.GivenName;
            adUser.LastName = userPrincipal.Surname;
            adUser.DistinguishedName = userPrincipal.DistinguishedName;
            adUser.GivenName = userPrincipal.GivenName;
            adUser.Login = userPrincipal.SamAccountName;
            adUser.UserPrincipalName = userPrincipal.UserPrincipalName;
            adUser.EmployeeId = userPrincipal.EmployeeId;

            adUser.Groups = new List<ADGroup>();
            if (userPrincipal.GetUnderlyingObjectType() == typeof(DirectoryEntry))
            {
                var entry = (DirectoryEntry)userPrincipal.GetUnderlyingObject();
                adUser.ProfilePath = (string)entry.Properties["profilePath"].Value;
                adUser.DNSHostname = (string)entry.Properties["dNSHostname"].Value;
                adUser.SN = (string)entry.Properties["sn"].Value;
                adUser.TelephoneNumber = (string)entry.Properties["telephoneNumber"].Value;
                adUser.OfficeLocation = (string)entry.Properties["physicalDeliveryOfficeName"].Value;
            }
            return adUser;
        }

        /// <summary>
        /// Get users Groups
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IEnumerable<GroupPrincipal> GetGroups(UserPrincipal user)
        {
            var grps = new List<GroupPrincipal>();
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetGroups(); //.GetAuthorizationGroups();
                foreach (var principal in groups)
                {
                    if (principal is GroupPrincipal)
                        grps.Add((GroupPrincipal)principal);
                }
            }

            return grps;
        }

        /// <summary>
        /// Search AD User for passed in Login Id 
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public ADUser SearchByLoginId(string samAccountName)
        {
            ADUser aduser = new ADUser();

            string username = ConfigurationManager.AppSettings["ADUsername"];
            string password = ConfigurationManager.AppSettings["ADPassword"];

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, null, username, password))
            {
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, samAccountName);
                if (userPrincipal != null)
                {
                   aduser = PopulateADUserFields(userPrincipal);
                   foreach (var group in GetGroups(userPrincipal))
                   {
                       aduser.Groups.Add(
                            new ADGroup(){ DisplayName = group.Name, SamAccountName = group.SamAccountName}
                           );
                   }
                }
            }

            return aduser;
        }

        /// <summary>
        /// Returns List of AD Users, searched by first and last name, 
        /// Supports wild card.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<ADUser> SearchByName(string firstName, string lastName)
        {
            PrincipalSearchResult<Principal> result = null;
            List<ADUser> users = new List<ADUser>();
            
            string username = ConfigurationManager.AppSettings["ADUsername"];
            string password = ConfigurationManager.AppSettings["ADPassword"];

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, null, username, password))
            {
                UserPrincipal filter = new UserPrincipal(pc);
                
                if(!string.IsNullOrEmpty(firstName))
                    filter.GivenName = firstName;

                if (!string.IsNullOrEmpty(lastName))
                    filter.Surname = lastName;

                PrincipalSearcher principalSearcher = new PrincipalSearcher(filter);

                //todo: need to test following for pagination
                //(principalSearcher.GetUnderlyingSearcher() as DirectorySearcher).PageSize = 2;
                //(principalSearcher.GetUnderlyingSearcher() as DirectorySearcher).SizeLimit = 2;
 

                result = principalSearcher.FindAll();

                foreach (UserPrincipal user in result)
                {
                    users.Add(PopulateADUserFields(user));
                }

            }

            return users;
        }
        
        public List<ADUser> GetUsersInGroupMembership(string groupName)
        {
            List<ADUser> adUsers = new List<ADUser>();

            string username = ConfigurationManager.AppSettings["ADUsername"];
            string password = ConfigurationManager.AppSettings["ADPassword"];

            using (PrincipalContext context = new PrincipalContext(ContextType.Domain, null, username, password))
            {
                using (var searcher = new PrincipalSearcher())
                {
                    var groupPrincipal = new GroupPrincipal(context, groupName);
                    searcher.QueryFilter = groupPrincipal;
                    var group = searcher.FindOne() as GroupPrincipal;

                    if (null != group)
                    {
                        foreach (var member in group.GetMembers())
                        {
                            if (member is UserPrincipal)
                            {
                                var user = member as UserPrincipal;
                                adUsers.Add(PopulateADUserFields(user));
                            }
                        }
                    }
                }
            }
            return adUsers;
        }

        /// <summary>
        /// Find users by login ID, supports wild card search
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public List<ADUser> SearchUsersByLoginId(string samAccountName)
        {
            PrincipalSearchResult<Principal> result = null;
            List<ADUser> users = new List<ADUser>();

            string username = ConfigurationManager.AppSettings["ADUsername"];
            string password = ConfigurationManager.AppSettings["ADPassword"];

            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, null, username, password))
            {
                UserPrincipal filter = new UserPrincipal(pc);

                if (!string.IsNullOrEmpty(samAccountName))
                    filter.SamAccountName = samAccountName;

                PrincipalSearcher principalSearcher = new PrincipalSearcher(filter);

                result = principalSearcher.FindAll();

                foreach (UserPrincipal user in result)
                {
                    users.Add(PopulateADUserFields(user));
                }

            }

            return users;
        }
    }
}