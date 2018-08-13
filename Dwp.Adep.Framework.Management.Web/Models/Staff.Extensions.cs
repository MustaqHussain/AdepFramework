using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Framework.Management.Web.Models
{

    public partial class StaffModel
    {
        public string FullName
        {
            get { return LastName + "," + FirstName; }
        }
        public string TeamName { get; set; }
        public string CommandName { get; set; }
        public string LocationName { get; set; }
        public string GradeName { get; set; }
        public Guid? TeamCode { get; set; }
        public Guid? CommandCode { get; set; }
        public Guid? LocationCode { get; set; }
    }

}