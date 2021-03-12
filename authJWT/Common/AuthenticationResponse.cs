using authJWT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authJWT.Common
{
    [Serializable]
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Uid;
            Username = user.Username;
            IsAdmin = (bool)user.IsAdmin;
            Token = token;
        }
    }
}
