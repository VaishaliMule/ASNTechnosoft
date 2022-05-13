using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class AddressModel
    {
        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Country")]
        public IList<SelectListItem> CountryNames { get; set; }

        public int CountryId { get; set; }

        [Display(Name = "State")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select State")]
        public IList<SelectListItem> StateNames { get; set; }
        public int StateId { get; set; }

        [Display(Name = "District")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select District")]
        public IList<SelectListItem> DistrictNames { get; set; }
        public int DistrictId { get; set; }

        [Display(Name = "Tahsil/Block")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Tahsil/Block")]
        public IList<SelectListItem> TahsilNames { get; set; }
        public int TahsilId { get; set; }

        [Display(Name = "SubUrbanArea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Location")]
        public IList<SelectListItem> AreaNames { get; set; }
        public int AreaId { get; set; }

        [Display(Name = "Pin Code")]
        [MaxLength(6)]
        [StringLength(6, MinimumLength = 6)]
        public string PinCode { get; set; }

        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Country")]
        public IList<SelectListItem> PCountryNames { get; set; }
        public int? PCountryId { get; set; }

        [Display(Name = "State")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select State")]
        public IList<SelectListItem> PStateNames { get; set; }
        public int? PStateId { get; set; }

        [Display(Name = "District")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select District")]
        public IList<SelectListItem> PDistrictNames { get; set; }
        public int? PDistrictId { get; set; }

        [Display(Name = "Tahsil/Block")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Tahsil/Block")]
        public IList<SelectListItem> PTahsilNames { get; set; }
        public int? PTahsilId { get; set; }

        [Display(Name = "SubUrbanArea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Location")]
        public IList<SelectListItem> PAreaNames { get; set; }
        public int  PAreaId { get; set; }

        [Display(Name = "Pin Code")]
        [MaxLength(6)]
        [StringLength(6, MinimumLength = 6)]
        public string PPinCode { get; set; }

        [Display(Name = "Mobile")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile number required")]
        public string Mobile { get; set; }

        [Display(Name = "Alternate Mobile")]
        public string AlternateMobile { get; set; }

    }
}