using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebIMSWithJWT.Models
{
    public class LoginResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}