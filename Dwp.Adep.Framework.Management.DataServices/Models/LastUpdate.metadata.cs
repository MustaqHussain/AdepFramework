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

namespace Dwp.Adep.Framework.Management.DataServices.Models
{
    [MetadataTypeAttribute(typeof(LastUpdate.LastUpdateMetadata))]
    public partial class LastUpdate : ILastUpdate
    {
    	public partial class LastUpdateMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_LastUpdate_Organisation_Security", "SecurityLabel", "Code", IsForeignKey = true)]
    		public virtual Organisation Organisation
    		{get; set;}
    
    		}
    }
}
