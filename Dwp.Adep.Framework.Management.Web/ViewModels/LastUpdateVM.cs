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
    public partial class LastUpdateVM
    {
    	public LastUpdateModel LastUpdateItem { get; set; }
    
        public string Message { get; set; }
    
        public string IsDeleteConfirmed { get; set; }
        public string IsExitConfirmed { get; set; }
        public string IsNewConfirmed { get; set; }
    	public bool IsViewDirty { get; set; }
    
        public LastUpdateAccessContext AccessContext { get; set; }
    	
    }
    
    public enum LastUpdateAccessContext
    {
        Create,
        View,
        Edit
    }
}