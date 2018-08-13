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
        #region Behaviour for NonStandardHoliday
    
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	NonStandardHolidayVMDC CreateNonStandardHoliday(string userName, string currentUserName, string appID, string overrideID, NonStandardHolidayDC dc);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	NonStandardHolidayVMDC GetNonStandardHoliday(string userName, string currentUserName, string appID, string overrideID, string code);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	List<NonStandardHolidayDC> GetAllNonStandardHoliday(string userName, string currentUserName, string appID, string overrideID, bool includeInActive);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	NonStandardHolidaySearchVMDC SearchNonStandardHoliday(string userName, string currentUserName, string appID, string overrideID, NonStandardHolidaySearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive);
    	
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	NonStandardHolidayVMDC UpdateNonStandardHoliday(string userName, string currentUserName, string appID, string overrideID, NonStandardHolidayDC dc);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	void DeleteNonStandardHoliday(string userName, string currentUserName, string appID, string overrideID, string dcCode, string lockID);

        #endregion
    }
}
