using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.AdminService;
using Dwp.Adep.Framework.Management.Web.ViewModels;
using AutoMapper;

namespace Dwp.Adep.Framework.Management.Web.Helpers
{
    public static partial class CacheManager
    {
        private static T Get<T>(string cacheID, Func<T> getItemCallback) where T : class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item, null, DateTime.Now.Add(new TimeSpan(0,0,3)), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return item;
        }

        private const string SCLists = "FRAMEWORKLISTS";
    }
}