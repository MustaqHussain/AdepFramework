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
    public partial class StaffDetailsVMDC
    {
    	[DataMember]
        public StaffDetailsDC StaffDetailsItem { get; set;}
    
    	[DataMember]
        public List<StaffDetailsDC> StaffDetailsList { get; set;}
    
    	//Code
    	//StaffCode
    	//StaffCode
    	[DataMember]
    	public List<StaffDC> StaffList { get; set; }
    
    	//StaffOfficeCode
    	//StaffOfficeCode
    	[DataMember]
    	public List<StaffOfficesDC> StaffOfficeList { get; set; }
    
    	//Section
    	//Room
    	//DirectDialNo
    	//Extension
    	//Email
    	//RowIdentifier
    	[DataMember]
    	public string Message { get; set; }
    }
}
