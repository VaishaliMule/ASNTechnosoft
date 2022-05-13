using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    public class StaffWizardFormModel
    {
        public int StaffId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email Id")]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string DOB { get; set; }

        [Display(Name = "Qualification")]
        public List<SelectListItem> QualificationNames { get; set; }
        public int? QualificationId { get; set; }

        public string Qualification { get; set; }

        [Display(Name = "Marital Status")]
        public string MarritalStatus { get; set; }

        [Display(Name = "Aadhar No")]
        [RegularExpression(@"^(\d{12}|\d{16})$", ErrorMessage = "enter Integers only")]
        public string Aadhar { get; set; }

        [Display(Name = "PAN No")]
        public string PAN { get; set; }

        //[Display(Name = "Country")]
        //public IList<SelectListItem> CountryNames { get; set; }
        //public int CountryId { get; set; }

        [Display(Name = "State")]
        public IList<SelectListItem> StateNames { get; set; }
        public int StateId { get; set; }
        public string CState { get; set; }

        [Display(Name = "District")]
        public IList<SelectListItem> DistrictNames { get; set; }
        public int DistrictId { get; set; }
        public string CDistrict { get; set; }

        [Display(Name = "Tahsil/Block")]
        public IList<SelectListItem> TahsilNames { get; set; }
        public int TahsilId { get; set; }
        public string CTaluka { get; set; }

        [Display(Name = "SubUrbanArea")]
        public IList<SelectListItem> AreaNames { get; set; }
        public int AreaId { get; set; }
      
        public string CLocationArea { get; set; }
        [Display(Name = "Address")]
        public string CAddress { get; set; }

        [Display(Name = "Pin Code")]
        [MaxLength(6)]
        [StringLength(6, MinimumLength = 6)]
        public string PinCode { get; set; }

        //[Display(Name = "Country")]
        //public IList<SelectListItem> PCountryNames { get; set; }
        //public int? PCountryId { get; set; }

        [Display(Name = "State")]
        public IList<SelectListItem> PStateNames { get; set; }
        public int? PStateId { get; set; }
        public string PState { get; set; }

        [Display(Name = "District")]
        public IList<SelectListItem> PDistrictNames { get; set; }
        public int? PDistrictId { get; set; }
        public string PDistrict { get; set; }

        [Display(Name = "Tahsil/Block")]
        public IList<SelectListItem> PTahsilNames { get; set; }
        public int? PTahsilId { get; set; }
        public string PTaluka { get; set; }

        [Display(Name = "SubUrbanArea")]
        public IList<SelectListItem> PAreaNames { get; set; }
        public int PAreaId { get; set; }
        public string PLocationArea { get; set; }

        [Display(Name = "Address")]
        public string PAddress { get; set; }

        [Display(Name = "Pin Code")]
        [MaxLength(6)]
        [StringLength(6, MinimumLength = 6)]
        public string PPinCode { get; set; }

        [Display(Name = "Mobile")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string Mobile { get; set; }

        [Display(Name = "Alternate Mobile")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Please Enter Valid Mobile Number.")]
        public string AlternateMobile { get; set; }

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

        public string BProofFilename { get; set; }

        [Display(Name = "Designation")]
      
        public IList<SelectListItem> DesignationNames { get; set; }
        public int DesignationId { get; set; }
        public string Designation { get; set; }

        [Display(Name = "Salary Type")]
      
        public SalaryType salaryType { get; set; }

        [Display(Name = "Transaction Limit")]
        public string TransactionLimit { get; set; }

        [Display(Name = "Upload Bank Proof")]
        public string PhotoUpload { get; set; }
        public HttpPostedFileBase PhotoImageFile { get; set; }
        public string Photofilename { get; set; }
        public int InstituteId { get; set; }
    }
}

[Flags]
public enum GenderList
{
    Select_Gender=0,
    Male=1,
    Female=2,
    TransGender=3  
}

[Flags]
public enum MaritalStatus
{
    Select_Marrital_Statu=0,
    Married=1,
    Single=2,
    Divorcee=3,
    Widow=4
}

public enum SalaryType
{
    Fixed,
    Intensive,
    Fixed_and_Intensive
}