using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.AdminService;

namespace Dwp.Adep.Framework.Management.Web
{
    public partial class TypeMappings
    {
        public static void DefineTypeMappings()
        {
            DefineTypeMappingsGenerated();

            Mapper.CreateMap<StaffSearchCriteriaModel, StaffSearchCriteriaDC>();
            Mapper.CreateMap<StaffSearchCriteriaDC, StaffSearchCriteriaModel>();
        }

        static partial void DefineTypeMappingsGenerated();
    }
}