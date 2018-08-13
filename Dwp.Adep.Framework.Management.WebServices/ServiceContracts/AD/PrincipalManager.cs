using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using Dwp.Adep.Framework.Management.WebServices.Exceptions;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts.AD
{
    public class PrincipalManager : IPrincipalManager
    {
        /// <summary>
        /// Get Active Directory groups for user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>String array of Active Directory groups</returns>
        public string[] GetGroups(string userId)
        {
            // Get the domain inoformation from configuration
            string domainName = ConfigurationManager.AppSettings.Get("DomainName");
            string adUserName = ConfigurationManager.AppSettings.Get("ADUserName");
            string adPassword = ConfigurationManager.AppSettings.Get("ADPassword");
            if (null == domainName) throw new ArgumentNullException("domainName");
            if (null == adUserName) throw new ArgumentNullException("adUserName");
            if (null == adPassword) throw new ArgumentNullException("adPassword");

            
            
            try
            {
                // Create principal context and user prnicipal with which to access Active Directory
                PrincipalContext principalContextItem = new PrincipalContext(ContextType.Domain, domainName, adUserName, adPassword);

                UserPrincipal userPrincipalItem = UserPrincipal.FindByIdentity(principalContextItem, userId);
           
                // Get all the AD groups for the user (includes parent groups)
                var adGroupItems = userPrincipalItem.GetAuthorizationGroups();

                // Convert groups to string array
                string[] adGroups = adGroupItems.Select(x => x.Name).ToArray<string>();

                return adGroups;
            }
            catch (Exception e)
            {
                if (e is PrincipalServerDownException)
                //if (e is NullReferenceException)
                {
                    throw new FailedAuthorisationException("Failed to find User ID in Active Directory");

                }

                return null;
            }
        }
    }
}