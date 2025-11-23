using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAMLitigation.Services;

namespace SAMLitigation.Models.AuthorizeAttribute
{
    public class CustomAuthorizationFillter : IAuthorizationFilter
    {
        private readonly AuthenticateService authenticateService;

        public CustomAuthorizationFillter(AuthenticateService authenticateService)
        {
            this.authenticateService = authenticateService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try 
            {
                var httpContext = context.HttpContext;

                //Check Authentication
                if (!httpContext.User.Identity?.IsAuthenticated ?? false) 
                {
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                    return;
                }

                // 2. Controller and Action
                var controllerName = context.RouteData.Values["controller"]?.ToString();
                var actionName = context.RouteData.Values["action"]?.ToString();

                bool isGet = httpContext.Request.Method == "GET";

                // 3. User info from session
                string userName = httpContext.Session.GetString("userName");
                decimal userId = Convert.ToDecimal(httpContext.Session.GetString("userId"));

                if (string.IsNullOrEmpty(userName))
                {
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                    return;
                }

                // 4. Call your API service
                bool isPermitted = authenticateService.GetControllerMethodPermission(
                    controllerName, actionName, userId, isGet
                );

                if (!isPermitted)
                {
                    context.Result = new RedirectToActionResult("Index", "Exception", null);
                    return;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
