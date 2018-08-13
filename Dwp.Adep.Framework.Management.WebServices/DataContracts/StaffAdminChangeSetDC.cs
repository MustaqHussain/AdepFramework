using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Framework.Management.WebServices.DataContracts
{
    [DataContract]
    public class StaffAdminChangeSetDC
    {
        [DataMember]
        public List<StaffOrganisationDC> DeletedStaffOrganisations { set; get; }

        [DataMember]
        public List<StaffOrganisationDC> UpdatedStaffOrganisations { set; get; }

        [DataMember]
        public List<StaffOrganisationDC> InsertedStaffOrganisations { set; get; }

        [DataMember]
        public List<StaffAttributesDC> DeletedStaffAttributes { set; get; }

        [DataMember]
        public List<StaffAttributesDC> UpdatedStaffAttributes { set; get; }

        [DataMember]
        public List<StaffAttributesDC> InsertedStaffAttributes { set; get; }

    }
}