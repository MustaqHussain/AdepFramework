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
using Dwp.Adep.Framework.Management.ResourceLibrary;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    [MetadataTypeAttribute(typeof(RoleModel.RoleModelMetadata))]
    public partial class RoleModel : IRoleModel
    {
    	public partial class RoleModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_ROLE_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_ROLE_APPLICATIONCODE", ResourceType=typeof(Resources))]
    		public System.Guid ApplicationCode {get; set;}
    
      		[Required]
    		[StringLength(50)]
    		[Display(Name="LABEL_ROLE_ROLENAME", ResourceType=typeof(Resources))]
    		public string RoleName {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_ROLE_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}