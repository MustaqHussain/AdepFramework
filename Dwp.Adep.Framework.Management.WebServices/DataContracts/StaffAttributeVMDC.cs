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
    public partial class StaffAttributeVMDC
    {
    	[DataMember]
        public StaffAttributeDC StaffAttributeItem { get; set;}
    
    	[DataMember]
        public List<StaffAttributeDC> StaffAttributeList { get; set;}
    
    	[DataMember]
    	public List<StaffDC> StaffList { get; set; }
    
    	[DataMember]
    	public List<ApplicationDC> ApplicationList { get; set; }
    
    	[DataMember]
    	public List<ApplicationAttributeDC> ApplicationAttributeList { get; set; }
    
    }
}
