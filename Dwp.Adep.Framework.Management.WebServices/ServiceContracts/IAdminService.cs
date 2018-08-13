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
    public partial interface IAdminService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        StaffApplicationAdminVMDC GetStaffApplicationAdministrationByStaffCodeAndApplicationCode(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode, Guid staffCode);

        [OperationContract]
        ApplicationOrganisationSelectVMDC GetApplicationOrganisationsByApplicationCode(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode);

        [OperationContract]
        ApplicationOrganisationAdminVMDC GetOrganisationWithParent(string currentUser, string user, string appID, string overrideID, Guid organisationCode, Guid applicationCode);

        [OperationContract]
        ApplicationOrganisationAdminVMDC UpdateOrganisationForApplication(string currentUser, string user, string appID, string overrideID, OrganisationDC dc, Guid parentOrganisationCode, Guid applicationCode);

        [OperationContract]
        ApplicationOrganisationAdminVMDC CreateOrganisationForApplication(string currentUser, string user, string appID, string overrideID, OrganisationDC dc, Guid parentOrganisationCode, Guid applicationCode);

        [OperationContract]
        void DeleteOrganisationForApplication(string currentUser, string user, string appID, string overrideID, Guid code, string lockID);

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        StaffApplicationAdminVMDC UpdateStaffApplicationAdministration(string userName, string currentUserName, string appID, string overrideID, Guid applicationCode, StaffDC staffItem, StaffAdminChangeSetDC staffChangeSet);

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void UpdateStaffCurrentOrganisation(string currentUser, string user, string appID, string overrideID, string applicationName, int organisationID);
    }
}
