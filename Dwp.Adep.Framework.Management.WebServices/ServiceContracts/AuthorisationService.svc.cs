using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.WebServices.Exceptions;
using Dwp.Adep.Framework.Management.WebServices;
using Dwp.Adep.Framework.Management.WebServices.FaultContracts;
using Dwp.Adep.Framework.Management.WebServices.ServiceContracts.AD;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    public class AuthorisationService : IAuthorisationService
    {
        public AuthorisationService()
        {
            //Initialise bootstrapper
            BootStrapper.InitializeIoc();
        }

        #region GetUserAuthorisationInfo

        public AuthorisationDC GetUserAuthorisationInfo(string token, string appID, string[] roles)
        {
            try
            {
                #region Parameter validation

                //Validate for parameters which cannot be null
                if (null == token) throw new ArgumentNullException("token");
                if (null == appID) throw new ArgumentNullException("appID");
                if (null == roles) throw new ArgumentNullException("roles");

                #endregion

                // Get the domain name from configuration
                string domainName = ConfigurationManager.AppSettings.Get("DomainName");

                // Remove domain name from user name
                token = token.Substring(token.IndexOf("\\") + 1);

                // Token passed was invalid
                if (null == token ) throw new ArgumentOutOfRangeException("token"); 

                //Remove the domain name from the AD groups
                var adGroupsWithoutDomainPrefix = (from x in roles
                                                   select (x.Contains("\\") ? x.Substring(x.LastIndexOf("\\") + 1) : x)).ToArray();

                // Create instance of Staff repository. Note - no way to specify user guids
                IRepository<Staff> staffRepository = new Repository<Staff>("", "", appID, "");

                //Find user
                List<Staff> staffItems = staffRepository.Find(x => x.StaffNumber == token && x.IsActive == true).ToList();

                Staff staffItem = null;

                if (staffItems.Count() != 1)
                {
                    // If zero staff records are found then the user is not in the Staff table
                    throw new FailedAuthorisationException();
                }
                else
                {
                    // One staff member found.
                    staffItem = staffItems[0];
                }

                // Convert to string
                string staffCode = staffItem.Code.ToString();

                // Create instance of ADRoleLookup repository
                IRepository<ADRoleLookup> adRoleLookupRepository = new Repository<ADRoleLookup>(staffCode, staffCode, appID, "");
                IRepository<StaffAttributes> staffAttributesRepository = new Repository<StaffAttributes>(staffCode, staffCode, appID, "");
                
                // Get user authorisation information
                AuthorisationDC returnObject = GetUserAuthorisationInfo(token, appID, adGroupsWithoutDomainPrefix, staffCode, staffItem.FirstName, staffItem.LastName, adRoleLookupRepository, staffAttributesRepository);

                return returnObject;
            }
            catch (ArgumentNullException e)
            {
                // Publish the exception information
                ExceptionManager.PublishException(e);

                //Prevent exception from propogating across the service interface
                throw new FaultException<AuthorisationFailureFault>(new AuthorisationFailureFault(), "Authorisation failure");
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Publish the exception information
                ExceptionManager.PublishException(e);

                //Prevent exception from propogating across the service interface
                throw new FaultException<AuthorisationFailureFault>(new AuthorisationFailureFault(), "Authorisation failure");
            }
            catch (FailedAuthorisationException)
            {
                //Prevent exception from propogating across the service interface
                throw new FaultException<AuthorisationFailureFault>(new AuthorisationFailureFault(), "Authorisation failure");
            }
            catch (Exception e)
            {
                if (!(e is FaultException))
                {
                    // Publish the exception information
                    ExceptionManager.PublishException(e);

                    // Throw default fault exception
                    throw new FaultException<ServiceErrorFault>(new ServiceErrorFault(), "The service experienced a serious error.");
                }

                return null;
            }
        }

        public AuthorisationDC GetUserAuthorisationInfo(string token, string appID, string[] adGroups, string staffCode, string firstName, string lastName, IRepository<ADRoleLookup> adRoleLookupRepository,IRepository<StaffAttributes> staffAttributesRepository)
        {
            try
            {
                #region Parameter validation

                //Validate for parameters which cannot be null
                if (null == token) throw new ArgumentNullException("token");
                if (null == adGroups) throw new ArgumentNullException("adGroups");
                if (null == staffCode) throw new ArgumentNullException("staffCode");
                if (null == adRoleLookupRepository) throw new ArgumentNullException("adRoleLookupRepository");

                #endregion

                // Call overload with injected objects
                string[] MatchingRoles = GetRolesForADGroups(staffCode, staffCode, appID, "", adGroups, adRoleLookupRepository);

                //***************************CHANGE TO GET ROLES FROM STAFF ATTRIBUTES********************************
                //*****************THE ONLY ROLE IN AD IS NOW ONE GIVING ACCESS TO THE APPLICATION USED BY LANDING PAGE********************
                Guid StaffCodeGuid = Guid.Parse(staffCode);

                List<StaffAttributes> attributes = staffAttributesRepository.Find(new Specification<StaffAttributes>(x => x.ApplicationAttribute.IsRole && x.LookupValue == "Yes" && x.StaffCode == StaffCodeGuid), x => x.ApplicationAttribute.AttributeName, "ApplicationAttribute", "Application", "ApplicationAttribute.ApplicationAttributeExtension").ToList();
                MatchingRoles = MatchingRoles.Concat<string>(attributes.Select(x => x.ApplicationAttribute.AttributeName)).ToArray();

                //add pseudo Staff Maintenance role if user is staff admin. This role in used in custom Authorization checks 
                if (attributes.Where(x => x.ApplicationAttribute.ApplicationAttributeExtension.Any(y => y.IsStaffAdmin == true)).Count() > 0)
                {
                    List<String> matchingRolesList = new List<string>(MatchingRoles);
                    matchingRolesList.Add(FrameworkRoles.STAFF_MAINTAINANCE);
                    MatchingRoles = matchingRolesList.ToArray();   
                }

                //Return user information
                AuthorisationDC returnObject = new AuthorisationDC() { Roles = MatchingRoles.AsEnumerable<string>(), UserID = staffCode, UserName = firstName + " " + lastName };

                return returnObject;
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);
                return null;
            }

        }

        #endregion

        #region GetUserRoles

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string[] GetUserRoles(string currentUser, string user, string appID, string overrideID, string userID, Guid? applicationCode)
        {
            // Create instance of ADRoleLookup repository
            IRepository<ADRoleLookup> adRoleLookupRepository = new Repository<ADRoleLookup>(currentUser, user, appID, overrideID);
            IRepository<StaffAttributes> staffAttributesRepository = new Repository<StaffAttributes>(currentUser, user, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(currentUser, user, appID, overrideID);
            IPrincipalManager principalManager = new PrincipalManager();
            IActiveDirectoryHelper activeDirectoryHelper = new ActiveDirectoryHelper(staffRepository, principalManager);

            // Call overload with injected objects
            return GetUserRoles(currentUser, user, appID, overrideID, userID, applicationCode, adRoleLookupRepository, staffAttributesRepository, activeDirectoryHelper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="userID"></param>
        /// <param name="adRoleLookupRepository"></param>
        /// <returns></returns>
        /// <remarks>*** THE ONLY ROLE IN AD IS NOW ONE GIVING ACCESS TO THE APPLICATION USED BY LANDING PAGE ***</remarks>
        public string[] GetUserRoles(string currentUser, string user, string appID, string overrideID, string userID, Guid? applicationCode,
            IRepository<ADRoleLookup> adRoleLookupRepository, IRepository<StaffAttributes> staffAttributesRepository, IActiveDirectoryHelper activeDirectoryHelper)
        {
            try
            {
                // Create instance of Staff repository. Note - no way to specify user guids
                Guid staffCodeGuid = Guid.Parse(userID);
//#if DEBUG
//                string[] adGroups = new string[] { "DWP-FW-ADMIN", "DWP-FW-APPLICATION", "DWP-UCB-APPLICATION", "DWP-DMACR-APPLICATION","DWP-BCAS-APPLCIATION" };
//#else
                // Get list of AD groups for user
               string[] adGroups = activeDirectoryHelper.GetADGroups(userID);
//#endif
                // Get roles for AD groups
                string[] MatchingRoles =  GetApplicationRolesForADGroups(currentUser, user, appID, overrideID, applicationCode, adGroups, adRoleLookupRepository);

                //***************************CHANGE TO GET ROLES FROM STAFF ATTRIBUTES********************************
                //*****************THE ONLY ROLE IN AD IS NOW ONE GIVING ACCESS TO THE APPLICATION USED BY LANDING PAGE********************
                 
                List<StaffAttributes> attributes = staffAttributesRepository.Find(new Specification<StaffAttributes>(x => x.ApplicationAttribute.IsRole && x.LookupValue == "Yes" && x.StaffCode == staffCodeGuid), x => x.ApplicationAttribute.AttributeName, "ApplicationAttribute", "Application").ToList();
                MatchingRoles = MatchingRoles.Concat<string>(attributes.Select(x => x.ApplicationAttribute.AttributeName)).ToArray();

                return MatchingRoles;
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region GetUserApplicationInfo

        /// <summary>
        /// Retrieve list of applications and related information to which the user has access to.
        /// </summary>
        /// <param name="currentUser">Current user guid/</param>
        /// <param name="user">Current user or alternate user guid/</param>
        /// <param name="appID">ID for the current Application being used</param>
        /// <param name="overrideID">Override ID</param>
        /// <param name="userID">Guid for user for whom we would like to retrieve application access information</param>
        /// <param name="roles">Active Directory groups associated with the user for whom we are determinin application access for</param>
        /// <returns>List of StaffAccessDC items for the current user</returns>
        public List<StaffAccessDC> GetUserApplicationInfo(string currentUser, string user, string appID, string overrideID, string userID, string[] roles)
        {
            // Create repositories
            IRepository<StaffOrganisation> staffOrganisationRepository = new Repository<StaffOrganisation>(currentUser, user, appID, overrideID);
            IRepository<ADRoleLookup> adRoleLookupRepository = new Repository<ADRoleLookup>(currentUser, user, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(currentUser, user, appID, overrideID);

            //Remove the domain name from the AD groups
            var adGroupsWithoutDomainPrefix = (from x in roles
                                               select (x.Contains("\\") ? x.Substring(x.LastIndexOf("\\") + 1) : x)).ToArray();
            // Call overload with injected objects
            return GetUserApplicationInfo(currentUser, user, appID, overrideID, userID, adGroupsWithoutDomainPrefix, staffRepository, staffOrganisationRepository, adRoleLookupRepository);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roles"></param>
        /// <param name="staffRepository"></param>
        /// <param name="staffOrganisationRepository"></param>
        /// <returns></returns>
        public List<StaffAccessDC> GetUserApplicationInfo(string currentUser, string user, string appID, string overrideID, string userID, string[] adGroups, 
            IRepository<Staff> staffRepository, IRepository<StaffOrganisation> staffOrganisationRepository, IRepository<ADRoleLookup> adRoleLookupRepository)
        {           

            try
            {
                #region Parameter validation

                //Validate for parameters which cannot be null
                if (null == userID) throw new ArgumentNullException("userID");
                if (null == staffRepository) throw new ArgumentNullException("staffRepository");
                if (null == staffOrganisationRepository) throw new ArgumentNullException("staffOrganisationRepository");

                // userID is a Guid
                Guid userGuid;
                bool isParsed = Guid.TryParse(userID, out userGuid);
                if (!isParsed) throw new ArgumentOutOfRangeException("userID");

                #endregion

                //Find user
                Staff staffItem = staffRepository.Single(x => x.Code == userGuid);

                // Remove domain prefix if present
                adGroups = adGroups.Select(x => x.Substring(("\\" + x).LastIndexOf('\\'))).ToArray();

                IRepository<Role> roleRepository = new Repository<Role>(currentUser, user, appID, overrideID);
                IEnumerable<Role> roles =  roleRepository.Find(x => x.ADRoleLookup.Any(y => adGroups.Contains(y.ADGroup)),"Application");
                List<Guid> applicationCodes = roles.Select(x => x.ApplicationCode).Distinct().ToList<Guid>();

                // Build specification for retrieving Staff Organisations
                ISpecification<StaffOrganisation> specificationAppCodes = new Specification<StaffOrganisation>(x => applicationCodes.Contains(x.ApplicationCode));
                ISpecification<StaffOrganisation> specificationStaff = new Specification<StaffOrganisation>(x => x.StaffCode == staffItem.Code);
                ISpecification<StaffOrganisation> specification = specificationAppCodes.And(specificationStaff);

                // Retrieve applications that the user has access to including the organisations that they are associated with for these applications
                IEnumerable<StaffOrganisation> staffOrganisations = staffOrganisationRepository.Find(specification, x => new { x.Application.ApplicationName }, "Application", "Staff", "Organisation");

                // Initialise ID. This is an identifier for each item in the list.
                int IDCount = 1;

                List<StaffAccessDC> staffAccessList = new List<StaffAccessDC>();

                // Map staff access information to data contracts
                foreach (StaffOrganisation staffOrganisationItem in staffOrganisations)
                {
                    // Current location of staff member within organisation is critical to this Application and the staff member is associated with more than on Org in the hierarchy
                    // therefore need to tag Organisation
                    if (staffOrganisationItem.Application.IsSpecificOrganisationAccessRequired &&
                        staffOrganisations.Where(x => x.ApplicationCode == staffOrganisationItem.ApplicationCode).Count() > 1)
                    {
                        // Populate staff access data contract
                        StaffAccessDC staffAccessItem = new StaffAccessDC();
                        staffAccessItem.ID = IDCount++;
                        staffAccessItem.ApplicationName = staffOrganisationItem.Application.ApplicationName;
                        staffAccessItem.Description = staffOrganisationItem.Application.Description;
                        staffAccessItem.Location = staffOrganisationItem.Application.Location;
                        staffAccessItem.OrganisationName = staffOrganisationItem.Organisation.Name;
                        staffAccessItem.OrganisationID = staffOrganisationItem.Organisation.ID;
                        staffAccessItem.IsSpecificOrganisationAccessRequired = staffOrganisationItem.Application.IsSpecificOrganisationAccessRequired;

                        // Add to staff access list
                        staffAccessList.Add(staffAccessItem);
                    }

                    // Current location of staff member within organisation not critical to this Application or...
                    // Current location of staff member within organisation is critical to this Application but the staff member is associated with only one Org in the hierarchy
                    // therefore don't need to tag organisation
                    if (((staffOrganisationItem.Application.IsSpecificOrganisationAccessRequired &&
                        staffOrganisations.Where(x => x.ApplicationCode == staffOrganisationItem.ApplicationCode).Count() == 1)) ||
                        (!staffOrganisationItem.Application.IsSpecificOrganisationAccessRequired &&
                        !staffAccessList.Any(x => x.ApplicationName == staffOrganisationItem.Application.ApplicationName)))
                    {
                        // Populate staff access data contract
                        StaffAccessDC staffAccessItem = new StaffAccessDC();
                        staffAccessItem.ID = IDCount++;
                        staffAccessItem.ApplicationName = staffOrganisationItem.Application.ApplicationName;
                        staffAccessItem.Description = staffOrganisationItem.Application.Description;
                        staffAccessItem.Location = staffOrganisationItem.Application.Location;
                        staffAccessItem.OrganisationName = string.Empty;
                        staffAccessItem.OrganisationID = 0;
                        staffAccessItem.IsSpecificOrganisationAccessRequired = staffOrganisationItem.Application.IsSpecificOrganisationAccessRequired;

                        // Add to staff access list
                        staffAccessList.Add(staffAccessItem);
                    }

                }

                // Map staff access information for any applications where the user has access based on role but where no association between user and organisation has been found
                foreach (Role roleItem in roles)
                {
                    if (!staffAccessList.Any(x => x.ApplicationName == roleItem.Application.ApplicationName))
                    {
                        // Do not provide a button for the Framework app
                        if (!(roleItem.Application.ApplicationName == ConfigurationManager.AppSettings.Get("FrameworkAppName")))
                        {
                            // Populate staff access data contract
                            StaffAccessDC staffAccessItem = new StaffAccessDC();
                            staffAccessItem.ID = IDCount++;
                            staffAccessItem.ApplicationName = roleItem.Application.ApplicationName;
                            staffAccessItem.Description = roleItem.Application.Description;
                            staffAccessItem.Location = roleItem.Application.Location;
                            staffAccessItem.OrganisationName = string.Empty;
                            staffAccessItem.OrganisationID = 0;
                            staffAccessItem.IsSpecificOrganisationAccessRequired = false;

                            // Add to staff access list
                            staffAccessList.Add(staffAccessItem);
                        }
                    }
                }

                // Order list by application name
                staffAccessList = staffAccessList.OrderBy(x => x.ApplicationName).ToList();

                // Return staff application access list
                return staffAccessList;
            }
            catch (Exception e)
            {
                ExceptionManager.ShieldException(e);
                return null;
            }
        }

        #endregion

        #region GetRolesForADGroups

        private string[] GetRolesForADGroups(string currentUser, string user, string appID, string overrideID, string[] adGroups, IRepository<ADRoleLookup> adRoleLookupRepository)
        {
            // Get roles based on AD groups
            IEnumerable<ADRoleLookup> adRoleItems = adRoleLookupRepository.Find(x => adGroups.Contains(x.ADGroup), "Role");

            // Retrieve role names
            string[] roles = adRoleItems.Select(x => x.Role.RoleName).ToArray<string>();

            return roles;
        }

        #endregion

        #region GetApplicationRolesForADGroups

        private string[] GetApplicationRolesForADGroups(string currentUser, string user, string appID, string overrideID, Guid? applicationCode,string[] adGroups, IRepository<ADRoleLookup> adRoleLookupRepository)
        {
            // Get roles based on AD groups
            Specification<ADRoleLookup> userRolesForApp = new Specification<ADRoleLookup>(x => adGroups.Contains(x.ADGroup) && x.Role.ApplicationCode == applicationCode);

            IEnumerable<ADRoleLookup> adRoleItems = adRoleLookupRepository.Find(userRolesForApp, x=>x.Role.RoleName,"Role");

            // Retrieve role names
            string[] roles = adRoleItems.Select(x => x.Role.RoleName).ToArray<string>();

            return roles;
        }

        #endregion


        

    }
}
