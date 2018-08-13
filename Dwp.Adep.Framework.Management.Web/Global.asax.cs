using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Configuration;
using System.Web.Configuration;

namespace Dwp.Adep.Framework.Management.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SessionExpiryAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_PreRequestHandlerExecute(HttpApplication sender, EventArgs e)
        {
            if (HttpContext.Current.CurrentHandler is MvcHandler)
            {
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                HttpContext.Current.Response.Cache.SetNoStore();
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            }
            else
            {
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(12));
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Add mappings for Models to data contracts and vice versa
            TypeMappings.DefineTypeMappings();

            string sessionCookieName = null;

            // Get session state section from web.config file
            SessionStateSection sessionStateValue = ConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;

            // Get session cookie name if it is provided otherwise use the default value of "ASP.NET_SessionId" 
            if (sessionStateValue != null && !string.IsNullOrEmpty(sessionStateValue.CookieName))
            {
                sessionCookieName = sessionStateValue.CookieName;
            }
            else
            {
                sessionCookieName = "ASP.NET_SessionId";
            }

            // Store in application state
            Application["sessionCookieName"] = sessionCookieName;
        }

    }
}