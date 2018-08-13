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

namespace Dwp.Adep.Framework.Management.Web.Models
{
    //[MetadataTypeAttribute(typeof(CheckerModel.CheckerModelMetadata))]
    public partial class CheckerModel : ICheckerModel
    {
    	public partial class CheckerModelMetadata
    	{
    		[Key]
    		[Display(Name="LABEL_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_STAFFCODE", ResourceType=typeof(Resources))]
    		public Nullable<System.Guid> StaffCode {get; set;}
    
      		[Required]
    		[StringLength(35)]
    		[Display(Name="LABEL_LASTNAME", ResourceType=typeof(Resources))]
    		public string LastName {get; set;}
    
      		[Required]
    		[StringLength(35)]
    		[Display(Name="LABEL_FIRSTNAME", ResourceType=typeof(Resources))]
    		public string FirstName {get; set;}
    
      		[Display(Name="LABEL_ISOPIT", ResourceType=typeof(Resources))]
    		public bool IsOPIT {get; set;}
    
      		[Display(Name="LABEL_ISCST", ResourceType=typeof(Resources))]
    		public bool IsCST {get; set;}
    
      		[StringLength(8)]
    		[Display(Name="LABEL_ROWIDENTIFIER", ResourceType=typeof(Resources))]
    		public byte[] RowIdentifier {get; set;}
    
        }
    }
}