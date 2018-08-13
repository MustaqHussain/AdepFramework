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
    [MetadataTypeAttribute(typeof(AuditSearchCriteriaModel.AuditSearchCriteriaModelMetadata))]
    public partial class AuditSearchCriteriaModel
    {
    	public partial class AuditSearchCriteriaModelMetadata
    	{
    		[StringLength(150)]
    		[Display(Name="LABEL_AUDIT_TYPEOFOBJECT", ResourceType=typeof(Resources))]
    		public string TypeOfObject {get; set;}
    
      		[StringLength(50)]
    		[Display(Name="LABEL_AUDIT_AUDITACTION", ResourceType=typeof(Resources))]
    		public string AuditAction {get; set;}
    
      		[StringLength(50)]
    		[Display(Name="LABEL_AUDIT_OBJECTCODE", ResourceType=typeof(Resources))]
    		public string ObjectCode {get; set;}
    
      		[DataType(DataType.Date)]
    		[Display(Name="LABEL_AUDIT_DATEUPDATED", ResourceType=typeof(Resources))]
    		public Nullable<System.DateTime> DateUpdated {get; set;}
    
      		[StringLength(100)]
    		[Display(Name="LABEL_AUDIT_CHANGEDBY", ResourceType=typeof(Resources))]
    		public string ChangedBy {get; set;}
    
      		[Display(Name="LABEL_AUDIT_AUDITTEXT", ResourceType=typeof(Resources))]
    		public string AuditText {get; set;}
    
        }
    }
}
