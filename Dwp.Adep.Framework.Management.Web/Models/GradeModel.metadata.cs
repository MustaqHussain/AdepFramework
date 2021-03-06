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
    [MetadataTypeAttribute(typeof(GradeModel.GradeModelMetadata))]
    public partial class GradeModel : IGradeModel
    {
    	public partial class GradeModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_GRADE_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[StringLength(10)]
    		[Display(Name="LABEL_GRADE_GRADE1", ResourceType=typeof(Resources))]
    		public string Grade1 {get; set;}
    
      		[DataType(DataType.Date)]
    		[Display(Name="LABEL_GRADE_DATEDELETED", ResourceType=typeof(Resources))]
    		public Nullable<System.DateTime> DateDeleted {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_GRADE_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}
