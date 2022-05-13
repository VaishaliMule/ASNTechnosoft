using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Models
{
    public class UserLogin
    {
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Email Id Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email Id")]
        public string EmailId { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        public string Password { get; set; }

        //[Display(Name = "Remember Me")]
        //public bool Rememberme { get; set; }
    }
}