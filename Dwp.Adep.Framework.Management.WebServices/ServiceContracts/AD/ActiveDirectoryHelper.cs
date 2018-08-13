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
    public class ActiveDirectoryHelper :  IActiveDirectoryHelper
    {
        IRepository<Staff> _staffRepository;
        IPrincipalManager _principalManager;

        public ActiveDirectoryHelper(IRepository<Staff> staffRepository, IPrincipalManager principalManager)
        {
            #region Parameter validation

            // Validate parameters
            if (null == staffRepository) throw new ArgumentNullException("staffRepository");
            if (null == principalManager) throw new ArgumentNullException("principalManager");

            #endregion

            _staffRepository = staffRepository;
            _principalManager = principalManager;
        }

        #region GetADGroups

        /// <summary>
        /// Retrieve a list of AD groups for the specified userID
        /// </summary>
        /// <param name="userID">The Staff guid of the user for whom we are finding groups</param>
        /// <returns>List of AD groups the user (userID) is a member of</returns>
        public string[] GetADGroups(string userID)
        {
            #region Parameter validation

            // Validate parameters
            if (string.IsNullOrEmpty(userID)) throw new ArgumentOutOfRangeException("userID");

            #endregion

            // Convert userid to guid
            Guid userIdGuid = Guid.Parse(userID);

            // Get member of staff
            Staff staffItem = _staffRepository.Single(x => x.Code == userIdGuid);

            // Get staff number of staff member which is the AD logon
            string staffNumber = staffItem.StaffNumber;

            // Get Active Directory Groups for user
            string[] adGroups = _principalManager.GetGroups(staffNumber);

            // Return groups
            return adGroups;
        }

        #endregion
    }
}