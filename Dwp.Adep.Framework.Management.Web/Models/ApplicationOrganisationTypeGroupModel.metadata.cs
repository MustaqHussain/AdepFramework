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
    [MetadataTypeAttribute(typeof(ApplicationOrganisationTypeGroupModel.ApplicationOrganisationTypeGroupModelMetadata))]
    public partial class ApplicationOrganisationTypeGroupModel : IApplicationOrganisationTypeGroupModel
    {
    	public partial class ApplicationOrganisationTypeGroupModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_APPLICATIONORGANISATIONTYPEGROUP_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_APPLICATIONORGANISATIONTYPEGROUP_APPLICATIONCODE", ResourceType=typeof(Resources))]
    		public System.Guid ApplicationCode {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_APPLICATIONORGANISATIONTYPEGROUP_ORGANISATIONTYPEGROUPCODE", ResourceType=typeof(Resources))]
    		public System.Guid OrganisationTypeGroupCode {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_APPLICATIONORGANISATIONTYPEGROUP_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_APPLICATIONORGANISATIONTYPEGROUP_ROOTORGANISATIONFORAPPLICATIONCODE", ResourceType=typeof(Resources))]
    		public System.Guid RootOrganisationForApplicationCode {get; set;}
    
        }
    }
}
