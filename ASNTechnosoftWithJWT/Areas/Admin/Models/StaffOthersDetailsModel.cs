using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class StaffOthersDetailsModel
    {
        [Display(Name = "Designation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Designation")]
        public IList<SelectListItem> DesignationNames { get; set; }
        public int DesignationId { get; set; }

        [Display(Name = "Salary Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select Salary Type")]
        public SalaryType salaryType { get; set; }

        [Display(Name ="Transaction Limit")]
        public decimal TransactionLimit { get; set; }

        [Display(Name = "Upload Bank Proof")]
        public string PhotoUpload { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        public int InstituteId { get; set; }
    }
}

