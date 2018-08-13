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
        #region Behaviour for Title
    
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFaultContract))]
    	[OperationContract]
    	TitleVMDC CreateTitle(string userName, string currentUserName, string appID, string overrideID, TitleDC dc);
    
    	[FaultContract(typeof(ServiceErrorFaultContract))]
    	[OperationContract]
    	TitleVMDC GetTitle(string userName, string currentUserName, string appID, string overrideID, string code);
    
    	[FaultContract(typeof(ServiceErrorFaultContract))]
    	[OperationContract]
    	List<TitleDC> GetAllTitle(string userName, string currentUserName, string appID, string overrideID, bool includeInActive);
    
    	[FaultContract(typeof(ServiceErrorFaultContract))]
    	[OperationContract]
    	TitleSearchVMDC SearchTitle(string userName, string currentUserName, string appID, string overrideID, TitleSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFaultContract))]
    	[OperationContract]
    	TitleVMDC UpdateTitle(string userName, string currentUserName, string appID, string overrideID, TitleDC dc);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFaultContract))]
    	[OperationContract]
    	void DeleteTitle(string userName, string currentUserName, string appID, string overrideID, string dcCode, string lockID);

        #endregion
    }
}