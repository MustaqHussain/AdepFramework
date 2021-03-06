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
    [MetadataTypeAttribute(typeof(CommandsModel.CommandsModelMetadata))]
    public partial class CommandsModel : ICommandsModel
    {
    	public partial class CommandsModelMetadata
    	{
    		[Key]
    		[Required]
    		[Display(Name="LABEL_COMMANDS_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[StringLength(50)]
    		[Display(Name="LABEL_COMMANDS_COMMAND", ResourceType=typeof(Resources))]
    		public string Command {get; set;}
    
      		[StringLength(50)]
    		[Display(Name="LABEL_COMMANDS_AMENDEDCOMMAND", ResourceType=typeof(Resources))]
    		public string AmendedCommand {get; set;}
    
      		[Required]
    		[Display(Name="LABEL_COMMANDS_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}
