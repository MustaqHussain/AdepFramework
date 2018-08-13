using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dwp.Adep.Framework.Management.Web.Models;

namespace Dwp.Adep.Framework.Management.Web
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult ||
                !HttpContext.Current.User.IsInRole(FrameworkRoles.APPLICATION))
            {
                var result = new ViewResult();
                result.ViewName = "UnAuthorized";
                result.MasterName = "_Layout";
                filterContext.Result = result;
            }
        }
    }
}