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
    public static partial class OrganisationTypeGroupBuilder
    {
        #region Create Method
        public static OrganisationTypeGroup Create()
        {
            return new OrganisationTypeGroup
            {
    				Code = Guid.NewGuid(),
    				Name = "test Name",
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static OrganisationTypeGroup WithCode(this OrganisationTypeGroup organisationTypeGroup, Guid code)
        {
            organisationTypeGroup.Code = code;
            return organisationTypeGroup;
        }
       	public static OrganisationTypeGroup WithName(this OrganisationTypeGroup organisationTypeGroup, String name)
        {
            organisationTypeGroup.Name = name;
            return organisationTypeGroup;
        }
       	public static OrganisationTypeGroup WithIsActive(this OrganisationTypeGroup organisationTypeGroup, Boolean isActive)
        {
            organisationTypeGroup.IsActive = isActive;
            return organisationTypeGroup;
        }
       	public static OrganisationTypeGroup WithApplicationOrganisationTypeGroup(this OrganisationTypeGroup organisationTypeGroup, ICollection< ApplicationOrganisationTypeGroup> applicationOrganisationTypeGroup)
        {
            organisationTypeGroup.ApplicationOrganisationTypeGroup = applicationOrganisationTypeGroup;
            return organisationTypeGroup;
        }
    
       	public static OrganisationTypeGroup WithOrganisationType(this OrganisationTypeGroup organisationTypeGroup, ICollection< OrganisationType> organisationType)
        {
            organisationTypeGroup.OrganisationType = organisationType;
            return organisationTypeGroup;
        }
    

        #endregion
    }
}