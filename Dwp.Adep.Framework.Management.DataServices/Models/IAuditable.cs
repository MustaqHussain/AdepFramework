using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Framework.Management.DataServices.Models
{
    public interface IAuditable
    {
        Guid Code { get; set; }
    }
}
