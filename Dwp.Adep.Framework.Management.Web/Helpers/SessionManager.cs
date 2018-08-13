using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.ViewModels;

namespace Dwp.Adep.Framework.Management.Web.Helpers
{
    public static partial class SessionManager 
    {
        private static T GetFromSession<T>(string key)
        {
            return (T)HttpContext.Current.Session[key];
        }

        private static void SetInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public static string[] UserRoles
        {
            get { return GetFromSession<string[]>(UserRolesKey); }
            set { SetInSession(UserRolesKey, value); }
        }

        public static string UserID
        {
            get { return GetFromSession<string>(UserIDKey); }
            set { SetInSession(UserIDKey, value); }
        }

        public static string UserName
        {
            get { return GetFromSession<string>(UserNameKey); }
            set { SetInSession(UserNameKey, value); }
        }

        public static string CurrentPage
        {
            get { return GetFromSession<string>(CurrentPageKey); }
            set { SetInSession(CurrentPageKey, value); }
        }

        public static int PageSize
        {
            get { return GetFromSession<int>(PageSizeKey); }
            set { SetInSession(PageSizeKey, value); }
        }

        public static List<StaffAccessModel> StaffAccessList
        {
            get { return GetFromSession<List<StaffAccessModel>>(StaffAccessListKey); }
            set { SetInSession(StaffAccessListKey, value); }
        }

        public static List<ApplicationModel> ApplicationList
        {
            get { return GetFromSession<List<ApplicationModel>>(ApplicationListKey); }
            set { SetInSession(ApplicationListKey, value); }
        }
        public static List<OrganisationByTypeVM> OrganisationByTypeList
        {
            get { return GetFromSession<List<OrganisationByTypeVM>>(OrganisationByTypeListKey); }
            set { SetInSession(OrganisationByTypeListKey, value); }
        }
        public static List<OrganisationByTypeVM> AllOrganisationsForApplicationByTypesList
        {
            get { return GetFromSession<List<OrganisationByTypeVM>>(AllOrganisationsForApplicationByTypesListKey); }
            set { SetInSession(AllOrganisationsForApplicationByTypesListKey, value); }
        }
        
        public static List<StaffApplicationAttributeVM> StaffApplicationAttributeList
        {
            get { return GetFromSession<List<StaffApplicationAttributeVM>>(StaffApplicationAttributeListKey); }
            set { SetInSession(StaffApplicationAttributeListKey, value); }
        }
        public static List<StaffApplicationAttributeVM> StaffApplicationAttributeListDBVersion
        {
            get { return GetFromSession<List<StaffApplicationAttributeVM>>(StaffApplicationAttributeListDBVersionKey); }
            set { SetInSession(StaffApplicationAttributeListDBVersionKey, value); }
        }
        public static List<StaffOrganisationModel> StaffOrganisationList
        {
            get { return GetFromSession<List<StaffOrganisationModel>>(StaffOrganisationListKey); }
            set { SetInSession(StaffOrganisationListKey, value); }
        }
        public static List<StaffOrganisationModel> StaffOrganisationListDBVersion
        {
            get { return GetFromSession<List<StaffOrganisationModel>>(StaffOrganisationListDBKey); }
            set { SetInSession(StaffOrganisationListDBKey, value); }
        }
        public static List<string> StaffRoleList
        {
            get { return GetFromSession<List<string>>(StaffRoleListKey); }
            set { SetInSession(StaffRoleListKey, value); }
        }
        public static OrganisationModel RootOrganisation
        {
            get { return GetFromSession<OrganisationModel>(RootOrganisationKey); }
            set { SetInSession(RootOrganisationKey, value); }
        }
      
        public static Guid? CurrentApplicationCode
        {
            get { return GetFromSession<Guid?>(CurrentApplicationCodeKey); }
            set { SetInSession(CurrentApplicationCodeKey, value); }
        }

        public static StaffModel CurrentStaffForAdmin
        {
            get { return GetFromSession<StaffModel>(CurrentStaffForAdminKey); }
            set { SetInSession(CurrentStaffForAdminKey, value); }
        }

        public static string PageFrom
        {
            get { return GetFromSession<string>(PageFromKey); }
            set { SetInSession(PageFromKey, value); }
        }

        public static StaffSearchCriteriaModel StaffSearchCritera
        {
            get { return GetFromSession<StaffSearchCriteriaModel>(StaffSearchCriteraKey); }
            set { SetInSession(StaffSearchCriteraKey, value); }
        }
        public static List<OrganisationTypeModel> AllTypesForApplication
        {
            get { return GetFromSession < List<OrganisationTypeModel>>(AllTypesForApplicationKey); }
            set { SetInSession(AllTypesForApplicationKey, value); }
        }
        public static int MaximumHopsToChildOrganisation
        {
            get { return GetFromSession<int>(MaximumHopsToChildOrganisationKey); }
            set { SetInSession(MaximumHopsToChildOrganisationKey, value); }
        }
        
        private const string UserRolesKey = "USERROLES";
        private const string UserIDKey = "USERID";
        private const string UserNameKey = "USERNAME";
        private const string CurrentPageKey = "CURRENTSEARCHPAGE";
        private const string PageSizeKey = "PAGESIZE";
        private const string StaffAccessListKey = "STAFFACCESSLIST";
        private const string ApplicationListKey = "APPLICATIONLIST";
        private const string OrganisationByTypeListKey = "ORGANISATIONBYTYPELIST";
        private const string AllOrganisationsForApplicationByTypesListKey = "ALLORGANISATIONFORAPPLICATIONBYTYPELIST";
        private const string StaffApplicationAttributeListKey = "STAFFAPPLICATIONATTRIBUTELIST";
        private const string StaffApplicationAttributeListDBVersionKey = "STAFFAPPLICATIONATTRIBUTEDBLIST";
        private const string StaffOrganisationListKey = "STAFFORGANISATIONLIST";
        private const string StaffOrganisationListDBKey = "STAFFORGANISATIONDBLIST";
        private const string StaffRoleListKey = "STAFFROLELIST";
        private const string RootOrganisationKey = "ROOTORGANISATION";
        private const string CurrentApplicationCodeKey = "CURRENTAPPLICATIONCODE";
        private const string CurrentStaffForAdminKey = "CURRENTSTAFFFORADMIN";
        private const string PageFromKey = "PAGEFROM";
        private const string StaffSearchCriteraKey = "STAFFSEARCHCRITERIA";
        private const string AllTypesForApplicationKey = "APPLICATIONORGANISATIONTYPES";
        private const string MaximumHopsToChildOrganisationKey = "MAXIMUMHOPSTOCHILDORGANISATION";
    }
    
}
