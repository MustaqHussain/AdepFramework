using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Framework.Management.DataServices.Models
{
    public interface IActiveAware
    {
        Guid Code { get; set; }
        bool IsActive { get; set; }
    }
}
