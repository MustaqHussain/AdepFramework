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
    public partial class HolidayModel : BaseModel
    {
    
        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;
    
        public virtual System.DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private System.DateTime _date;
    
        public virtual bool IsNational
        {
            get { return _isNational; }
            set { _isNational = value; }
        }
        private bool _isNational;
    
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _description;
    
        public virtual bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private bool _isActive;
    
        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;
    }
}
