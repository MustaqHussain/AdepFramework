using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Common;
using System.Data;

namespace Dwp.Adep.Framework.Management.DataServices.Repositories
{
    public class NullObjectContext : IObjectContext
    {
     
        IObjectSet<T>  IObjectContext.CreateObjectSet<T>()
        {
 	        throw new NotImplementedException();
        }

        public int  SaveChanges()
        {
 	        throw new NotImplementedException();
        }

        public void  Dispose()
        {
 	        throw new NotImplementedException();
        }

        public void DetectChanges()
        {
        }

        ObjectStateManager _osm;

        public ObjectStateManager ObjectStateManager
        {
            get { return _osm; }
        }

        public DbConnection Connection { get; set; }

        public object GetObjectByKey(EntityKey key)
        {
            return null;
        }
    }
}
