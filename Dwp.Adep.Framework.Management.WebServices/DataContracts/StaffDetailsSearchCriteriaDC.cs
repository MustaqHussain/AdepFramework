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
    public partial class StaffDetailsSearchCriteriaDC
    {
    
    	[DataMember]
        public virtual System.Guid StaffCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Staff
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual Nullable<System.Guid> StaffOfficeCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string StaffOffice
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Section
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Room
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string DirectDialNo
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Extension
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Email
        {
    	    get;
            set;
        }
    }
}