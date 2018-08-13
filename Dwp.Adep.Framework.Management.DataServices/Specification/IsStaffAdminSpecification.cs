using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dwp.Adep.Framework.Management.DataServices.Models;

namespace Dwp.Adep.Framework.Management.DataServices.Specification
{
    public class IsStaffAdminSpecification : Specification<Application>
    {

        ISpecification<Application> Spec = new Specification<Application>();

        public IsStaffAdminSpecification(string StaffCode)
        {
            if (StaffCode != null)
            {
                Guid StaffCodeGuid;
                if (!Guid.TryParse(StaffCode, out StaffCodeGuid))
                    throw new ArgumentOutOfRangeException("StaffCode must be a GUID");

                Spec = new Specification<Application>();
                Spec = Spec.And(x => x.StaffAttributes.Any(a=> a.StaffCode == StaffCodeGuid && x.ApplicationAttribute.Any(y => y.Code == a.ApplicationAttributeCode && y.ApplicationAttributeExtension.Any(z => z.IsStaffAdmin.Equals(true)))));


                Predicate = Spec.Predicate;// ApplicationAttributeSpecification.And(StaffSpecification).Predicate;
            }
            else
            {
                throw new ArgumentNullException("StaffCode");
            }

            
        }
    }
}
