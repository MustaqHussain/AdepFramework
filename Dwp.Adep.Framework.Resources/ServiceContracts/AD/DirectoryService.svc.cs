using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dwp.Adep.Framework.Resources.FaultContracts;
using Dwp.Adep.Framework.Resources.DataContracts;

namespace Dwp.Adep.Framework.Resources.ServiceContracts.AD
{
    //NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DirectoryService" in code, svc and config file together.
    public class DirectoryService : IDirectoryService
    {
        public List<string> GetEmailAddress(string firstName, string lastName)
        {
            try
            {
                var searcher = new RealPrincipalSearcher();
                ActiveDirectoryHelper helper = new ActiveDirectoryHelper(searcher);
                return helper.GetEmailAddresses(firstName, lastName);
            }
            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  GetEmailAddress(string firstName:{0}, string lastName{1}); Detailed exception:{2}", firstName, lastName, ex.ToString());
                Exception custom = new Exception(message);                
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Directory Service";
                fault.ProblemType = "Error in GetEmailAddress";

                throw new FaultException<ServiceErrorFault>(fault);

            }
        }


        public ADUser FindEmail(string samAccountName)
        {
            try
            {
                var searcher = new RealPrincipalSearcher();
                ActiveDirectoryHelper helper = new ActiveDirectoryHelper(searcher);
                return helper.GetEmailAddress(samAccountName);
            }
            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  FindEmail(string samAccountName:{0}); Detailed exception:{1}", samAccountName, ex.ToString());
                Exception custom = new Exception(message);                
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Directory Service";
                fault.ProblemType = "Error in FindEmail";

                throw new FaultException<ServiceErrorFault>(fault);                                
            }
        }

        /// <summary>
        /// Returns AD User for passed in Login Id 
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public ADUser SearchUserByLogin(string samAccountName)
        {
            try
            {
                var searcher = new RealPrincipalSearcher();
                ActiveDirectoryHelper helper = new ActiveDirectoryHelper(searcher);
                return helper.SearchUserByLogin(samAccountName);
            }
            catch (Exception ex)
            {
                ///*log erorr locally */
                string message = string.Format("Error in  SearchUserByLogin(string samAccountName:{0}); Detailed exception:{1}", samAccountName, ex.ToString());
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Directory Service";
                fault.ProblemType = "Error in SearchUserByLogin";

                var faultExp = new FaultException<ServiceErrorFault>(fault,ex.ToString());
                

                throw faultExp;
            }
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
            try
            {
                var searcher = new RealPrincipalSearcher();
                ActiveDirectoryHelper helper = new ActiveDirectoryHelper(searcher);
                return helper.SearchUserByName(firstName, lastName);
            }
            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  SearchUserByName(string firstName:{0}, string lastName{1}); Detailed exception:{2}", firstName, lastName, ex.ToString());
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Directory Service";
                fault.ProblemType = "Error in SearchUserByName";

                throw new FaultException<ServiceErrorFault>(fault);

            }
        }

        /// <summary>
        /// Retrives all users in Group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public List<ADUser> GetUsersInGroupMembership(string groupName)
        {
            try
            {
                var searcher = new RealPrincipalSearcher();
                ActiveDirectoryHelper helper = new ActiveDirectoryHelper(searcher);
                return helper.GetUsersInGroupMembership(groupName);
            }
            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  GetUsersInGroupMembership(string groupName:{0} Detailed exception:{1}", groupName, ex.ToString());
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Directory Service";
                fault.ProblemType = "Error in GetUsersInGroupMembership";

                throw new FaultException<ServiceErrorFault>(fault);

            }
        }

        /// <summary>
        /// Find users by login id , supports wild card search
        /// </summary>
        /// <param name="samAccountName"></param>
        /// <returns></returns>
        public List<ADUser> SearchUsersByLogin(string samAccountName)
        {
            try
            {
                var searcher = new RealPrincipalSearcher();
                ActiveDirectoryHelper helper = new ActiveDirectoryHelper(searcher);
                return helper.SearchUsersByLoginId(samAccountName);
            }
            catch (Exception ex)
            {
                /*log erorr locally */
                string message = string.Format("Error in  SearchUsersByLogin(string samAccountName:{0} Detailed exception:{1}", samAccountName, ex.ToString());
                Exception custom = new Exception(message);
                ExceptionManager.HandleException(custom);

                ServiceErrorFault fault = new ServiceErrorFault();
                fault.Operation = "Directory Service";
                fault.ProblemType = "Error in SearchUsersByLogin";

                throw new FaultException<ServiceErrorFault>(fault);

            }
        }
    }
}
