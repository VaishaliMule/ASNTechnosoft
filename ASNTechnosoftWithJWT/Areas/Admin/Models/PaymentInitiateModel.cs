using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class PaymentInitiateModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        [Display(Name = "Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter amount")]
        public int amount { get; set; }
        public int InstituteId { get; set; }
    }
}