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
    public partial class StaffOrganisationVMDC
    {
    	[DataMember]
        public StaffOrganisationDC StaffOrganisationItem { get; set;}
    
    	[DataMember]
        public List<StaffOrganisationDC> StaffOrganisationList { get; set;}
    
    	//Code
    	//StaffCode
    	//StaffCode
    	[DataMember]
    	public List<StaffDC> StaffList { get; set; }
    
    	//OrganisationCode
    	//OrganisationCode
    	[DataMember]
    	public List<OrganisationDC> OrganisationList { get; set; }
    
    	//ApplicationCode
    	//ApplicationCode
    	[DataMember]
    	public List<ApplicationDC> ApplicationList { get; set; }
    
    	//IsDefault
    	//IsCurrent
    	//RowIdentifier
    	[DataMember]
    	public string Message { get; set; }
    }
}
