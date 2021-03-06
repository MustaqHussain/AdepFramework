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
using Dwp.Adep.Framework.Management.Web.Models;

namespace Dwp.Adep.Framework.Management.Web.Helpers
{
    public static partial class SessionManager 
    {
    
    
    	public static int ApplicationPageNumber
        {
    		get { return GetFromSession<int>(ApplicationPageNumberKey); }
            set { SetInSession(ApplicationPageNumberKey, value); }
        }
    
        private const string ApplicationPageNumberKey = "APPLICATIONPAGENUMBER";
    	
    	public static string ApplicationCode
        {
    		get { return GetFromSession<string>(ApplicationCodeKey); }
            set { SetInSession(ApplicationCodeKey, value); }
        }
    
        private const string ApplicationCodeKey = "APPLICATIONCODE";
    
    	public static ApplicationModel CurrentApplication
        {
    		get { return GetFromSession<ApplicationModel>(CurrentApplicationKey); }
            set { SetInSession(CurrentApplicationKey, value); }
        }
    
        private const string CurrentApplicationKey = "CURRENTAPPLICATION";
    
    	public static ApplicationModel ApplicationServiceVersion
        {
    		get { return GetFromSession<ApplicationModel>(ApplicationServiceVersionKey); }
            set { SetInSession(ApplicationServiceVersionKey, value); }
        }
    
        private const string ApplicationServiceVersionKey = "APPLICATIONSERVICEVERSION";
    
    	public static int ApplicationAttributePageNumber
        {
    		get { return GetFromSession<int>(ApplicationAttributePageNumberKey); }
            set { SetInSession(ApplicationAttributePageNumberKey, value); }
        }
    
        private const string ApplicationAttributePageNumberKey = "APPLICATIONATTRIBUTEPAGENUMBER";
    	
    	public static string ApplicationAttributeCode
        {
    		get { return GetFromSession<string>(ApplicationAttributeCodeKey); }
            set { SetInSession(ApplicationAttributeCodeKey, value); }
        }
    
        private const string ApplicationAttributeCodeKey = "APPLICATIONATTRIBUTECODE";
    
    	public static ApplicationAttributeModel CurrentApplicationAttribute
        {
    		get { return GetFromSession<ApplicationAttributeModel>(CurrentApplicationAttributeKey); }
            set { SetInSession(CurrentApplicationAttributeKey, value); }
        }
    
        private const string CurrentApplicationAttributeKey = "CURRENTAPPLICATIONATTRIBUTE";
    
    	public static ApplicationAttributeModel ApplicationAttributeServiceVersion
        {
    		get { return GetFromSession<ApplicationAttributeModel>(ApplicationAttributeServiceVersionKey); }
            set { SetInSession(ApplicationAttributeServiceVersionKey, value); }
        }
    
        private const string ApplicationAttributeServiceVersionKey = "APPLICATIONATTRIBUTESERVICEVERSION";
    
    	public static int CountryPageNumber
        {
    		get { return GetFromSession<int>(CountryPageNumberKey); }
            set { SetInSession(CountryPageNumberKey, value); }
        }
    
        private const string CountryPageNumberKey = "COUNTRYPAGENUMBER";
    	
    	public static string CountryCode
        {
    		get { return GetFromSession<string>(CountryCodeKey); }
            set { SetInSession(CountryCodeKey, value); }
        }
    
        private const string CountryCodeKey = "COUNTRYCODE";
    
    	public static CountryModel CurrentCountry
        {
    		get { return GetFromSession<CountryModel>(CurrentCountryKey); }
            set { SetInSession(CurrentCountryKey, value); }
        }
    
        private const string CurrentCountryKey = "CURRENTCOUNTRY";
    
    	public static CountryModel CountryServiceVersion
        {
    		get { return GetFromSession<CountryModel>(CountryServiceVersionKey); }
            set { SetInSession(CountryServiceVersionKey, value); }
        }
    
        private const string CountryServiceVersionKey = "COUNTRYSERVICEVERSION";
    
    	public static int GradePageNumber
        {
    		get { return GetFromSession<int>(GradePageNumberKey); }
            set { SetInSession(GradePageNumberKey, value); }
        }
    
        private const string GradePageNumberKey = "GRADEPAGENUMBER";
    	
    	public static string GradeCode
        {
    		get { return GetFromSession<string>(GradeCodeKey); }
            set { SetInSession(GradeCodeKey, value); }
        }
    
        private const string GradeCodeKey = "GRADECODE";
    
