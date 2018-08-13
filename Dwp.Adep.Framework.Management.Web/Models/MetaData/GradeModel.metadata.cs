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
    [MetadataTypeAttribute(typeof(GradeModel.GradeModelMetadata))]
    public partial class GradeModel
    {
    	public partial class GradeModelMetadata
    	{
    		[Key]
    		[Required]
    		[Tooltip("TOOLTIP_GRADE_CODE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_GRADE_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required]
    		[StringLength(10)]
    		[Tooltip("TOOLTIP_GRADE_GRADE1", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_GRADE_GRADE1", ResourceType=typeof(Resources))]
    		public string Grade1 {get; set;}
    
      		[Required]
    		[Tooltip("TOOLTIP_GRADE_ISACTIVE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_GRADE_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}
