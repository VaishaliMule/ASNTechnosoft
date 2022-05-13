using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class EOIModel
    {
        public int PurchasedId { get; set; }
        public int InstituteId { get; set; }
        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; }
        public int SubscriptionServiceId { get; set; }
        [Display(Name = "Service Name")]
        public IList<SelectListItem> ServicesNames { get; set; }
        [Display(Name = "Subcribed Service")]
        public string ServiceName { get; set; }
        [Display(Name = "Quantity")]
        public string Quantity { get; set; }
        [Display(Name = "Amount")]
        public string Amount { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        public string walletAmount { get; set; }
        public string RenewalDate { get; set; }
        public string SubcribedDate { get; set; }
    }
}