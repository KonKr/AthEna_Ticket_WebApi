using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Text;

namespace AthEna_WebApi.Services
{
    public class BasicAuthenticationAttribute : TypeFilterAttribute
    {
        public BasicAuthenticationAttribute() : base(typeof(BasicAuthenticationFilter))
        {
        }
    }

    public class BasicAuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (authorization.Length < 5)//if no basic authentication is set...
            {
                context.Result = new UnauthorizedResult();//return 401...
            }
            else
            {
                var authorizationTrimmed = authorization.Remove(0, 6); //remove the "basic" word...
                var authorizationDecoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationTrimmed)); //decode...

                string[] usernamePasswordArray = authorizationDecoded.Split(':'); //split username and password...
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];
                if (!(username == "kapoios" && password =="qwerty123456!@#$%^"))//final check of credentials...
                {
                    context.Result = new UnauthorizedResult();//return 401...
                }
            }
        }
    }
}