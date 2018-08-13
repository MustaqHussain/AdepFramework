using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Dwp.Adep.Framework.Management.DataServices.Models
{
    public partial class AdepDatabaseEntities : IAdepDatabaseEntities
    {
        System.Data.Objects.IObjectSet<T> IObjectContext.CreateObjectSet<T>()
        {
            return (IObjectSet<T>)this.CreateObjectSet<T>();
        }

    }
}
