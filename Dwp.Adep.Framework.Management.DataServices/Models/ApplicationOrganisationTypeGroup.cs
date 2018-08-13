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
    public partial class ApplicationOrganisationTypeGroup : IAuditable, IActiveAware 
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual System.Guid ApplicationCode
        {
            get { return _applicationCode; }
            set
            {
                if (_applicationCode != value)
                {
                    if (Application != null && Application.Code != value)
                    {
                        Application = null;
                    }
                    _applicationCode = value;
                }
            }
        }
        private System.Guid _applicationCode;
    
        public virtual System.Guid OrganisationTypeGroupCode
        {
            get { return _organisationTypeGroupCode; }
            set
            {
                if (_organisationTypeGroupCode != value)
                {
                    if (OrganisationTypeGroup != null && OrganisationTypeGroup.Code != value)
                    {
                        OrganisationTypeGroup = null;
                    }
                    _organisationTypeGroupCode = value;
                }
            }
        }
        private System.Guid _organisationTypeGroupCode;
    
        public virtual System.Guid RootOrganisationForApplicationCode
        {
            get { return _rootOrganisationForApplicationCode; }
            set
            {
                if (_rootOrganisationForApplicationCode != value)
                {
                    if (Organisation != null && Organisation.Code != value)
                    {
                        Organisation = null;
                    }
                    _rootOrganisationForApplicationCode = value;
                }
            }
        }
        private System.Guid _rootOrganisationForApplicationCode;
    
        public virtual bool IsActive
        {
            get;
            set;
        }
    
        public virtual byte[] RowIdentifier
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual Application Application
        {
            get { return _application; }
            set
            {
                if (!ReferenceEquals(_application, value))
                {
                    var previousValue = _application;
                    _application = value;
                    FixupApplication(previousValue);
                }
            }
        }
        private Application _application;
    
        public virtual Organisation Organisation
        {
            get { return _organisation; }
            set
            {
                if (!ReferenceEquals(_organisation, value))
                {
                    var previousValue = _organisation;
                    _organisation = value;
                    FixupOrganisation(previousValue);
                }
            }
        }
        private Organisation _organisation;
    
        public virtual OrganisationTypeGroup OrganisationTypeGroup
        {
            get { return _organisationTypeGroup; }
            set
            {
                if (!ReferenceEquals(_organisationTypeGroup, value))
                {
                    var previousValue = _organisationTypeGroup;
                    _organisationTypeGroup = value;
                    FixupOrganisationTypeGroup(previousValue);
                }
            }
        }
        private OrganisationTypeGroup _organisationTypeGroup;

        #endregion
        #region Association Fixup
    
        private void FixupApplication(Application previousValue)
        {
            if (previousValue != null && previousValue.ApplicationOrganisationTypeGroup.Contains(this))
            {
                previousValue.ApplicationOrganisationTypeGroup.Remove(this);
            }
    
            if (Application != null)
            {
                if (!Application.ApplicationOrganisationTypeGroup.Contains(this))
                {
                    Application.ApplicationOrganisationTypeGroup.Add(this);
                }
                if (ApplicationCode != Application.Code)
                {
                    ApplicationCode = Application.Code;
                }
            }
        }
    
        private void FixupOrganisation(Organisation previousValue)
        {
            if (previousValue != null && previousValue.ApplicationOrganisationTypeGroup.Contains(this))
            {
                previousValue.ApplicationOrganisationTypeGroup.Remove(this);
            }
    
            if (Organisation != null)
            {
                if (!Organisation.ApplicationOrganisationTypeGroup.Contains(this))
                {
                    Organisation.ApplicationOrganisationTypeGroup.Add(this);
                }
                if (RootOrganisationForApplicationCode != Organisation.Code)
                {
                    RootOrganisationForApplicationCode = Organisation.Code;
                }
            }
        }
    
        private void FixupOrganisationTypeGroup(OrganisationTypeGroup previousValue)
        {
            if (previousValue != null && previousValue.ApplicationOrganisationTypeGroup.Contains(this))
            {
                previousValue.ApplicationOrganisationTypeGroup.Remove(this);
            }
    
            if (OrganisationTypeGroup != null)
            {
                if (!OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Contains(this))
                {
                    OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Add(this);
                }
                if (OrganisationTypeGroupCode != OrganisationTypeGroup.Code)
                {
                    OrganisationTypeGroupCode = OrganisationTypeGroup.Code;
                }
            }
        }

        #endregion
    }
}
