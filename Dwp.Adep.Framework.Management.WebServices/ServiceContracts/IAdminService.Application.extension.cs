using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.WebServices.FaultContracts;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    public partial interface IAdminService
    {

     //   [FaultContract(typeof(ServiceErrorFault))]
     //   [OperationContract]
    //    ApplicationVMDC GetApplicationsWithStaffAdmin(string userName, string currentUserName, string appID, string overrideID, string code);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        List<ApplicationDC> GetApplicationsWithStaffAdmin(string userName, string currentUserName, string appID, string overrideID, string[] userRoles, bool includeInActive);

    }
}