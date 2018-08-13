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
    public static partial class ADRoleLookupBuilder
    {
        #region Create Method
        public static ADRoleLookup Create()
        {
            return new ADRoleLookup
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				ADGroup = "test ADGroup",
    				RoleCode = Guid.NewGuid(),
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static ADRoleLookup WithCode(this ADRoleLookup aDRoleLookup, Guid code)
        {
            aDRoleLookup.Code = code;
            return aDRoleLookup;
        }
       	public static ADRoleLookup WithSecurityLabel(this ADRoleLookup aDRoleLookup, Guid securityLabel)
        {
            aDRoleLookup.SecurityLabel = securityLabel;
            return aDRoleLookup;
        }
       	public static ADRoleLookup WithADGroup(this ADRoleLookup aDRoleLookup, String aDGroup)
        {
            aDRoleLookup.ADGroup = aDGroup;
            return aDRoleLookup;
        }
       	public static ADRoleLookup WithRoleCode(this ADRoleLookup aDRoleLookup, Guid roleCode)
        {
            aDRoleLookup.RoleCode = roleCode;
            return aDRoleLookup;
        }
       	public static ADRoleLookup WithIsActive(this ADRoleLookup aDRoleLookup, Boolean isActive)
        {
            aDRoleLookup.IsActive = isActive;
            return aDRoleLookup;
        }
       	public static ADRoleLookup WithOrganisation(this ADRoleLookup aDRoleLookup, Organisation organisation)
        {
            aDRoleLookup.Organisation = organisation;
            return aDRoleLookup;
        }
    
       	public static ADRoleLookup WithRole(this ADRoleLookup aDRoleLookup, Role role)
        {
            aDRoleLookup.Role = role;
            return aDRoleLookup;
        }
    

        #endregion
    }
}