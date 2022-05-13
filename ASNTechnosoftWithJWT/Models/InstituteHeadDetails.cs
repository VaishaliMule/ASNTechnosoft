using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Models
{
    public class InstituteHeadDetails
    {
        [Display(Name="First Name")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="First name required")]
        public string FirstName { get; set; }

        [Display(Name ="Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name ="Last Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name ="Mobile")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Mobile number required")]
        public string Mobile { get; set; }

        [Display(Name = "Alternate Mobile")]
        public string AlternateMobile { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide gender")]
        public string Gender { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of Birth required")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Aadhar No")]
        public string AADHAR { get; set; }

        [Display(Name = "PAN No")]
        public string PAN { get; set; }

    }
}