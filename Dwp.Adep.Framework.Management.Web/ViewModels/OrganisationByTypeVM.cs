using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Management.Web.Models;

namespace Dwp.Adep.Framework.Management.Web.ViewModels
{
    public class OrganisationByTypeVM
    {
        public string SelectedOrganisationCode { get; set; }
        public OrganisationTypeModel OrganisationTypeItem { get; set; }
        public List<OrganisationModel> OrganisationList { get; set; }
    }
}