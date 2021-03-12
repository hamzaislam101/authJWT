using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace authJWT.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        //this is the base controller from which we'll inherit all the controllers where we need these claims 
        //Notice that it is using Authorize attribute, it is optional

        //its better to create a model of ActiveUser having all these parameters
        //but to make it simple I'm keeping it this way

        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public BaseController(IHttpContextAccessor contextAccessor)
        {
            UserId = Convert.ToInt32(contextAccessor?.HttpContext?.User?.Claims?.Where(x => x.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value ?? "0");
            Username = contextAccessor?.HttpContext?.User?.Claims?.Where(x => x.Type == ClaimTypes.Name)?.FirstOrDefault()?.Value ?? "";
            IsAdmin = Convert.ToBoolean(contextAccessor?.HttpContext?.User?.Claims?.Where(x => x.Type == "isA")?.FirstOrDefault()?.Value ?? "false");

        }
    }
}
