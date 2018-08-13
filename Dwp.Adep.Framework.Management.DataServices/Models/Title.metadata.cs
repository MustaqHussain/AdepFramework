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
    [MetadataTypeAttribute(typeof(Title.TitleMetadata))]
    public partial class Title : ITitle
    {
    	public partial class TitleMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_Customer_Title", "Code", "TitleCode", IsForeignKey = false)]
    		public virtual ICollection<Customer> Customer
    		{get; set;}
    
    	
    		[Association("FK_Title_Organisation_Security", "SecurityLabel", "Code", IsForeignKey = true)]
    		public virtual Organisation Organisation
    		{get; set;}
    
    		}
    }
}