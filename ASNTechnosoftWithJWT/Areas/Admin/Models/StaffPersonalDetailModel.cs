using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class StaffPersonalDetailModel
    {
        [Display(Name = "FirstName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "MiddleName")]
        public string MiddleName { get; set; }

        [Display(Name = "LastName")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select gender")]
        public string Gender { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Id required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email Id")]
        public string Email { get; set; }

        [Display(Name = "DateofBirth")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of Birth required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }

        [Display(Name = "Qualification")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Qualification")]
        public List<SelectListItem> QualificationNames { get; set; }
        public int? QualificationId { get; set; }

        [Display(Name = "Marital Status")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Marital Status")]
        public string MarritalStatus { get; set; }

        [Display(Name = "AadharNo")]
        public string Aadhar { get; set; }

        [Display(Name = "PAN No")]
        public string PAN { get; set; }
    }
  
}
