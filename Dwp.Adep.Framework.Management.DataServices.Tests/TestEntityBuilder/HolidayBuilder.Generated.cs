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
    public static partial class HolidayBuilder
    {
        #region Create Method
        public static Holiday Create()
        {
            return new Holiday
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				Date = DateTime.Now,
    				IsNational = false,
    				Description = "test Description",
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static Holiday WithCode(this Holiday holiday, Guid code)
        {
            holiday.Code = code;
            return holiday;
        }
       	public static Holiday WithSecurityLabel(this Holiday holiday, Guid securityLabel)
        {
            holiday.SecurityLabel = securityLabel;
            return holiday;
        }
       	public static Holiday WithDate(this Holiday holiday, DateTime date)
        {
            holiday.Date = date;
            return holiday;
        }
       	public static Holiday WithIsNational(this Holiday holiday, Boolean isNational)
        {
            holiday.IsNational = isNational;
            return holiday;
        }
       	public static Holiday WithDescription(this Holiday holiday, String description)
        {
            holiday.Description = description;
            return holiday;
        }
       	public static Holiday WithIsActive(this Holiday holiday, Boolean isActive)
        {
            holiday.IsActive = isActive;
            return holiday;
        }
       	public static Holiday WithOrganisation(this Holiday holiday, Organisation organisation)
        {
            holiday.Organisation = organisation;
            return holiday;
        }
    

        #endregion
    }
}