    	public static GradeModel CurrentGrade
        {
    		get { return GetFromSession<GradeModel>(CurrentGradeKey); }
            set { SetInSession(CurrentGradeKey, value); }
        }
    
        private const string CurrentGradeKey = "CURRENTGRADE";
    
    	public static GradeModel GradeServiceVersion
        {
    		get { return GetFromSession<GradeModel>(GradeServiceVersionKey); }
            set { SetInSession(GradeServiceVersionKey, value); }
        }
    
        private const string GradeServiceVersionKey = "GRADESERVICEVERSION";
    
    	public static int HolidayPageNumber
        {
    		get { return GetFromSession<int>(HolidayPageNumberKey); }
            set { SetInSession(HolidayPageNumberKey, value); }
        }
    
        private const string HolidayPageNumberKey = "HOLIDAYPAGENUMBER";
    	
    	public static string HolidayCode
        {
    		get { return GetFromSession<string>(HolidayCodeKey); }
            set { SetInSession(HolidayCodeKey, value); }
        }
    
        private const string HolidayCodeKey = "HOLIDAYCODE";
    
    	public static HolidayModel CurrentHoliday
        {
    		get { return GetFromSession<HolidayModel>(CurrentHolidayKey); }
            set { SetInSession(CurrentHolidayKey, value); }
        }
    
        private const string CurrentHolidayKey = "CURRENTHOLIDAY";
    
    	public static HolidayModel HolidayServiceVersion
        {
    		get { return GetFromSession<HolidayModel>(HolidayServiceVersionKey); }
            set { SetInSession(HolidayServiceVersionKey, value); }
        }
    
        private const string HolidayServiceVersionKey = "HOLIDAYSERVICEVERSION";
    
    	public static int NonStandardHolidayPageNumber
        {
    		get { return GetFromSession<int>(NonStandardHolidayPageNumberKey); }
            set { SetInSession(NonStandardHolidayPageNumberKey, value); }
        }
    
        private const string NonStandardHolidayPageNumberKey = "NONSTANDARDHOLIDAYPAGENUMBER";
    	
    	public static string NonStandardHolidayCode
        {
    		get { return GetFromSession<string>(NonStandardHolidayCodeKey); }
            set { SetInSession(NonStandardHolidayCodeKey, value); }
        }
    
        private const string NonStandardHolidayCodeKey = "NONSTANDARDHOLIDAYCODE";
    
    	public static NonStandardHolidayModel CurrentNonStandardHoliday
        {
    		get { return GetFromSession<NonStandardHolidayModel>(CurrentNonStandardHolidayKey); }
            set { SetInSession(CurrentNonStandardHolidayKey, value); }
        }
    
        private const string CurrentNonStandardHolidayKey = "CURRENTNONSTANDARDHOLIDAY";
    
    	public static NonStandardHolidayModel NonStandardHolidayServiceVersion
        {
    		get { return GetFromSession<NonStandardHolidayModel>(NonStandardHolidayServiceVersionKey); }
            set { SetInSession(NonStandardHolidayServiceVersionKey, value); }
        }
    
        private const string NonStandardHolidayServiceVersionKey = "NONSTANDARDHOLIDAYSERVICEVERSION";
    
    	public static int OrganisationPageNumber
        {
    		get { return GetFromSession<int>(OrganisationPageNumberKey); }
            set { SetInSession(OrganisationPageNumberKey, value); }
        }
    
        private const string OrganisationPageNumberKey = "ORGANISATIONPAGENUMBER";
    	
    	public static string OrganisationCode
        {
    		get { return GetFromSession<string>(OrganisationCodeKey); }
            set { SetInSession(OrganisationCodeKey, value); }
        }
    
        private const string OrganisationCodeKey = "ORGANISATIONCODE";
    
    	public static OrganisationModel CurrentOrganisation
        {
    		get { return GetFromSession<OrganisationModel>(CurrentOrganisationKey); }
            set { SetInSession(CurrentOrganisationKey, value); }
        }
    
        private const string CurrentOrganisationKey = "CURRENTORGANISATION";
    
    	public static OrganisationModel OrganisationServiceVersion
        {
    		get { return GetFromSession<OrganisationModel>(OrganisationServiceVersionKey); }
            set { SetInSession(OrganisationServiceVersionKey, value); }
        }
    
        private const string OrganisationServiceVersionKey = "ORGANISATIONSERVICEVERSION";
    
    	public static int OrganisationTypePageNumber
        {
    		get { return GetFromSession<int>(OrganisationTypePageNumberKey); }
            set { SetInSession(OrganisationTypePageNumberKey, value); }
        }
    
        private const string OrganisationTypePageNumberKey = "ORGANISATIONTYPEPAGENUMBER";
    	
