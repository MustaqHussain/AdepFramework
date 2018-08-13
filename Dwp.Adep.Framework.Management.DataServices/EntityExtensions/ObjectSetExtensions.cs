using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public static class ObjectSetExtensions
    {
        public static void AttachAsModified<T>(this ObjectSet<T> objectSet, T entity) where T : class
        {
            objectSet.Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Detached);
            objectSet.Attach(entity);
            objectSet.Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
    }
}
