using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public interface IUnitOfWork : IDisposable
    {
        IObjectContext ObjectContext{get;}
        void Commit();
    }
}
