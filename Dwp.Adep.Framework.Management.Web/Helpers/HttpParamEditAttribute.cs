using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dwp.Adep.Framework.Management.Web.Helpers
{
    public class HttpParamEditAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if (!actionName.Equals("Edit", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            var request = controllerContext.RequestContext.HttpContext.Request;
            return request[Prefix + methodInfo.Name] != null && !controllerContext.IsChildAction;
        }

        public string Prefix = "Edit::";
    }
}