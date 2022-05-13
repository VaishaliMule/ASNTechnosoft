using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class BankDetailsModel
    {

        [Display(Name = "Do you have bank details?")]
        public string HaveBankDetails { get; set; }

        [Display(Name = "Account Holder Name")]
        public string AccountHolderName { get; set; }

        [Display(Name = "Account Number")]
        public string BankAccountNo { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Branch Name")]
        public string BankBranch { get; set; }

        [Display(Name = "IFSC Code")]
        public string IFSCCode { get; set; }

        [Display(Name = "Upload Bank Proof")]
        public string BankProofUpload { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}