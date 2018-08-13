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
    [MetadataTypeAttribute(typeof(ApplicationSearchCriteriaModel.ApplicationSearchCriteriaModelMetadata))]
    public partial class ApplicationSearchCriteriaModel
    {
    	public partial class ApplicationSearchCriteriaModelMetadata
    	{
    		[Required]
    		[StringLength(50)]
    		[Display(Name="LABEL_APPLICATION_APPLICATIONNAME", ResourceType=typeof(Resources))]
    		public string ApplicationName {get; set;}
    
      		[Required]
    		[StringLength(300)]
    		[Display(Name="LABEL_APPLICATION_LOCATION", ResourceType=typeof(Resources))]
    		public string Location {get; set;}
    
      		[StringLength(1000)]
    		[Display(Name="LABEL_APPLICATION_DESCRIPTION", ResourceType=typeof(Resources))]
    		public string Description {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_APPLICATION_ISSPECIFICORGANISATIONACCESSREQUIRED", ResourceType=typeof(Resources))]
    		public bool IsSpecificOrganisationAccessRequired {get; set;}
    
        }
    }
}
