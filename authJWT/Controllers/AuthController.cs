using authJWT.Common;
using authJWT.Models;
using authJWT.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace authJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthAppContext _context;
        

        public AuthController(AuthAppContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest login)
        {
            login.Password = CryptoService.Encrypt(login.Password);
            var check = _context.Users.Where(x => x.Username == login.Username && x.Password == login.Password);

            if (check.Count() > 0)
            {
                var user = check.First();
                var token = GenerateJwtToken(user);
                var response = new AuthenticateResponse(user, token);
                return Ok(new APIResponse<AuthenticateResponse>() { ModelData = response, StatusCode = 200, Success = true });
            }

            else
                return Ok(new APIResponse<string>() { ModelData = "No user found", StatusCode = 201, Success = false });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User obj)
        {
            if (_context.Users.Any(x => x.Username == obj.Username))
            {
                return Ok(new APIResponse<string>() { ModelData = "Username Already Exists", StatusCode = 400, Success = false });
            }
            obj.Password = CryptoService.Encrypt(obj.Password);
            obj.CreatedOn = DateTime.Now;
            obj.UpdatedOn = DateTime.Now;
            //this is not a good way to get username but I did to due to short time
            //use pattern matching if possible
            obj.IsAdmin = false;
            _context.Users.Add(obj);
            _context.SaveChanges();
            return Ok(new APIResponse<string>() { ModelData = "User Created", StatusCode = 200, Success = true });
        }



        private string GenerateJwtToken(User user)
        {

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();

            //same key that we are using in startup to tell that we used this key to create a token
            //should be of 32 bits
            var key = Encoding.ASCII.GetBytes("5TGB&YHN7UJM(IK<5TGB&YHN7UJM(IK<");
            var tokenDescriptor = new SecurityTokenDescriptor();

            if (user.Uid > 0)
            {

                tokenDescriptor.Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Uid.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("isA", user.IsAdmin.ToString())
                });
                tokenDescriptor.Expires = DateTime.UtcNow.AddDays(7);
                tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
