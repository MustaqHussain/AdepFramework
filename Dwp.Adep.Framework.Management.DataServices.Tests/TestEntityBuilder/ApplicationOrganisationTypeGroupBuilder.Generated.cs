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
    public static partial class ApplicationOrganisationTypeGroupBuilder
    {
        #region Create Method
        public static ApplicationOrganisationTypeGroup Create()
        {
            return new ApplicationOrganisationTypeGroup
            {
    				Code = Guid.NewGuid(),
    				ApplicationCode = Guid.NewGuid(),
    				OrganisationTypeGroupCode = Guid.NewGuid(),
    				IsActive = false,
    				RowIdentifier = null,
    				RootOrganisationForApplicationCode = Guid.NewGuid()
            };
        }

        #endregion
    
        #region With Methods
       	public static ApplicationOrganisationTypeGroup WithCode(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Guid code)
        {
            applicationOrganisationTypeGroup.Code = code;
            return applicationOrganisationTypeGroup;
        }
       	public static ApplicationOrganisationTypeGroup WithApplicationCode(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Guid applicationCode)
        {
            applicationOrganisationTypeGroup.ApplicationCode = applicationCode;
            return applicationOrganisationTypeGroup;
        }
       	public static ApplicationOrganisationTypeGroup WithOrganisationTypeGroupCode(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Guid organisationTypeGroupCode)
        {
            applicationOrganisationTypeGroup.OrganisationTypeGroupCode = organisationTypeGroupCode;
            return applicationOrganisationTypeGroup;
        }
       	public static ApplicationOrganisationTypeGroup WithIsActive(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Boolean isActive)
        {
            applicationOrganisationTypeGroup.IsActive = isActive;
            return applicationOrganisationTypeGroup;
        }
       	public static ApplicationOrganisationTypeGroup WithRootOrganisationForApplicationCode(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Guid rootOrganisationForApplicationCode)
        {
            applicationOrganisationTypeGroup.RootOrganisationForApplicationCode = rootOrganisationForApplicationCode;
            return applicationOrganisationTypeGroup;
        }
       	public static ApplicationOrganisationTypeGroup WithApplication(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Application application)
        {
            applicationOrganisationTypeGroup.Application = application;
            return applicationOrganisationTypeGroup;
        }
    
       	public static ApplicationOrganisationTypeGroup WithOrganisationTypeGroup(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, OrganisationTypeGroup organisationTypeGroup)
        {
            applicationOrganisationTypeGroup.OrganisationTypeGroup = organisationTypeGroup;
            return applicationOrganisationTypeGroup;
        }
    
       	public static ApplicationOrganisationTypeGroup WithOrganisation(this ApplicationOrganisationTypeGroup applicationOrganisationTypeGroup, Organisation organisation)
        {
            applicationOrganisationTypeGroup.Organisation = organisation;
            return applicationOrganisationTypeGroup;
        }
    

        #endregion
    }
}
