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

namespace Dwp.Adep.Framework.Management.Web.Models
{
    public partial class CheckerModel
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;
    
        public virtual Nullable<System.Guid> StaffCode
        {
            get { return _staffCode; }
            set { _staffCode = value; }
        }
        private Nullable<System.Guid> _staffCode;
    
        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        private string _lastName;
    
        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        private string _firstName;
    
        public virtual bool IsOPIT
        {
            get { return _isOPIT; }
            set { _isOPIT = value; }
        }
        private bool _isOPIT;
    
        public virtual bool IsCST
        {
            get { return _isCST; }
            set { _isCST = value; }
        }
        private bool _isCST;
    
        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;

        #endregion
    }
}
