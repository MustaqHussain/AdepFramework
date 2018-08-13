using System;
using System.Data.Objects;
using System.Data;
using System.Data.Common;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public interface IObjectContext : IDisposable 
    { 
        IObjectSet<T> CreateObjectSet<T>() where T : class;
        int SaveChanges();
        void DetectChanges();
        object GetObjectByKey(EntityKey key);

        ObjectStateManager ObjectStateManager { get; }
        DbConnection Connection { get; }
    }
}
