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
    public partial class GradeSearchMatchDC
    {
    
    	[DataMember]
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Grade1
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
    }
}