    	public static string OrganisationTypeCode
        {
    		get { return GetFromSession<string>(OrganisationTypeCodeKey); }
            set { SetInSession(OrganisationTypeCodeKey, value); }
        }
    
        private const string OrganisationTypeCodeKey = "ORGANISATIONTYPECODE";
    
    	public static OrganisationTypeModel CurrentOrganisationType
        {
    		get { return GetFromSession<OrganisationTypeModel>(CurrentOrganisationTypeKey); }
            set { SetInSession(CurrentOrganisationTypeKey, value); }
        }
    
        private const string CurrentOrganisationTypeKey = "CURRENTORGANISATIONTYPE";
    
    	public static OrganisationTypeModel OrganisationTypeServiceVersion
        {
    		get { return GetFromSession<OrganisationTypeModel>(OrganisationTypeServiceVersionKey); }
            set { SetInSession(OrganisationTypeServiceVersionKey, value); }
        }
    
        private const string OrganisationTypeServiceVersionKey = "ORGANISATIONTYPESERVICEVERSION";
    
    	public static int OrganisationTypeGroupPageNumber
        {
    		get { return GetFromSession<int>(OrganisationTypeGroupPageNumberKey); }
            set { SetInSession(OrganisationTypeGroupPageNumberKey, value); }
        }
    
        private const string OrganisationTypeGroupPageNumberKey = "ORGANISATIONTYPEGROUPPAGENUMBER";
    	
    	public static string OrganisationTypeGroupCode
        {
    		get { return GetFromSession<string>(OrganisationTypeGroupCodeKey); }
            set { SetInSession(OrganisationTypeGroupCodeKey, value); }
        }
    
        private const string OrganisationTypeGroupCodeKey = "ORGANISATIONTYPEGROUPCODE";
    
    	public static OrganisationTypeGroupModel CurrentOrganisationTypeGroup
        {
    		get { return GetFromSession<OrganisationTypeGroupModel>(CurrentOrganisationTypeGroupKey); }
            set { SetInSession(CurrentOrganisationTypeGroupKey, value); }
        }
    
        private const string CurrentOrganisationTypeGroupKey = "CURRENTORGANISATIONTYPEGROUP";
    
    	public static OrganisationTypeGroupModel OrganisationTypeGroupServiceVersion
        {
    		get { return GetFromSession<OrganisationTypeGroupModel>(OrganisationTypeGroupServiceVersionKey); }
            set { SetInSession(OrganisationTypeGroupServiceVersionKey, value); }
        }
    
        private const string OrganisationTypeGroupServiceVersionKey = "ORGANISATIONTYPEGROUPSERVICEVERSION";
    
    	public static int StaffPageNumber
        {
    		get { return GetFromSession<int>(StaffPageNumberKey); }
            set { SetInSession(StaffPageNumberKey, value); }
        }
    
        private const string StaffPageNumberKey = "STAFFPAGENUMBER";
    	
    	public static string StaffCode
        {
    		get { return GetFromSession<string>(StaffCodeKey); }
            set { SetInSession(StaffCodeKey, value); }
        }
    
        private const string StaffCodeKey = "STAFFCODE";
    
    	public static StaffModel CurrentStaff
        {
    		get { return GetFromSession<StaffModel>(CurrentStaffKey); }
            set { SetInSession(CurrentStaffKey, value); }
        }
    
        private const string CurrentStaffKey = "CURRENTSTAFF";
    
    	public static StaffModel StaffServiceVersion
        {
    		get { return GetFromSession<StaffModel>(StaffServiceVersionKey); }
            set { SetInSession(StaffServiceVersionKey, value); }
        }
    
        private const string StaffServiceVersionKey = "STAFFSERVICEVERSION";
    
    	public static int StaffAttributesPageNumber
        {
    		get { return GetFromSession<int>(StaffAttributesPageNumberKey); }
            set { SetInSession(StaffAttributesPageNumberKey, value); }
        }
    
        private const string StaffAttributesPageNumberKey = "STAFFATTRIBUTESPAGENUMBER";
    	
    	public static string StaffAttributesCode
        {
    		get { return GetFromSession<string>(StaffAttributesCodeKey); }
            set { SetInSession(StaffAttributesCodeKey, value); }
        }
    
        private const string StaffAttributesCodeKey = "STAFFATTRIBUTESCODE";
    
    	public static StaffAttributesModel CurrentStaffAttributes
        {
    		get { return GetFromSession<StaffAttributesModel>(CurrentStaffAttributesKey); }
            set { SetInSession(CurrentStaffAttributesKey, value); }
        }
    
