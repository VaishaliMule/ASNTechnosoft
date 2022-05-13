using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public int InstituteId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

    }
}