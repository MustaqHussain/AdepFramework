using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    public partial class OrganisationModel
    {
        public Guid ParentID { get; set; }
        
        public string NameAndActive
        {
            get { return Name + " " + (IsActive == true ? "" : "(Inactive)"); }
        }
    }


}