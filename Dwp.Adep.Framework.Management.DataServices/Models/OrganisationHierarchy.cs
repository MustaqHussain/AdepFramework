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
    public partial class OrganisationHierarchy : IAuditable, IActiveAware 
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual System.Guid AncestorOrganisationCode
        {
            get { return _ancestorOrganisationCode; }
            set
            {
                if (_ancestorOrganisationCode != value)
                {
                    if (Organisation1 != null && Organisation1.Code != value)
                    {
                        Organisation1 = null;
                    }
                    _ancestorOrganisationCode = value;
                }
            }
        }
        private System.Guid _ancestorOrganisationCode;
    
        public virtual System.Guid OrganisationCode
        {
            get { return _organisationCode; }
            set
            {
                if (_organisationCode != value)
                {
                    if (Organisation != null && Organisation.Code != value)
                    {
                        Organisation = null;
                    }
                    _organisationCode = value;
                }
            }
        }
        private System.Guid _organisationCode;
    
        public virtual bool ImmediateParent
        {
            get;
            set;
        }
    
        public virtual Nullable<int> HopsBetweenOrgAndAncestor
        {
            get;
            set;
        }
    
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
    
        public virtual Organisation Organisation1
        {
            get { return _organisation1; }
            set
            {
                if (!ReferenceEquals(_organisation1, value))
                {
                    var previousValue = _organisation1;
                    _organisation1 = value;
                    FixupOrganisation1(previousValue);
                }
            }
        }
        private Organisation _organisation1;

        #endregion
        #region Association Fixup
    
        private void FixupOrganisation(Organisation previousValue)
        {
            if (previousValue != null && previousValue.OrganisationHierarchy.Contains(this))
            {
                previousValue.OrganisationHierarchy.Remove(this);
            }
    
            if (Organisation != null)
            {
                if (!Organisation.OrganisationHierarchy.Contains(this))
                {
                    Organisation.OrganisationHierarchy.Add(this);
                }
                if (OrganisationCode != Organisation.Code)
                {
                    OrganisationCode = Organisation.Code;
                }
            }
        }
    
        private void FixupOrganisation1(Organisation previousValue)
        {
            if (previousValue != null && previousValue.OrganisationHierarchy1.Contains(this))
            {
                previousValue.OrganisationHierarchy1.Remove(this);
            }
    
            if (Organisation1 != null)
            {
                if (!Organisation1.OrganisationHierarchy1.Contains(this))
                {
                    Organisation1.OrganisationHierarchy1.Add(this);
                }
                if (AncestorOrganisationCode != Organisation1.Code)
                {
                    AncestorOrganisationCode = Organisation1.Code;
                }
            }
        }

        #endregion
    }
}
