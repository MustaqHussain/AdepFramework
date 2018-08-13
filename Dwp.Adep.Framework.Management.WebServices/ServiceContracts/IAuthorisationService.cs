using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.WebServices.FaultContracts;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    [ServiceContract]
    public interface IAuthorisationService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [FaultContract(typeof(AuthorisationFailureFault))]
        [OperationContract]
        AuthorisationDC GetUserAuthorisationInfo(string token, string appID, string[] roles);

        [FaultContract(typeof(ServiceErrorFault))]
        [FaultContract(typeof(AuthorisationFailureFault))]
        [OperationContract]
        string[] GetUserRoles(string currentUser, string user, string appID, string overrideID, string userID, Guid? applicationCode);

        [FaultContract(typeof(ServiceErrorFault))]
        [FaultContract(typeof(AuthorisationFailureFault))]
        [OperationContract]
        List<StaffAccessDC> GetUserApplicationInfo(string currentUser, string user, string appID, string overrideID, string userID, string[] roles);
    }
}
