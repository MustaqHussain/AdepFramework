﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    public partial class StaffOrganisationDC
    {
        [DataMember]
        public List<string> OrganisationPath {get;set;}
    }
}