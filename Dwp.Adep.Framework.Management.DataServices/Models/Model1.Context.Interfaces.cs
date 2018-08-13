﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace Dwp.Adep.Framework.Management.DataServices.Models
{
    public interface IAdepDatabaseEntities : IDisposable, IObjectContext
    {
        #region ObjectSet Properties
        IObjectSet<ADRoleLookup> ADRoleLookup {get;}
        IObjectSet<Application> Application {get;}
        IObjectSet<ApplicationAttribute> ApplicationAttribute {get;}
        IObjectSet<ApplicationOrganisationTypeGroup> ApplicationOrganisationTypeGroup {get;}
        IObjectSet<Audit> Audit {get;}
        IObjectSet<Country> Country {get;}
        IObjectSet<Grade> Grade {get;}
        IObjectSet<Holiday> Holiday {get;}
        IObjectSet<Organisation> Organisation {get;}
        IObjectSet<OrganisationHierarchy> OrganisationHierarchy {get;}
        IObjectSet<OrganisationType> OrganisationType {get;}
        IObjectSet<OrganisationTypeGroup> OrganisationTypeGroup {get;}
        IObjectSet<Role> Role {get;}
        IObjectSet<Staff> Staff {get;}
        IObjectSet<StaffAttributes> StaffAttributes {get;}
        IObjectSet<StaffDetails> StaffDetails {get;}
        IObjectSet<StaffOffices> StaffOffices {get;}
        IObjectSet<StaffOrganisation> StaffOrganisation {get;}
        IObjectSet<UploadErrorLog> UploadErrorLog {get;}
        IObjectSet<UploadQueue> UploadQueue {get;}
        IObjectSet<ApplicationAttributeExtension> ApplicationAttributeExtension {get;}
        IObjectSet<NonStandardHoliday> NonStandardHoliday {get;}

        #endregion
    }
}
