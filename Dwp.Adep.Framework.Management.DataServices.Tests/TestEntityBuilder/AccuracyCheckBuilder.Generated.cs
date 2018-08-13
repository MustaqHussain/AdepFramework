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
    public static partial class AccuracyCheckBuilder
    {
        #region Create Method
        public static AccuracyCheck Create()
        {
            return new AccuracyCheck
            {
    				Code = Guid.NewGuid(),
    				SecurityLabel = Guid.NewGuid(),
    				CheckID = 100,
    				RaisedDate = DateTime.Now,
    				IsNewClaim = false,
    				IsChangeOfCircs = false,
    				CheckerStaffCode = Guid.NewGuid(),
    				BenefitCode = Guid.NewGuid(),
    				GeneratedDate = DateTime.Now,
    				CheckTypeCode = Guid.NewGuid(),
    				CheckSubTypeCode = Guid.NewGuid(),
    				IsPrePaymentCheck = false,
    				IsPostPaymentCheck = false,
    				NI_Number = "test NI_Number",
    				CustomerFirstName = "test CustomerFirstName",
    				CustomerLastName = "test CustomerLastName",
    				CountryCode = Guid.NewGuid(),
    				IsClaim = false,
    				DaysToClear = 100,
    				BCS_NumberCode = Guid.NewGuid(),
    				BCS_Date = DateTime.Now,
    				BCS_TargetDate = DateTime.Now,
    				ScheduleNumberCode = Guid.NewGuid(),
    				IOP_OnSchedule = 100,
    				ServerCode = Guid.NewGuid(),
    				PapersRequestedForChecking = DateTime.Now,
    				RequestReceivedByOps = DateTime.Now,
    				PapersSentForChecking = DateTime.Now,
    				PapersReceivedForChecking = DateTime.Now,
    				StaffCode = Guid.NewGuid(),
    				TeamCode = Guid.NewGuid(),
    				CommandCode = Guid.NewGuid(),
    				LocationCode = Guid.NewGuid(),
    				BF_Date = DateTime.Now,
    				CheckCompletedDate = DateTime.Now,
    				ValidatorStaffCode = Guid.NewGuid(),
    				ValidatedOn = DateTime.Now,
    				PapersReturnedToSection = DateTime.Now,
    				PapersReceivedInOps = DateTime.Now,
    				ValidationNotes = "test ValidationNotes",
    				GeneralNotes = "test GeneralNotes",
    				DateLastUpdated = DateTime.Now,
    				IsActive = false,
    				RowIdentifier = null
            };
        }

        #endregion
    
        #region With Methods
       	public static AccuracyCheck WithCode(this AccuracyCheck accuracyCheck, Guid code)
        {
            accuracyCheck.Code = code;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithSecurityLabel(this AccuracyCheck accuracyCheck, Guid securityLabel)
        {
            accuracyCheck.SecurityLabel = securityLabel;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCheckID(this AccuracyCheck accuracyCheck, Int32 checkID)
        {
            accuracyCheck.CheckID = checkID;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithRaisedDate(this AccuracyCheck accuracyCheck, DateTime raisedDate)
        {
            accuracyCheck.RaisedDate = raisedDate;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIsNewClaim(this AccuracyCheck accuracyCheck, Boolean isNewClaim)
        {
            accuracyCheck.IsNewClaim = isNewClaim;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIsChangeOfCircs(this AccuracyCheck accuracyCheck, Boolean isChangeOfCircs)
        {
            accuracyCheck.IsChangeOfCircs = isChangeOfCircs;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCheckerStaffCode(this AccuracyCheck accuracyCheck, Guid checkerStaffCode)
        {
            accuracyCheck.CheckerStaffCode = checkerStaffCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithBenefitCode(this AccuracyCheck accuracyCheck, Guid benefitCode)
        {
            accuracyCheck.BenefitCode = benefitCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithGeneratedDate(this AccuracyCheck accuracyCheck, DateTime generatedDate)
        {
            accuracyCheck.GeneratedDate = generatedDate;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCheckTypeCode(this AccuracyCheck accuracyCheck, Guid checkTypeCode)
        {
            accuracyCheck.CheckTypeCode = checkTypeCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCheckSubTypeCode(this AccuracyCheck accuracyCheck, Guid checkSubTypeCode)
        {
            accuracyCheck.CheckSubTypeCode = checkSubTypeCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIsPrePaymentCheck(this AccuracyCheck accuracyCheck, Boolean isPrePaymentCheck)
        {
            accuracyCheck.IsPrePaymentCheck = isPrePaymentCheck;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIsPostPaymentCheck(this AccuracyCheck accuracyCheck, Boolean isPostPaymentCheck)
        {
            accuracyCheck.IsPostPaymentCheck = isPostPaymentCheck;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithNI_Number(this AccuracyCheck accuracyCheck, String nI_Number)
        {
            accuracyCheck.NI_Number = nI_Number;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCustomerFirstName(this AccuracyCheck accuracyCheck, String customerFirstName)
        {
            accuracyCheck.CustomerFirstName = customerFirstName;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCustomerLastName(this AccuracyCheck accuracyCheck, String customerLastName)
        {
            accuracyCheck.CustomerLastName = customerLastName;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCountryCode(this AccuracyCheck accuracyCheck, Guid countryCode)
        {
            accuracyCheck.CountryCode = countryCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIsClaim(this AccuracyCheck accuracyCheck, Boolean isClaim)
        {
            accuracyCheck.IsClaim = isClaim;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithDaysToClear(this AccuracyCheck accuracyCheck, Int32 daysToClear)
        {
            accuracyCheck.DaysToClear = daysToClear;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithBCS_NumberCode(this AccuracyCheck accuracyCheck, Guid bCS_NumberCode)
        {
            accuracyCheck.BCS_NumberCode = bCS_NumberCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithBCS_Date(this AccuracyCheck accuracyCheck, DateTime bCS_Date)
        {
            accuracyCheck.BCS_Date = bCS_Date;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithBCS_TargetDate(this AccuracyCheck accuracyCheck, DateTime bCS_TargetDate)
        {
            accuracyCheck.BCS_TargetDate = bCS_TargetDate;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithScheduleNumberCode(this AccuracyCheck accuracyCheck, Guid scheduleNumberCode)
        {
            accuracyCheck.ScheduleNumberCode = scheduleNumberCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIOP_OnSchedule(this AccuracyCheck accuracyCheck, Int32 iOP_OnSchedule)
        {
            accuracyCheck.IOP_OnSchedule = iOP_OnSchedule;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithServerCode(this AccuracyCheck accuracyCheck, Guid serverCode)
        {
            accuracyCheck.ServerCode = serverCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithPapersRequestedForChecking(this AccuracyCheck accuracyCheck, DateTime papersRequestedForChecking)
        {
            accuracyCheck.PapersRequestedForChecking = papersRequestedForChecking;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithRequestReceivedByOps(this AccuracyCheck accuracyCheck, DateTime requestReceivedByOps)
        {
            accuracyCheck.RequestReceivedByOps = requestReceivedByOps;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithPapersSentForChecking(this AccuracyCheck accuracyCheck, DateTime papersSentForChecking)
        {
            accuracyCheck.PapersSentForChecking = papersSentForChecking;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithPapersReceivedForChecking(this AccuracyCheck accuracyCheck, DateTime papersReceivedForChecking)
        {
            accuracyCheck.PapersReceivedForChecking = papersReceivedForChecking;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithStaffCode(this AccuracyCheck accuracyCheck, Guid staffCode)
        {
            accuracyCheck.StaffCode = staffCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithTeamCode(this AccuracyCheck accuracyCheck, Guid teamCode)
        {
            accuracyCheck.TeamCode = teamCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCommandCode(this AccuracyCheck accuracyCheck, Guid commandCode)
        {
            accuracyCheck.CommandCode = commandCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithLocationCode(this AccuracyCheck accuracyCheck, Guid locationCode)
        {
            accuracyCheck.LocationCode = locationCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithBF_Date(this AccuracyCheck accuracyCheck, DateTime bF_Date)
        {
            accuracyCheck.BF_Date = bF_Date;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithCheckCompletedDate(this AccuracyCheck accuracyCheck, DateTime checkCompletedDate)
        {
            accuracyCheck.CheckCompletedDate = checkCompletedDate;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithValidatorStaffCode(this AccuracyCheck accuracyCheck, Guid validatorStaffCode)
        {
            accuracyCheck.ValidatorStaffCode = validatorStaffCode;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithValidatedOn(this AccuracyCheck accuracyCheck, DateTime validatedOn)
        {
            accuracyCheck.ValidatedOn = validatedOn;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithPapersReturnedToSection(this AccuracyCheck accuracyCheck, DateTime papersReturnedToSection)
        {
            accuracyCheck.PapersReturnedToSection = papersReturnedToSection;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithPapersReceivedInOps(this AccuracyCheck accuracyCheck, DateTime papersReceivedInOps)
        {
            accuracyCheck.PapersReceivedInOps = papersReceivedInOps;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithValidationNotes(this AccuracyCheck accuracyCheck, String validationNotes)
        {
            accuracyCheck.ValidationNotes = validationNotes;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithGeneralNotes(this AccuracyCheck accuracyCheck, String generalNotes)
        {
            accuracyCheck.GeneralNotes = generalNotes;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithDateLastUpdated(this AccuracyCheck accuracyCheck, DateTime dateLastUpdated)
        {
            accuracyCheck.DateLastUpdated = dateLastUpdated;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithIsActive(this AccuracyCheck accuracyCheck, Boolean isActive)
        {
            accuracyCheck.IsActive = isActive;
            return accuracyCheck;
        }
       	public static AccuracyCheck WithScheduleNumber(this AccuracyCheck accuracyCheck, ScheduleNumber scheduleNumber)
        {
            accuracyCheck.ScheduleNumber = scheduleNumber;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithBCSNumber(this AccuracyCheck accuracyCheck, BCSNumber bCSNumber)
        {
            accuracyCheck.BCSNumber = bCSNumber;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithBenefit(this AccuracyCheck accuracyCheck, Benefit benefit)
        {
            accuracyCheck.Benefit = benefit;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithCheckSubType(this AccuracyCheck accuracyCheck, CheckSubType checkSubType)
        {
            accuracyCheck.CheckSubType = checkSubType;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithCheckType(this AccuracyCheck accuracyCheck, CheckType checkType)
        {
            accuracyCheck.CheckType = checkType;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithCountry(this AccuracyCheck accuracyCheck, Country country)
        {
            accuracyCheck.Country = country;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithOrganisation(this AccuracyCheck accuracyCheck, Organisation organisation)
        {
            accuracyCheck.Organisation = organisation;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithOrganisation1(this AccuracyCheck accuracyCheck, Organisation organisation1)
        {
            accuracyCheck.Organisation1 = organisation1;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithOrganisation2(this AccuracyCheck accuracyCheck, Organisation organisation2)
        {
            accuracyCheck.Organisation2 = organisation2;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithOrganisation3(this AccuracyCheck accuracyCheck, Organisation organisation3)
        {
            accuracyCheck.Organisation3 = organisation3;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithServer(this AccuracyCheck accuracyCheck, Server server)
        {
            accuracyCheck.Server = server;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithStaff(this AccuracyCheck accuracyCheck, Staff staff)
        {
            accuracyCheck.Staff = staff;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithStaff1(this AccuracyCheck accuracyCheck, Staff staff1)
        {
            accuracyCheck.Staff1 = staff1;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithStaff2(this AccuracyCheck accuracyCheck, Staff staff2)
        {
            accuracyCheck.Staff2 = staff2;
            return accuracyCheck;
        }
    
       	public static AccuracyCheck WithError(this AccuracyCheck accuracyCheck, ICollection< Error> error)
        {
            accuracyCheck.Error = error;
            return accuracyCheck;
        }
    

        #endregion
    }
}
