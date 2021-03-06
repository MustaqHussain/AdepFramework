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
    public static partial class BenefitBuilder
    {
        #region Create Method
        public static Benefit Create()
        {
            return new Benefit
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				Benefit1 = "test Benefit1",
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static Benefit WithCode(this Benefit benefit, Guid code)
        {
            benefit.Code = code;
            return benefit;
        }
       	public static Benefit WithSecurityLabel(this Benefit benefit, Guid securityLabel)
        {
            benefit.SecurityLabel = securityLabel;
            return benefit;
        }
       	public static Benefit WithBenefit1(this Benefit benefit, String benefit1)
        {
            benefit.Benefit1 = benefit1;
            return benefit;
        }
       	public static Benefit WithIsActive(this Benefit benefit, Boolean isActive)
        {
            benefit.IsActive = isActive;
            return benefit;
        }
       	public static Benefit WithAccuracyCheck(this Benefit benefit, ICollection< AccuracyCheck> accuracyCheck)
        {
            benefit.AccuracyCheck = accuracyCheck;
            return benefit;
        }
    
       	public static Benefit WithOrganisation(this Benefit benefit, Organisation organisation)
        {
            benefit.Organisation = organisation;
            return benefit;
        }
    

        #endregion
    }
}
