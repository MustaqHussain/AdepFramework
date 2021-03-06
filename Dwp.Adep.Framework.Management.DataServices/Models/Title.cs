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
    public partial class Title
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual System.Guid SecurityLabel
        {
            get { return _securityLabel; }
            set
            {
                if (_securityLabel != value)
                {
                    if (Organisation != null && Organisation.Code != value)
                    {
                        Organisation = null;
                    }
                    _securityLabel = value;
                }
            }
        }
        private System.Guid _securityLabel;
    
        public virtual string Description
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
    
        public virtual ICollection<Customer> Customer
        {
            get
            {
                if (_customer == null)
                {
                    var newCollection = new FixupCollection<Customer>();
                    newCollection.CollectionChanged += FixupCustomer;
                    _customer = newCollection;
                }
                return _customer;
            }
            set
            {
                if (!ReferenceEquals(_customer, value))
                {
                    var previousValue = _customer as FixupCollection<Customer>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupCustomer;
                    }
                    _customer = value;
                    var newValue = value as FixupCollection<Customer>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupCustomer;
                    }
                }
            }
        }
        private ICollection<Customer> _customer;
    
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

        #endregion
        #region Association Fixup
    
        private void FixupOrganisation(Organisation previousValue)
        {
            if (previousValue != null && previousValue.Title.Contains(this))
            {
                previousValue.Title.Remove(this);
            }
    
            if (Organisation != null)
            {
                if (!Organisation.Title.Contains(this))
                {
                    Organisation.Title.Add(this);
                }
                if (SecurityLabel != Organisation.Code)
                {
                    SecurityLabel = Organisation.Code;
                }
            }
        }
    
        private void FixupCustomer(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Customer item in e.NewItems)
                {
                    item.Title = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Customer item in e.OldItems)
                {
                    if (ReferenceEquals(item.Title, this))
                    {
                        item.Title = null;
                    }
                }
            }
        }

        #endregion
    }
}
