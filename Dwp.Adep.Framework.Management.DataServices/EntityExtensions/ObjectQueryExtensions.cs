using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Dwp.Adep.Framework.Management.DataServices
{
    public static class ObjectQueryExtensions
    {
        public static IQueryable<T> Include<T>(this IQueryable<T> source, string path) where T : class
        {
            var objectQuery = source as ObjectQuery<T>;
            if (objectQuery != null)
            {
                return objectQuery.Include(path);
            }

            return source;
        }
    }
}
