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
    public partial class OrganisationDC
    {
    
    	[DataMember]
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual int ID
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Name
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual System.Guid OrganisationTypeCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string HEO
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual Nullable<System.DateTime> DateDeleted
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual bool IsActive
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual byte[] RowIdentifier
        {
    	    get;
            set;
        }
    }
}
