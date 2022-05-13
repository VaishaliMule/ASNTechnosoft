using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class InstituteModel
    {
        public int InstituteId { get; set; }

        [Display(Name = "Center Name")]
        public string InstituteName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Mobile")]
        [MaxLength(10)]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^([7-9]{1})([0-9]{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Display(Name = "Alternate Mobile")]
        [MaxLength(10)]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^([7-9]{1})([0-9]{9})$", ErrorMessage = "Invalid Mobile Number.")]
        public string AlternateMobile { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email Id")]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string DOB { get; set; }
        [Display(Name = "Gender")]

        public string Gender { get; set; }

        [Display(Name = "Marital Status")]
        public string MarritalStatus { get; set; }

        [Display(Name = "Aadhar Number")]
        public string Aadhar { get; set; }

        [Display(Name = "PAN Number")]
        public string PAN { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Country")]
        public IList<SelectListItem> CountryNames { get; set; }
        public int CountryId { get; set; }

        [Display(Name = "State")]
        public IList<SelectListItem> StateNames { get; set; }
        public int StateId { get; set; }

        [Display(Name = "District")]
        public IList<SelectListItem> DistrictNames { get; set; }
        public int DistrictId { get; set; }

        [Display(Name = "Tahsil/ Block")]
        public IList<SelectListItem> TahsilNames { get; set; }
        public int TahsilId { get; set; }

        [Display(Name = "Location")]
        public IList<SelectListItem> AreaNames { get; set; }
        public int AreaId { get; set; }
        public string Area { get; set; }

        [Display(Name = "Pin Code")]
        [MaxLength(6)]
        public string PinCode { get; set; }
        public string PhotoUpload { get; set; }
        public HttpPostedFileBase PhotoImageFile { get; set; }
        public string Photofilename { get; set; }
        public string LogoUpload { get; set; }
        public HttpPostedFileBase LogoImageFile { get; set; }
        public string Logofilename { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be 8 char long.")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password must match.")]
        public string VerifyPassword { get; set; }
    }
}