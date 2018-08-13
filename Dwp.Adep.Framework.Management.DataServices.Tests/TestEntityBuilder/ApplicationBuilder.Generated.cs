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
    public static partial class ApplicationBuilder
    {
        #region Create Method
        public static Application Create()
        {
            return new Application
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				ApplicationName = "test ApplicationName",
    				Location = "test Location",
    				Description = "test Description",
    				IsActive = false,
    				IsSpecificOrganisationAccessRequired = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static Application WithCode(this Application application, Guid code)
        {
            application.Code = code;
            return application;
        }
       	public static Application WithSecurityLabel(this Application application, Guid securityLabel)
        {
            application.SecurityLabel = securityLabel;
            return application;
        }
       	public static Application WithApplicationName(this Application application, String applicationName)
        {
            application.ApplicationName = applicationName;
            return application;
        }
       	public static Application WithLocation(this Application application, String location)
        {
            application.Location = location;
            return application;
        }
       	public static Application WithDescription(this Application application, String description)
        {
            application.Description = description;
            return application;
        }
       	public static Application WithIsActive(this Application application, Boolean isActive)
        {
            application.IsActive = isActive;
            return application;
        }
       	public static Application WithIsSpecificOrganisationAccessRequired(this Application application, Boolean isSpecificOrganisationAccessRequired)
        {
            application.IsSpecificOrganisationAccessRequired = isSpecificOrganisationAccessRequired;
            return application;
        }
       	public static Application WithOrganisation(this Application application, Organisation organisation)
        {
            application.Organisation = organisation;
            return application;
        }
    
       	public static Application WithApplicationAttribute(this Application application, ICollection< ApplicationAttribute> applicationAttribute)
        {
            application.ApplicationAttribute = applicationAttribute;
            return application;
        }
    
       	public static Application WithApplicationOrganisationTypeGroup(this Application application, ICollection< ApplicationOrganisationTypeGroup> applicationOrganisationTypeGroup)
        {
            application.ApplicationOrganisationTypeGroup = applicationOrganisationTypeGroup;
            return application;
        }
    
       	public static Application WithBCSNumber(this Application application, ICollection< BCSNumber> bCSNumber)
        {
            application.BCSNumber = bCSNumber;
            return application;
        }
    
       	public static Application WithReportType(this Application application, ICollection< ReportType> reportType)
        {
            application.ReportType = reportType;
            return application;
        }
    
       	public static Application WithRole(this Application application, ICollection< Role> role)
        {
            application.Role = role;
            return application;
        }
    
       	public static Application WithStaffAttributes(this Application application, ICollection< StaffAttributes> staffAttributes)
        {
            application.StaffAttributes = staffAttributes;
            return application;
        }
    
       	public static Application WithStaffOrganisation(this Application application, ICollection< StaffOrganisation> staffOrganisation)
        {
            application.StaffOrganisation = staffOrganisation;
            return application;
        }
    

        #endregion
    }
}