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

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public partial class NonStandardHolidayVMDC
    {
    	[DataMember]
        public NonStandardHolidayDC NonStandardHolidayItem { get; set;}
    
    	[DataMember]
        public List<NonStandardHolidayDC> NonStandardHolidayList { get; set;}
    
    	//Code
    	//Date
    	//Country
    	//Description
    	//IsActive
    	//RowIdentifier
    	[DataMember]
    	public string Message { get; set; }
    }
}
