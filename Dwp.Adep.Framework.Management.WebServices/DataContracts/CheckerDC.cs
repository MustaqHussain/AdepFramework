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
    public partial class CheckerDC
    {
        #region Primitive Properties
    
    	[DataMember]
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual Nullable<System.Guid> StaffCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string LastName
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string FirstName
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual bool IsOPIT
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual bool IsCST
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

        #endregion
    }
}
