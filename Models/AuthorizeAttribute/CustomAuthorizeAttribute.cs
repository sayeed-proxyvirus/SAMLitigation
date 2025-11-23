using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SAMLitigation.Services.ServiceImple;

namespace SAMLitigation.Models.AuthorizeAttribute
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizationFillter))
        {
        }
    }
}
