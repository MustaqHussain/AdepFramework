using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    public class StaffAccessModel
    {
        public int ID { get; set; }

        public string ApplicationName { get; set; }

        public bool IsSpecificOrganisationAccessRequired { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string OrganisationName { get; set; }

        public int OrganisationID { get; set; }

    }
}