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
    public static partial class ServerBuilder
    {
        #region Create Method
        public static Server Create()
        {
            return new Server
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				Number = "test Number",
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static Server WithCode(this Server server, Guid code)
        {
            server.Code = code;
            return server;
        }
       	public static Server WithSecurityLabel(this Server server, Guid securityLabel)
        {
            server.SecurityLabel = securityLabel;
            return server;
        }
       	public static Server WithNumber(this Server server, String number)
        {
            server.Number = number;
            return server;
        }
       	public static Server WithIsActive(this Server server, Boolean isActive)
        {
            server.IsActive = isActive;
            return server;
        }
       	public static Server WithAccuracyCheck(this Server server, ICollection< AccuracyCheck> accuracyCheck)
        {
            server.AccuracyCheck = accuracyCheck;
            return server;
        }
    
       	public static Server WithOrganisation(this Server server, Organisation organisation)
        {
            server.Organisation = organisation;
            return server;
        }
    
       	public static Server WithOrganisation1(this Server server, Organisation organisation1)
        {
            server.Organisation1 = organisation1;
            return server;
        }
    
       	public static Server WithSecurityCheck(this Server server, ICollection< SecurityCheck> securityCheck)
        {
            server.SecurityCheck = securityCheck;
            return server;
        }
    

        #endregion
    }
}
