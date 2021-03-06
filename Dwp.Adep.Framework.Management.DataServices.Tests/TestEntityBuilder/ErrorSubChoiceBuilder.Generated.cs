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
    public static partial class ErrorSubChoiceBuilder
    {
        #region Create Method
        public static ErrorSubChoice Create()
        {
            return new ErrorSubChoice
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				ErrorChoiceCode = Guid.NewGuid(),
    				Description = "test Description",
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static ErrorSubChoice WithCode(this ErrorSubChoice errorSubChoice, Guid code)
        {
            errorSubChoice.Code = code;
            return errorSubChoice;
        }
       	public static ErrorSubChoice WithSecurityLabel(this ErrorSubChoice errorSubChoice, Guid securityLabel)
        {
            errorSubChoice.SecurityLabel = securityLabel;
            return errorSubChoice;
        }
       	public static ErrorSubChoice WithErrorChoiceCode(this ErrorSubChoice errorSubChoice, Guid errorChoiceCode)
        {
            errorSubChoice.ErrorChoiceCode = errorChoiceCode;
            return errorSubChoice;
        }
       	public static ErrorSubChoice WithDescription(this ErrorSubChoice errorSubChoice, String description)
        {
            errorSubChoice.Description = description;
            return errorSubChoice;
        }
       	public static ErrorSubChoice WithIsActive(this ErrorSubChoice errorSubChoice, Boolean isActive)
        {
            errorSubChoice.IsActive = isActive;
            return errorSubChoice;
        }
       	public static ErrorSubChoice WithError(this ErrorSubChoice errorSubChoice, ICollection< Error> error)
        {
            errorSubChoice.Error = error;
            return errorSubChoice;
        }
    
       	public static ErrorSubChoice WithErrorChoice(this ErrorSubChoice errorSubChoice, ErrorChoice errorChoice)
        {
            errorSubChoice.ErrorChoice = errorChoice;
            return errorSubChoice;
        }
    
       	public static ErrorSubChoice WithOrganisation(this ErrorSubChoice errorSubChoice, Organisation organisation)
        {
            errorSubChoice.Organisation = organisation;
            return errorSubChoice;
        }
    

        #endregion
    }
}
