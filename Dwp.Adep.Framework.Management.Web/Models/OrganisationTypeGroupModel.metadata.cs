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
    [MetadataTypeAttribute(typeof(OrganisationTypeGroupModel.OrganisationTypeGroupModelMetadata))]
    public partial class OrganisationTypeGroupModel : IOrganisationTypeGroupModel
    {
    	public partial class OrganisationTypeGroupModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_ORGANISATIONTYPEGROUP_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required]
    		[StringLength(50)]
    		[Display(Name="LABEL_ORGANISATIONTYPEGROUP_NAME", ResourceType=typeof(Resources))]
    		public string Name {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_ORGANISATIONTYPEGROUP_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}
