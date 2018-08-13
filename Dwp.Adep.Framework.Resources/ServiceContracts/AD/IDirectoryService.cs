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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDirectoryService" in both code and config file together.
    [ServiceContract]
    public interface IDirectoryService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceErrorFault))]
        List<string> GetEmailAddress(string firstName, string lastName);

        [OperationContract]
        [FaultContract(typeof(ServiceErrorFault))]
        ADUser FindEmail(string samAccountName);

        [OperationContract]
        [FaultContract(typeof(ServiceErrorFault))]
        ADUser SearchUserByLogin(string samAccountName);

        [OperationContract]
        [FaultContract(typeof(ServiceErrorFault))]
        List<ADUser> SearchUsersByLogin(string samAccountName);

        [OperationContract]
        [FaultContract(typeof(ServiceErrorFault))]
        List<ADUser> SearchUserByName(string firstName, string lastName);

        [OperationContract]
        [FaultContract(typeof(ServiceErrorFault))]
        List<ADUser> GetUsersInGroupMembership(string groupName);
    }
}
