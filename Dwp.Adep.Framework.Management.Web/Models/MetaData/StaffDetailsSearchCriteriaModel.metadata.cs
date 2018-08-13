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
    [MetadataTypeAttribute(typeof(StaffDetailsSearchCriteriaModel.StaffDetailsSearchCriteriaModelMetadata))]
    public partial class StaffDetailsSearchCriteriaModel
    {
    	public partial class StaffDetailsSearchCriteriaModelMetadata
    	{
    		[Required]
    		[Display(Name="LABEL_STAFFDETAILS_STAFFCODE", ResourceType=typeof(Resources))]
    		public System.Guid StaffCode {get; set;}
    
      		[Display(Name="LABEL_STAFFDETAILS_STAFFOFFICECODE", ResourceType=typeof(Resources))]
    		public Nullable<System.Guid> StaffOfficeCode {get; set;}
    
      		[StringLength(35)]
    		[Display(Name="LABEL_STAFFDETAILS_SECTION", ResourceType=typeof(Resources))]
    		public string Section {get; set;}
    
      		[StringLength(35)]
    		[Display(Name="LABEL_STAFFDETAILS_ROOM", ResourceType=typeof(Resources))]
    		public string Room {get; set;}
    
      		[StringLength(20)]
    		[Display(Name="LABEL_STAFFDETAILS_DIRECTDIALNO", ResourceType=typeof(Resources))]
    		public string DirectDialNo {get; set;}
    
      		[StringLength(35)]
    		[Display(Name="LABEL_STAFFDETAILS_EXTENSION", ResourceType=typeof(Resources))]
    		public string Extension {get; set;}
    
      		[StringLength(100)]
    		[Display(Name="LABEL_STAFFDETAILS_EMAIL", ResourceType=typeof(Resources))]
    		public string Email {get; set;}
    
        }
    }
}