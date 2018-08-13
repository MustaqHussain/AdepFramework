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
using Dwp.Adep.Framework.Management.DataServices;
using Dwp.Adep.Framework.Management.DataServices.Models;

namespace Dwp.Adep.Framework.Management.DataServices.Tests.TestEntityBuilder
{
    public static partial class CommandsBuilder
    {
        #region Create Method
        public static Commands Create()
        {
            return new Commands
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				Command = "test Command",
    				AmendedCommand = "test AmendedCommand",
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static Commands WithCode(this Commands commands, Guid code)
        {
            commands.Code = code;
            return commands;
        }
       	public static Commands WithSecurityLabel(this Commands commands, Guid securityLabel)
        {
            commands.SecurityLabel = securityLabel;
            return commands;
        }
       	public static Commands WithCommand(this Commands commands, String command)
        {
            commands.Command = command;
            return commands;
        }
       	public static Commands WithAmendedCommand(this Commands commands, String amendedCommand)
        {
            commands.AmendedCommand = amendedCommand;
            return commands;
        }
       	public static Commands WithIsActive(this Commands commands, Boolean isActive)
        {
            commands.IsActive = isActive;
            return commands;
        }
       	public static Commands WithOrganisation(this Commands commands, Organisation organisation)
        {
            commands.Organisation = organisation;
            return commands;
        }
    

        #endregion
    }
}