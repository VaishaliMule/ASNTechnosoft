using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Models
{
    public class InstituteRegistration
    {
        [Display(Name = "Registration Date")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Center Name")]
     //   [Required(AllowEmptyStrings = false, ErrorMessage = "Center name required")]
        public string InstituteName { get; set; }

        [Display(Name = "First Name")]
      //  [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Mobile")]
       // [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile number required")]
        [MaxLength(10)]
        [StringLength(10,MinimumLength =10)]
        [RegularExpression(@"^([7-9]{1})([0-9]{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Display(Name ="Email")]
      //  [Required(AllowEmptyStrings =false, ErrorMessage ="Email Id required")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please enter valid email Id")]
        public string Email { get; set; }

        [Display(Name = "Mobile OTP")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Enter Mobile OTP to verify")]
        public string SMSOTP { get; set; }
        public string GeneratedMobileOTP { get; set; }

        [Display(Name = "Email OTP")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Enter Email OTP to verify")]
        public string EmailOTP { get; set; }
        public string GeneratedEmailOTP { get; set; }


    }
}