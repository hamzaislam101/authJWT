using authJWT.Common;
using authJWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly AuthAppContext _context;
        public TestController(AuthAppContext context, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _context = context;
        }

        [HttpGet("GetUsername")]
        public IActionResult GetUsername()
        {
            if (string.IsNullOrEmpty(Username))
            {
                return Ok(new APIResponse<string>() { ModelData = "User not logged in", StatusCode = 200, Success = false });
            }
            return Ok(new APIResponse<string>() { ModelData =Username, StatusCode = 200, Success = true });
        }
    }
}
