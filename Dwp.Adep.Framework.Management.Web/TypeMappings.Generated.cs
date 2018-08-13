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
using AutoMapper;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.AdminService;

namespace Dwp.Adep.Framework.Management.Web
{
    
    public partial class TypeMappings
    {
        static partial void DefineTypeMappingsGenerated()
        {
    
                Mapper.CreateMap<ApplicationModel, ApplicationDC>();
                Mapper.CreateMap<ApplicationDC, ApplicationModel>();
    			Mapper.CreateMap<ApplicationSearchMatchModel, ApplicationSearchMatchDC>();
                Mapper.CreateMap<ApplicationSearchMatchDC, ApplicationSearchMatchModel>();
    
                Mapper.CreateMap<ApplicationAttributeModel, ApplicationAttributeDC>();
                Mapper.CreateMap<ApplicationAttributeDC, ApplicationAttributeModel>();
    			Mapper.CreateMap<ApplicationAttributeSearchMatchModel, ApplicationAttributeSearchMatchDC>();
                Mapper.CreateMap<ApplicationAttributeSearchMatchDC, ApplicationAttributeSearchMatchModel>();
    
                Mapper.CreateMap<CountryModel, CountryDC>();
                Mapper.CreateMap<CountryDC, CountryModel>();
    			Mapper.CreateMap<CountrySearchMatchModel, CountrySearchMatchDC>();
                Mapper.CreateMap<CountrySearchMatchDC, CountrySearchMatchModel>();
    
                Mapper.CreateMap<GradeModel, GradeDC>();
                Mapper.CreateMap<GradeDC, GradeModel>();
    			Mapper.CreateMap<GradeSearchMatchModel, GradeSearchMatchDC>();
                Mapper.CreateMap<GradeSearchMatchDC, GradeSearchMatchModel>();
    
                Mapper.CreateMap<HolidayModel, HolidayDC>();
                Mapper.CreateMap<HolidayDC, HolidayModel>();
    			Mapper.CreateMap<HolidaySearchMatchModel, HolidaySearchMatchDC>();
                Mapper.CreateMap<HolidaySearchMatchDC, HolidaySearchMatchModel>();
    
                Mapper.CreateMap<NonStandardHolidayModel, NonStandardHolidayDC>();
                Mapper.CreateMap<NonStandardHolidayDC, NonStandardHolidayModel>();
    			Mapper.CreateMap<NonStandardHolidaySearchMatchModel, NonStandardHolidaySearchMatchDC>();
                Mapper.CreateMap<NonStandardHolidaySearchMatchDC, NonStandardHolidaySearchMatchModel>();
    
                Mapper.CreateMap<OrganisationModel, OrganisationDC>();
                Mapper.CreateMap<OrganisationDC, OrganisationModel>();
    			Mapper.CreateMap<OrganisationSearchMatchModel, OrganisationSearchMatchDC>();
                Mapper.CreateMap<OrganisationSearchMatchDC, OrganisationSearchMatchModel>();
    
                Mapper.CreateMap<OrganisationTypeModel, OrganisationTypeDC>();
                Mapper.CreateMap<OrganisationTypeDC, OrganisationTypeModel>();
    			Mapper.CreateMap<OrganisationTypeSearchMatchModel, OrganisationTypeSearchMatchDC>();
                Mapper.CreateMap<OrganisationTypeSearchMatchDC, OrganisationTypeSearchMatchModel>();
    
                Mapper.CreateMap<OrganisationTypeGroupModel, OrganisationTypeGroupDC>();
                Mapper.CreateMap<OrganisationTypeGroupDC, OrganisationTypeGroupModel>();
    			Mapper.CreateMap<OrganisationTypeGroupSearchMatchModel, OrganisationTypeGroupSearchMatchDC>();
                Mapper.CreateMap<OrganisationTypeGroupSearchMatchDC, OrganisationTypeGroupSearchMatchModel>();
    
                Mapper.CreateMap<StaffModel, StaffDC>();
                Mapper.CreateMap<StaffDC, StaffModel>();
    			Mapper.CreateMap<StaffSearchMatchModel, StaffSearchMatchDC>();
                Mapper.CreateMap<StaffSearchMatchDC, StaffSearchMatchModel>();
    
                Mapper.CreateMap<StaffAttributesModel, StaffAttributesDC>();
                Mapper.CreateMap<StaffAttributesDC, StaffAttributesModel>();
    			Mapper.CreateMap<StaffAttributesSearchMatchModel, StaffAttributesSearchMatchDC>();
                Mapper.CreateMap<StaffAttributesSearchMatchDC, StaffAttributesSearchMatchModel>();
    
                Mapper.CreateMap<StaffDetailsModel, StaffDetailsDC>();
                Mapper.CreateMap<StaffDetailsDC, StaffDetailsModel>();
    			Mapper.CreateMap<StaffDetailsSearchMatchModel, StaffDetailsSearchMatchDC>();
                Mapper.CreateMap<StaffDetailsSearchMatchDC, StaffDetailsSearchMatchModel>();
    
                Mapper.CreateMap<StaffOfficesModel, StaffOfficesDC>();
                Mapper.CreateMap<StaffOfficesDC, StaffOfficesModel>();
    			Mapper.CreateMap<StaffOfficesSearchMatchModel, StaffOfficesSearchMatchDC>();
                Mapper.CreateMap<StaffOfficesSearchMatchDC, StaffOfficesSearchMatchModel>();
    
                Mapper.CreateMap<StaffOrganisationModel, StaffOrganisationDC>();
                Mapper.CreateMap<StaffOrganisationDC, StaffOrganisationModel>();
    			Mapper.CreateMap<StaffOrganisationSearchMatchModel, StaffOrganisationSearchMatchDC>();
                Mapper.CreateMap<StaffOrganisationSearchMatchDC, StaffOrganisationSearchMatchModel>();
    	}
    }
}