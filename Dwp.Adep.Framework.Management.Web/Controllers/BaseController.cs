using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dwp.Adep.Framework.Management.Web.ViewModels;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using Dwp.Adep.Framework.Management.ResourceLibrary;
using Dwp.Adep.Framework.Management.Web.Models;
using Dwp.Adep.Framework.Management.Web.AdminService;
using AutoMapper;
using Dwp.Adep.Framework.Management.Web.Helpers;
using Dwp.Adep.Framework.Management.Web;
using System.Configuration;
using Dwp.Adep.Framework.Management.Web.Exceptions;

namespace Dwp.Adep.Framework.Management.Web.Controllers
{
    public class BaseController : Controller
    {
        #region Class fields
        
        public int page = 1;
        public string appID = "FrameworkAdmin";

        public int PageSize
        {
            get
            {
                int pageSize;

                try
                {
                    pageSize = SessionManager.PageSize;
                }
                catch (Exception e)
                {
                    pageSize = int.Parse(ConfigurationManager.AppSettings.Get("DefaultPageSize"));

                    SessionManager.PageSize = pageSize;
                }

                return pageSize;
            }
        }

        // Gets the current user's guid and stores in session
        public string CurrentUser
        {
            get
            {
                string userID = SessionManager.UserID;

                if (null == userID)
                {
                    // If user's ID is null then call authorisation process
                    AuthorisationManager.GetUserAuthorisationInfo(this.HttpContext, appID);
                    userID = SessionManager.UserID;
                }

                return userID;
            }
        }

        #endregion

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            AuthorisationManager.GetUserAuthorisationInfo(this.HttpContext, appID);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //WriteLog(string message) yadda yadda

            if (filterContext.Exception is SessionExpiredException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = View("SessionExpired");
            }
            else
            {
                base.OnException(filterContext);
            }
        }

    }
}