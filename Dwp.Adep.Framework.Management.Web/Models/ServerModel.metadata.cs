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
    [MetadataTypeAttribute(typeof(ServerModel.ServerModelMetadata))]
    public partial class ServerModel : IServerModel
    {
    	public partial class ServerModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_SERVER_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[StringLength(7)]
    		[Display(Name="LABEL_SERVER_NUMBER", ResourceType=typeof(Resources))]
    		public string Number {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_SERVER_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}
