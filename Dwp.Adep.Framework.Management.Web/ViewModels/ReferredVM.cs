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
using Dwp.Adep.Framework.Management.Web.Models;

namespace Dwp.Adep.Framework.Management.Web.ViewModels
{
    public partial class ReferredVM
    {
    	public ReferredModel ReferredItem { get; set; }
    
        public string Message { get; set; }
    
        public string IsDeleteConfirmed { get; set; }
        public string IsExitConfirmed { get; set; }
        public string IsNewConfirmed { get; set; }
    	public bool IsViewDirty { get; set; }
    
        public ReferredAccessContext AccessContext { get; set; }
    	
    }
    
    public enum ReferredAccessContext
    {
        Create,
        View,
        Edit
    }
}