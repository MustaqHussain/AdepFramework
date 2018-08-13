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
    public static partial class SecurityCheckBuilder
    {
        #region Create Method
        public static SecurityCheck Create()
        {
            return new SecurityCheck
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				CheckID = 100,
    				SEF_ID = 100,
    				DateRaised = DateTime.Now,
    				CheckerCode = Guid.NewGuid(),
    				CustomerCode = Guid.NewGuid(),
    				LiaisonCountryCode = Guid.NewGuid(),
    				ReasonForDelayCode = Guid.NewGuid(),
    				BSCNumberCode = Guid.NewGuid(),
    				ServerCode = Guid.NewGuid(),
    				DateAccountDeletion = DateTime.Now,
    				DateBF = DateTime.Now,
    				DateCheckGenerated = DateTime.Now,
    				DateCheckCompleted = DateTime.Now,
    				DaysToClear = 100,
    				DateCheckCompletedEntered = DateTime.Now,
    				DateReminder = DateTime.Now,
    				DateEvidenceRequested = DateTime.Now,
    				DateSentToSAT = DateTime.Now,
    				DateReceivedBySAT = DateTime.Now,
    				DateReceivedInOPS = DateTime.Now,
    				DateRequestReceivedByOps = DateTime.Now,
    				DateReturnedBySAT = DateTime.Now,
    				DateValidation = DateTime.Now,
    				Notes = "test Notes",
    				BF_Notes = "test BF_Notes",
    				StaffCode = Guid.NewGuid(),
    				TeamCode = Guid.NewGuid(),
    				CommandCode = Guid.NewGuid(),
    				LocationCode = Guid.NewGuid(),
    				DateGeneratedMonth = DateTime.Now,
    				DateClearedMonth = DateTime.Now,
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static SecurityCheck WithCode(this SecurityCheck securityCheck, Guid code)
        {
            securityCheck.Code = code;
            return securityCheck;
        }
       	public static SecurityCheck WithSecurityLabel(this SecurityCheck securityCheck, Guid securityLabel)
        {
            securityCheck.SecurityLabel = securityLabel;
            return securityCheck;
        }
       	public static SecurityCheck WithCheckID(this SecurityCheck securityCheck, Int32 checkID)
        {
            securityCheck.CheckID = checkID;
            return securityCheck;
        }
       	public static SecurityCheck WithSEF_ID(this SecurityCheck securityCheck, Int32 sEF_ID)
        {
            securityCheck.SEF_ID = sEF_ID;
            return securityCheck;
        }
       	public static SecurityCheck WithDateRaised(this SecurityCheck securityCheck, DateTime dateRaised)
        {
            securityCheck.DateRaised = dateRaised;
            return securityCheck;
        }
       	public static SecurityCheck WithCheckerCode(this SecurityCheck securityCheck, Guid checkerCode)
        {
            securityCheck.CheckerCode = checkerCode;
            return securityCheck;
        }
       	public static SecurityCheck WithCustomerCode(this SecurityCheck securityCheck, Guid customerCode)
        {
            securityCheck.CustomerCode = customerCode;
            return securityCheck;
        }
       	public static SecurityCheck WithLiaisonCountryCode(this SecurityCheck securityCheck, Guid liaisonCountryCode)
        {
            securityCheck.LiaisonCountryCode = liaisonCountryCode;
            return securityCheck;
        }
       	public static SecurityCheck WithReasonForDelayCode(this SecurityCheck securityCheck, Guid reasonForDelayCode)
        {
            securityCheck.ReasonForDelayCode = reasonForDelayCode;
            return securityCheck;
        }
       	public static SecurityCheck WithBSCNumberCode(this SecurityCheck securityCheck, Guid bSCNumberCode)
        {
            securityCheck.BSCNumberCode = bSCNumberCode;
            return securityCheck;
        }
       	public static SecurityCheck WithServerCode(this SecurityCheck securityCheck, Guid serverCode)
        {
            securityCheck.ServerCode = serverCode;
            return securityCheck;
        }
       	public static SecurityCheck WithDateAccountDeletion(this SecurityCheck securityCheck, DateTime dateAccountDeletion)
        {
            securityCheck.DateAccountDeletion = dateAccountDeletion;
            return securityCheck;
        }
       	public static SecurityCheck WithDateBF(this SecurityCheck securityCheck, DateTime dateBF)
        {
            securityCheck.DateBF = dateBF;
            return securityCheck;
        }
       	public static SecurityCheck WithDateCheckGenerated(this SecurityCheck securityCheck, DateTime dateCheckGenerated)
        {
            securityCheck.DateCheckGenerated = dateCheckGenerated;
            return securityCheck;
        }
       	public static SecurityCheck WithDateCheckCompleted(this SecurityCheck securityCheck, DateTime dateCheckCompleted)
        {
            securityCheck.DateCheckCompleted = dateCheckCompleted;
            return securityCheck;
        }
       	public static SecurityCheck WithDaysToClear(this SecurityCheck securityCheck, Int32 daysToClear)
        {
            securityCheck.DaysToClear = daysToClear;
            return securityCheck;
        }
       	public static SecurityCheck WithDateCheckCompletedEntered(this SecurityCheck securityCheck, DateTime dateCheckCompletedEntered)
        {
            securityCheck.DateCheckCompletedEntered = dateCheckCompletedEntered;
            return securityCheck;
        }
       	public static SecurityCheck WithDateReminder(this SecurityCheck securityCheck, DateTime dateReminder)
        {
            securityCheck.DateReminder = dateReminder;
            return securityCheck;
        }
       	public static SecurityCheck WithDateEvidenceRequested(this SecurityCheck securityCheck, DateTime dateEvidenceRequested)
        {
            securityCheck.DateEvidenceRequested = dateEvidenceRequested;
            return securityCheck;
        }
       	public static SecurityCheck WithDateSentToSAT(this SecurityCheck securityCheck, DateTime dateSentToSAT)
        {
            securityCheck.DateSentToSAT = dateSentToSAT;
            return securityCheck;
        }
       	public static SecurityCheck WithDateReceivedBySAT(this SecurityCheck securityCheck, DateTime dateReceivedBySAT)
        {
            securityCheck.DateReceivedBySAT = dateReceivedBySAT;
            return securityCheck;
        }
       	public static SecurityCheck WithDateReceivedInOPS(this SecurityCheck securityCheck, DateTime dateReceivedInOPS)
        {
            securityCheck.DateReceivedInOPS = dateReceivedInOPS;
            return securityCheck;
        }
       	public static SecurityCheck WithDateRequestReceivedByOps(this SecurityCheck securityCheck, DateTime dateRequestReceivedByOps)
        {
            securityCheck.DateRequestReceivedByOps = dateRequestReceivedByOps;
            return securityCheck;
        }
       	public static SecurityCheck WithDateReturnedBySAT(this SecurityCheck securityCheck, DateTime dateReturnedBySAT)
        {
            securityCheck.DateReturnedBySAT = dateReturnedBySAT;
            return securityCheck;
        }
       	public static SecurityCheck WithDateValidation(this SecurityCheck securityCheck, DateTime dateValidation)
        {
            securityCheck.DateValidation = dateValidation;
            return securityCheck;
        }
       	public static SecurityCheck WithNotes(this SecurityCheck securityCheck, String notes)
        {
            securityCheck.Notes = notes;
            return securityCheck;
        }
       	public static SecurityCheck WithBF_Notes(this SecurityCheck securityCheck, String bF_Notes)
        {
            securityCheck.BF_Notes = bF_Notes;
            return securityCheck;
        }
       	public static SecurityCheck WithStaffCode(this SecurityCheck securityCheck, Guid staffCode)
        {
            securityCheck.StaffCode = staffCode;
            return securityCheck;
        }
       	public static SecurityCheck WithTeamCode(this SecurityCheck securityCheck, Guid teamCode)
        {
            securityCheck.TeamCode = teamCode;
            return securityCheck;
        }
       	public static SecurityCheck WithCommandCode(this SecurityCheck securityCheck, Guid commandCode)
        {
            securityCheck.CommandCode = commandCode;
            return securityCheck;
        }
       	public static SecurityCheck WithLocationCode(this SecurityCheck securityCheck, Guid locationCode)
        {
            securityCheck.LocationCode = locationCode;
            return securityCheck;
        }
       	public static SecurityCheck WithDateGeneratedMonth(this SecurityCheck securityCheck, DateTime dateGeneratedMonth)
        {
            securityCheck.DateGeneratedMonth = dateGeneratedMonth;
            return securityCheck;
        }
       	public static SecurityCheck WithDateClearedMonth(this SecurityCheck securityCheck, DateTime dateClearedMonth)
        {
            securityCheck.DateClearedMonth = dateClearedMonth;
            return securityCheck;
        }
       	public static SecurityCheck WithIsActive(this SecurityCheck securityCheck, Boolean isActive)
        {
            securityCheck.IsActive = isActive;
            return securityCheck;
        }
       	public static SecurityCheck WithBCSNumber(this SecurityCheck securityCheck, BCSNumber bCSNumber)
        {
            securityCheck.BCSNumber = bCSNumber;
            return securityCheck;
        }
    
       	public static SecurityCheck WithCountry(this SecurityCheck securityCheck, Country country)
        {
            securityCheck.Country = country;
            return securityCheck;
        }
    
       	public static SecurityCheck WithCustomer(this SecurityCheck securityCheck, Customer customer)
        {
            securityCheck.Customer = customer;
            return securityCheck;
        }
    
       	public static SecurityCheck WithOrganisation(this SecurityCheck securityCheck, Organisation organisation)
        {
            securityCheck.Organisation = organisation;
            return securityCheck;
        }
    
       	public static SecurityCheck WithOrganisation1(this SecurityCheck securityCheck, Organisation organisation1)
        {
            securityCheck.Organisation1 = organisation1;
            return securityCheck;
        }
    
       	public static SecurityCheck WithOrganisation2(this SecurityCheck securityCheck, Organisation organisation2)
        {
            securityCheck.Organisation2 = organisation2;
            return securityCheck;
        }
    
       	public static SecurityCheck WithOrganisation3(this SecurityCheck securityCheck, Organisation organisation3)
        {
            securityCheck.Organisation3 = organisation3;
            return securityCheck;
        }
    
       	public static SecurityCheck WithReasonForDelay(this SecurityCheck securityCheck, ReasonForDelay reasonForDelay)
        {
            securityCheck.ReasonForDelay = reasonForDelay;
            return securityCheck;
        }
    
       	public static SecurityCheck WithServer(this SecurityCheck securityCheck, Server server)
        {
            securityCheck.Server = server;
            return securityCheck;
        }
    
       	public static SecurityCheck WithStaff(this SecurityCheck securityCheck, Staff staff)
        {
            securityCheck.Staff = staff;
            return securityCheck;
        }
    
       	public static SecurityCheck WithStaff1(this SecurityCheck securityCheck, Staff staff1)
        {
            securityCheck.Staff1 = staff1;
            return securityCheck;
        }
    

        #endregion
    }
}