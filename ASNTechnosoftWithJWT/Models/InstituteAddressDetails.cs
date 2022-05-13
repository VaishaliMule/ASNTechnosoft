using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Models
{
    public class InstituteAddressDetails
    {
        [Display(Name = "Select State")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select State")]
        public int StateId { get; set; }

        [Display(Name = "Select District")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select District")]
        public int DistrictId { get; set; }

        [Display(Name = "Select Tahsil/Block")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Taluka/Block")]
        public int TalukaId { get; set; }


        [Display(Name = "Pin Code")]
        public string pincode { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }


        [Display(Name = "Rural/Urban")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Rural/Urban")]
        public string Gender { get; set; }
    }
}