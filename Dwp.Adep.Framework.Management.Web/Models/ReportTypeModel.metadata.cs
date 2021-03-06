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
    [MetadataTypeAttribute(typeof(ReportTypeModel.ReportTypeModelMetadata))]
    public partial class ReportTypeModel : IReportTypeModel
    {
    	public partial class ReportTypeModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_REPORTTYPE_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[StringLength(50)]
    		[Display(Name="LABEL_REPORTTYPE_DESCRIPTION", ResourceType=typeof(Resources))]
    		public string Description {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_REPORTTYPE_APPLICATIONCODE", ResourceType=typeof(Resources))]
    		public System.Guid ApplicationCode {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_REPORTTYPE_ISACTREPORT", ResourceType=typeof(Resources))]
    		public bool IsACTReport {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_REPORTTYPE_ISOPSREPORT", ResourceType=typeof(Resources))]
    		public bool IsOpsReport {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_REPORTTYPE_ISSATREPORT", ResourceType=typeof(Resources))]
    		public bool IsSATReport {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_REPORTTYPE_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}
