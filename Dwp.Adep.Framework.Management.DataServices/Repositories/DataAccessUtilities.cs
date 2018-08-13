using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dwp.Adep.Framework.Management.IoC.ServiceLocation;
using Dwp.Adep.Framework.Management.DataServices.Repositories;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public class DataAccessUtilities
    {
        private DataAccessUtilities()
        { }
        public static T RepositoryLocator<T>(IObjectContext context) 
        {
            Dictionary<string, object> contextHolder = new Dictionary<string, object>();
            
            contextHolder.Add("context", context);
            
            return SimpleServiceLocator.Instance.Get<T>(contextHolder);
        }
        public static T RepositoryLocator<T>()
        {
            return RepositoryLocator<T>(new NullObjectContext());
        }
        
    }
}
