using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Dwp.Adep.Framework.Management.Web.Exceptions;

namespace Dwp.Adep.Framework.Management.Web
{
    public class SessionExpiryAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContext context = HttpContext.Current;

            if (context.Request.IsAuthenticated && context.Session != null && context.Session.IsNewSession)
            {
                string sessionCookieName = context.Application["sessionCookieName"].ToString();

                string sessionCookie = context.Request.Headers["Cookie"];
                if (sessionCookie != null && sessionCookie.IndexOf(sessionCookieName) >= 0)
                {
                    throw new SessionExpiredException("Session Lost");
                }
            }
        }
    }
}