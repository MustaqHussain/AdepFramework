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
    [MetadataTypeAttribute(typeof(HolidaySearchCriteriaModel.HolidaySearchCriteriaModelMetadata))]
    public partial class HolidaySearchCriteriaModel
    {
    	public partial class HolidaySearchCriteriaModelMetadata
    	{
    		[Required]
    		[Display(Name="LABEL_HOLIDAY_DATE", ResourceType=typeof(Resources))]
    		public System.DateTime Date {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_HOLIDAY_ISNATIONAL", ResourceType=typeof(Resources))]
    		public bool IsNational {get; set;}
    
      		[Required]
    		[StringLength(30)]
    		[Display(Name="LABEL_HOLIDAY_DESCRIPTION", ResourceType=typeof(Resources))]
    		public string Description {get; set;}
    
        }
    }
}