        private const string CurrentStaffAttributesKey = "CURRENTSTAFFATTRIBUTES";
    
    	public static StaffAttributesModel StaffAttributesServiceVersion
        {
    		get { return GetFromSession<StaffAttributesModel>(StaffAttributesServiceVersionKey); }
            set { SetInSession(StaffAttributesServiceVersionKey, value); }
        }
    
        private const string StaffAttributesServiceVersionKey = "STAFFATTRIBUTESSERVICEVERSION";
    
    	public static int StaffDetailsPageNumber
        {
    		get { return GetFromSession<int>(StaffDetailsPageNumberKey); }
            set { SetInSession(StaffDetailsPageNumberKey, value); }
        }
    
        private const string StaffDetailsPageNumberKey = "STAFFDETAILSPAGENUMBER";
    	
    	public static string StaffDetailsCode
        {
    		get { return GetFromSession<string>(StaffDetailsCodeKey); }
            set { SetInSession(StaffDetailsCodeKey, value); }
        }
    
        private const string StaffDetailsCodeKey = "STAFFDETAILSCODE";
    
    	public static StaffDetailsModel CurrentStaffDetails
        {
    		get { return GetFromSession<StaffDetailsModel>(CurrentStaffDetailsKey); }
            set { SetInSession(CurrentStaffDetailsKey, value); }
        }
    
        private const string CurrentStaffDetailsKey = "CURRENTSTAFFDETAILS";
    
    	public static StaffDetailsModel StaffDetailsServiceVersion
        {
    		get { return GetFromSession<StaffDetailsModel>(StaffDetailsServiceVersionKey); }
            set { SetInSession(StaffDetailsServiceVersionKey, value); }
        }
    
        private const string StaffDetailsServiceVersionKey = "STAFFDETAILSSERVICEVERSION";
    
    	public static int StaffOfficesPageNumber
        {
    		get { return GetFromSession<int>(StaffOfficesPageNumberKey); }
            set { SetInSession(StaffOfficesPageNumberKey, value); }
        }
    
        private const string StaffOfficesPageNumberKey = "STAFFOFFICESPAGENUMBER";
    	
    	public static string StaffOfficesCode
        {
    		get { return GetFromSession<string>(StaffOfficesCodeKey); }
            set { SetInSession(StaffOfficesCodeKey, value); }
        }
    
        private const string StaffOfficesCodeKey = "STAFFOFFICESCODE";
    
    	public static StaffOfficesModel CurrentStaffOffices
        {
    		get { return GetFromSession<StaffOfficesModel>(CurrentStaffOfficesKey); }
            set { SetInSession(CurrentStaffOfficesKey, value); }
        }
    
        private const string CurrentStaffOfficesKey = "CURRENTSTAFFOFFICES";
    
    	public static StaffOfficesModel StaffOfficesServiceVersion
        {
    		get { return GetFromSession<StaffOfficesModel>(StaffOfficesServiceVersionKey); }
            set { SetInSession(StaffOfficesServiceVersionKey, value); }
        }
    
        private const string StaffOfficesServiceVersionKey = "STAFFOFFICESSERVICEVERSION";
    
    	public static int StaffOrganisationPageNumber
        {
    		get { return GetFromSession<int>(StaffOrganisationPageNumberKey); }
            set { SetInSession(StaffOrganisationPageNumberKey, value); }
        }
    
        private const string StaffOrganisationPageNumberKey = "STAFFORGANISATIONPAGENUMBER";
    	
    	public static string StaffOrganisationCode
        {
    		get { return GetFromSession<string>(StaffOrganisationCodeKey); }
            set { SetInSession(StaffOrganisationCodeKey, value); }
        }
    
        private const string StaffOrganisationCodeKey = "STAFFORGANISATIONCODE";
    
    	public static StaffOrganisationModel CurrentStaffOrganisation
        {
    		get { return GetFromSession<StaffOrganisationModel>(CurrentStaffOrganisationKey); }
            set { SetInSession(CurrentStaffOrganisationKey, value); }
        }
    
        private const string CurrentStaffOrganisationKey = "CURRENTSTAFFORGANISATION";
    
    	public static StaffOrganisationModel StaffOrganisationServiceVersion
        {
    		get { return GetFromSession<StaffOrganisationModel>(StaffOrganisationServiceVersionKey); }
            set { SetInSession(StaffOrganisationServiceVersionKey, value); }
        }
    
        private const string StaffOrganisationServiceVersionKey = "STAFFORGANISATIONSERVICEVERSION";
    }
}
