using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace Employeservice
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationtoken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedauthenticationtoken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationtoken));
                string[] userpassword = decodedauthenticationtoken.Split(':');
                string username = userpassword[0];
                string password = userpassword[1];

                if(EmployeeSecurity.Login(username,password))
                {

                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                }
            }
            //base.OnAuthorization(actionContext);
        }
    }
}