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
using System.ComponentModel.DataAnnotations;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    public partial class AuditSearchCriteriaModel
    {
    
        public virtual string TypeOfObject
        {
    	    get;
            set;
        }
    
        public virtual string AuditAction
        {
    	    get;
            set;
        }
    
        public virtual string ObjectCode
        {
    	    get;
            set;
        }
    
        public virtual Nullable<System.DateTime> DateUpdated
        {
    	    get;
            set;
        }
    
        public virtual string ChangedBy
        {
    	    get;
            set;
        }
    
        public virtual string AuditText
        {
    	    get;
            set;
        }
    }
}
