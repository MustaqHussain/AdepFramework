using System;
using System.Web;
using System.Security.Principal;
using System.Threading;
using Dwp.Adep.Framework.Management.Web.AuthorisationService;
using Dwp.Adep.Framework.Management.Web.Helpers;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using System.ServiceModel;

namespace Dwp.Adep.Framework.Management.Web
{
    public class AuthorisationManager
    {

        /// <summary>
        /// Gets authorisation information relating to the current user
        /// </summary>
        /// <remarks>
        /// Retrieves application roles based on a users group membership
        /// </remarks>
        /// <param name="context">HttpContext</param>
        /// <param name="appID">Application ID</param>
        public static void GetUserAuthorisationInfo(HttpContextBase context, string appID)
        {

            string[] roles = null;

            if (context.User.Identity.IsAuthenticated && context.Request.LogonUserIdentity != null && context.Request.LogonUserIdentity.IsAuthenticated)
            {
                //Get roles from session if available
                if (null != SessionManager.UserRoles)
                {
                    roles = SessionManager.UserRoles;
                }
                else
                // If roles not in session then retrieve from authorisation service
                {
                    AuthorisationDC authorisationResult = null;

                    //Create instance of Authorisation service
                    AuthorisationServiceClient sc = new AuthorisationServiceClient();

                    //Get user name for current user
                    string userName = context.User.Identity.Name;

                    try
                    {
#if DEBUG
                        authorisationResult = new AuthorisationDC();
                        authorisationResult.Roles = new string[] { "FW-ADMIN", "FW-APPLICATION", "UCB-APPLICATION", "AM-APPLICATION" };
                        authorisationResult.UserID = "F308F543-75A8-4218-A644-F27765CE51AB";
                        authorisationResult.UserName = "Anna McNabb";
#else
                        string[] adGroups = Roles.GetRolesForUser();

                        //Authorise user and retrieve user roles
                        authorisationResult = sc.GetUserAuthorisationInfo(userName, appID, adGroups);

                        //Close service communication
                        ((ICommunicationObject)sc).Close();
#endif

                        //Store roles in session so we don't have to call service each time
                        SessionManager.UserRoles = roles = authorisationResult.Roles;

                        //Store user's ID in session
                        SessionManager.UserID = authorisationResult.UserID;

                        //Store user's Name in session
                        SessionManager.UserName = authorisationResult.UserName;

                    }
                    catch (FaultException<AuthorisationFailureFault>)
                    {
                        ((ICommunicationObject)sc).Close();

                        //Store user's ID in session
                        SessionManager.UserID = userName;

                        //Store roles as empty array
                        SessionManager.UserRoles = new string[] { };

                    }
                    catch (Exception e)
                    {

                        ExceptionManager.HandleException(e, (ICommunicationObject)sc);
                    }

                }

            }

            //Create new principal object and attach roles
            GenericPrincipal principal = new GenericPrincipal(context.User.Identity, roles);

            //Attach new principal to current thread
            Thread.CurrentPrincipal = context.User = principal;
        }

    }
}