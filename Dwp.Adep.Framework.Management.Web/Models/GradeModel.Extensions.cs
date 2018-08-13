using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Management.Web.Models
{
    public partial class GradeModel
    {
        public string NameAndActiveFlag
        {
            get { return Grade1 + " " + (IsActive == true ? "" : "(Inactive)"); }
        }
    }
}