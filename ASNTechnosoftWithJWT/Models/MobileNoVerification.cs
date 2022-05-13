using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Models
{
    public class MobileNoVerification
    {
        public int id { get; set; }

        [Display(Name ="Mobile OTP")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Enter Mobile OTP to verify")]
        public string SMSOTP { get; set; }

        [Display(Name = "Email OTP")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter Email OTP to verify")]
        public string EmailOTP { get; set; }
    }
}