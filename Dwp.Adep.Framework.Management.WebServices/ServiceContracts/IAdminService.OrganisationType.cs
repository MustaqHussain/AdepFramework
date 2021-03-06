//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ServiceModel;
using Dwp.Adep.Framework.Management.WebServices.DataContracts;
using Dwp.Adep.Framework.Management.WebServices.FaultContracts;

namespace Dwp.Adep.Framework.Management.WebServices.ServiceContracts
{
    public partial interface IAdminService
    {
        #region Behaviour for OrganisationType
    
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	OrganisationTypeVMDC CreateOrganisationType(string userName, string currentUserName, string appID, string overrideID, OrganisationTypeDC dc);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	OrganisationTypeVMDC GetOrganisationType(string userName, string currentUserName, string appID, string overrideID, string code);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	List<OrganisationTypeDC> GetAllOrganisationType(string userName, string currentUserName, string appID, string overrideID, bool includeInActive);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	OrganisationTypeSearchVMDC SearchOrganisationType(string userName, string currentUserName, string appID, string overrideID, OrganisationTypeSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive);
    	
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	OrganisationTypeVMDC UpdateOrganisationType(string userName, string currentUserName, string appID, string overrideID, OrganisationTypeDC dc);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	void DeleteOrganisationType(string userName, string currentUserName, string appID, string overrideID, string dcCode, string lockID);

        #endregion
    }
}
