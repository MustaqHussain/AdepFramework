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
using Dwp.Adep.Framework.Management.Web.DataAnnotation;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    [MetadataTypeAttribute(typeof(ApplicationModel.ApplicationModelMetadata))]
    public partial class ApplicationModel
    {
    	public partial class ApplicationModelMetadata
    	{
    		[Key]
    		[Required]
    		[Tooltip("TOOLTIP_APPLICATION_CODE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_APPLICATION_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required]
    		[StringLength(50)]
    		[Tooltip("TOOLTIP_APPLICATION_APPLICATIONNAME", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_APPLICATION_APPLICATIONNAME", ResourceType=typeof(Resources))]
    		public string ApplicationName {get; set;}
    
      		[Required]
    		[StringLength(300)]
    		[Tooltip("TOOLTIP_APPLICATION_LOCATION", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_APPLICATION_LOCATION", ResourceType=typeof(Resources))]
    		public string Location {get; set;}
    
      		[StringLength(1000)]
    		[Tooltip("TOOLTIP_APPLICATION_DESCRIPTION", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_APPLICATION_DESCRIPTION", ResourceType=typeof(Resources))]
    		public string Description {get; set;}
    
      		[Required]
    		[Tooltip("TOOLTIP_APPLICATION_ISACTIVE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_APPLICATION_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
      		[Required]
    		[Tooltip("TOOLTIP_APPLICATION_ISSPECIFICORGANISATIONACCESSREQUIRED", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_APPLICATION_ISSPECIFICORGANISATIONACCESSREQUIRED", ResourceType=typeof(Resources))]
    		public bool IsSpecificOrganisationAccessRequired {get; set;}
    
        }
    }
}
