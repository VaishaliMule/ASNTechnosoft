using ASNTechnosoftWithJWT.Areas.Admin.Models;
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnosoftModel;
using static ASNTechnosoftWithJWT.Areas.Admin.Models.Enum;

namespace ASNTechnosoftWithJWT.Areas.Admin.Controllers
{
    public class StaffDataTableController : BaseController
    {
        // GET: InstituteAdmin/StaffDataTable
        public ActionResult Index()
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            List<User> listStaff = UsersBL.GetAll(Convert.ToInt32(Usermodel.InstituteId));
            return View(listStaff);
        }

        //public ActionResult ShowGrid()
        //{
        //    return View();
        //}
        //[HttpGet]
        //public ActionResult GetList()
        //{
        //    User Usermodel = TempData["UserObject"] as User;
        //    TempData.Keep();
        //    List<User> listStaff = UsersBL.GetAll(Convert.ToInt32(Usermodel.InstituteId));
        //    return Json(new { data = listStaff }, JsonRequestBehavior.AllowGet);
        //}
        private static List<SelectListItem> PopulateQualification()
        {
            List<SelectListItem> qualificationNames = new List<SelectListItem>();
            List<Qualification> qualifications = QualificationBL.GetAll();
            qualifications.ForEach(x =>
            {
                qualificationNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            });
            return qualificationNames;
        }
        private static List<SelectListItem> PopulateStateNames()
        {
            List<SelectListItem> stateNames = new List<SelectListItem>();
            List<State> states = StateBL.GetByCountryId(1);
            states.ForEach(x =>
            {
                stateNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            });
            return stateNames;
        }
        private static List<SelectListItem> PopulateDistrictNames()
        {
            List<SelectListItem> districtNames = new List<SelectListItem>();
            List<District> districts = DistrictBL.GetAll();
            districts.ForEach(x =>
            {
                districtNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            });
            return districtNames;
        }
        private static List<SelectListItem> PopulateTalukaNames()
        {
            List<SelectListItem> talukaNames = new List<SelectListItem>();
            List<Taluka> talukas = TalukaBL.GetAll();
            talukas.ForEach(x =>
            {
                talukaNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            });
            return talukaNames;
        }
        private static List<SelectListItem> PopulateAreaNames()
        {
            List<SelectListItem> areaNames = new List<SelectListItem>();
            List<Suburb> areas = SuburbBL.GetAll();
            areas.ForEach(x =>
            {
                areaNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            });
            return areaNames;
        }
        private static List<SelectListItem> PopulateDesignation()
        {
            List<SelectListItem> designationNames = new List<SelectListItem>();

            List<Designation> designations = DesignationBL.GetAll();
            designations.ForEach(x =>
            {
                designationNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            });
            return designationNames;
        }

        [HttpPost]
        public ActionResult GetState(string CountryId)
        {

            int countryId;
            List<SelectListItem> stateNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(CountryId))
            {
                countryId = Convert.ToInt32(CountryId);
                List<State> states = StateBL.GetByCountryId(countryId);
                states.ForEach(x =>
                {
                    stateNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                });
            }

            return Json(stateNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDistrict(string stateId)
        {

            int statId;
            List<SelectListItem> districtNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(stateId))
            {
                statId = Convert.ToInt32(stateId);
                List<District> districts = DistrictBL.GetByStateId(statId);
                districts.ForEach(x =>
                {
                    districtNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                });
            }
            return Json(districtNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetTahsil(string districtId)
        {

            int DistrictId;
            List<SelectListItem> tahsilNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(districtId))
            {
                DistrictId = Convert.ToInt32(districtId);
                List<Taluka> tahsils = TalukaBL.GetTalukaByDistrictId(DistrictId);
                tahsils.ForEach(x =>
                {
                    tahsilNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                });
            }
            return Json(tahsilNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSubUrbanArea(string talukaId)
        {

            int TalukaId;
            List<SelectListItem> subUrbanAreas = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(talukaId))
            {
                TalukaId = Convert.ToInt32(talukaId);
                List<Suburb> areas = SuburbBL.GetSubUrbanByCity(TalukaId);
                areas.ForEach(x =>
                {
                    subUrbanAreas.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                });
            }
            return Json(subUrbanAreas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            StaffWizardFormModel dataModel = new StaffWizardFormModel();
            dataModel.QualificationNames = PopulateQualification();
            dataModel.StateNames = PopulateStateNames();
            dataModel.DistrictNames = PopulateDistrictNames();
            dataModel.TahsilNames = PopulateTalukaNames();
            dataModel.PStateNames = PopulateStateNames();
            dataModel.PDistrictNames = PopulateDistrictNames();
            dataModel.PTahsilNames = PopulateTalukaNames();
            dataModel.AreaNames = PopulateAreaNames();
            dataModel.PAreaNames = PopulateAreaNames();
            dataModel.DesignationNames = PopulateDesignation();
            return View("_CreatePartial", dataModel);
        }
        private User GetStaff()
        {
            if (Session["user"] == null)
            {
                Session["user"] = new User();
            }
            return (User)Session["user"];
        }

        private void RemoveStaff()
        {
            Session.Remove("user");
        }

        [HttpPost]
        public ActionResult Create(StaffWizardFormModel data, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                User obj = new User();
                obj.FirstName = data.FirstName;
                obj.MiddleName = data.MiddleName;
                obj.LastName = data.LastName;
                obj.Gender = data.Gender.ToString();
                obj.Username = data.Email;
                obj.DOB = Convert.ToDateTime(data.DOB);
                obj.QualificationId = Convert.ToInt32(data.QualificationId);
                obj.MarritalStatus = data.MarritalStatus.ToString();
                obj.Aadhar = data.Aadhar;
                obj.PAN = data.PAN;
                obj.CAddress = data.CAddress;
                obj.CStateId = data.StateId;
                obj.CDistrictId = data.DistrictId;
                obj.PDistrictId = data.PDistrictId;
                data.CLocationArea = SuburbBL.GetById(data.AreaId).Name;
                obj.CSuburbId = data.AreaId;
                obj.CPinCode = data.PinCode;
                obj.CStateId = data.StateId;
                obj.CTalukaId = data.TahsilId;
                if (data.PAreaId == 0)
                {
                    data.PLocationArea = null;
                }
                else
                {
                    data.PLocationArea = SuburbBL.GetById(data.PAreaId).Name;
                }

                obj.PAddress = data.PAddress;
                obj.PPinCode = data.PPinCode;
                obj.PStateId = data.PStateId;
                obj.PTalukaId = data.PTahsilId;
                obj.PSuburbId = data.PAreaId;
                obj.AlternateMobileNo = data.AlternateMobile;
                obj.MobileNo = data.Mobile;
                if (data.HaveBankDetails == "Yes")
                {
                    obj.AccountHolderName = data.AccountHolderName;
                    obj.BankAccountNo = data.BankAccountNo;
                    obj.BankBranch = data.BankBranch;
                    obj.BankName = data.BankName;
                    HttpPostedFileBase Imagefile = data.ImageFile;
                    if (Imagefile != null && Imagefile.ContentLength > 0)
                    {
                        string FileName = Path.GetFileNameWithoutExtension(Imagefile.FileName);
                        string FileExtension = Path.GetExtension(Imagefile.FileName);
                        FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                        string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
                        data.PhotoUpload = UploadPath.ToString();
                        Imagefile.SaveAs(data.PhotoUpload);
                        obj.BankProofUpload = data.PhotoUpload.ToString();
                        data.BProofFilename = FileName;
                        obj.IsBankProofUpload = true;
                    }
                    else
                    {
                        obj.BankProofUpload = null;
                        obj.IsBankProofUpload = false;
                    }
                    obj.IFSCCode = data.IFSCCode;
                }
                else
                {
                    obj.AccountHolderName = null;
                    obj.BankAccountNo = null;
                    obj.BankBranch = null;
                    obj.BankName = null;
                    obj.BankProofUpload = null;
                    obj.IsBankProofUpload = false;
                    obj.IFSCCode = null;
                }
                obj.DesignationId = data.DesignationId;
                obj.SalaryType = data.salaryType.ToString();
                obj.TransactionLimit = Convert.ToDecimal(data.TransactionLimit);
                obj.DateOfJioning = DateTime.Now;
                obj.DateOfLeaving = null;

                obj.CashInHand = Convert.ToDecimal(0.00);
                obj.IsDeleted = false;
                HttpPostedFileBase Photofile = data.PhotoImageFile;
                if (Photofile != null && Photofile.ContentLength > 0)
                {
                    string FileName = Path.GetFileNameWithoutExtension(Photofile.FileName);
                    string FileExtension = Path.GetExtension(Photofile.FileName);
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                    string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
                    data.PhotoUpload = UploadPath.ToString();
                    Photofile.SaveAs(data.PhotoUpload);
                    obj.PhotoUpload = data.PhotoUpload.ToString();
                    data.Photofilename = FileName;
                    obj.IsPhotoUpload = true;
                }
                else
                {
                    obj.PhotoUpload = null;
                    obj.IsPhotoUpload = false;
                }

                string strNewPassword = GeneratePassword().ToString();
                obj.Password = strNewPassword;
                User Usermodel = TempData["UserObject"] as User;
                TempData.Keep();
                obj.InstituteId = Convert.ToInt32(Usermodel.InstituteId);
                obj.IsActive = true;
                obj.RoleId = 5;

                if (UsersBL.Add(obj) == true)
                {
                    RemoveStaff();
                    EmailBL.SendStaffRegistrationEmail(obj);
                    SweetAlert("Done", "User added Successfully.! Login details send on your email id. ", NotificationType.success);
                    return RedirectToAction("Index", "StaffDataTable");
                }
            }
            return View();
        }

        public string GeneratePassword()
        {
            string PasswordLength = "8";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }
        public ActionResult ReviewPartial(string prevBtn, string nextBtn)
        {
            User obj = GetStaff();
            StaffWizardFormModel model = TempData["Review"] as StaffWizardFormModel;
            //  TempData.Keep();
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    obj.IsDeleted = false;
                    string strNewPassword = GeneratePassword().ToString();
                    obj.Password = strNewPassword;
                    User Usermodel = TempData["UserObject"] as User;
                    TempData.Keep();
                    obj.InstituteId = Convert.ToInt32(Usermodel.InstituteId);

                    User userobj = new User();
                    userobj.Aadhar = obj.Aadhar;
                    userobj.AccountHolderName = obj.AccountHolderName;
                    userobj.AlternateMobileNo = obj.AlternateMobileNo;
                    userobj.BankAccountNo = obj.BankAccountNo;
                    userobj.BankBranch = obj.BankBranch;
                    userobj.BankName = obj.BankName;
                    userobj.BankProofUpload = obj.BankProofUpload;
                    userobj.CAddress = obj.CAddress;
                    userobj.CashInHand = obj.CashInHand;
                    userobj.CDistrictId = obj.CDistrictId;
                    userobj.CSuburbId = obj.CSuburbId;
                    userobj.CPinCode = obj.CPinCode;
                    userobj.CStateId = obj.CStateId;
                    userobj.CTalukaId = obj.CTalukaId;
                    userobj.DateOfJioning = DateTime.Now;
                    userobj.DateOfLeaving = null;
                    userobj.DesignationId = obj.DesignationId;
                    userobj.DOB = obj.DOB;
                    userobj.Username = obj.Username;
                    userobj.FirstName = obj.FirstName;
                    userobj.Gender = obj.Gender;
                    userobj.IFSCCode = obj.IFSCCode;
                    userobj.InstituteId = obj.InstituteId;
                    userobj.IsBankProofUpload = obj.IsBankProofUpload;
                    userobj.IsDeleted = false;
                    userobj.IsPhotoUpload = obj.IsPhotoUpload;
                    userobj.LastName = obj.LastName;
                    userobj.MarritalStatus = obj.MarritalStatus;
                    userobj.MiddleName = obj.MiddleName;
                    userobj.MobileNo = obj.MobileNo;
                    userobj.PAddress = obj.PAddress;
                    userobj.PAN = obj.PAN;
                    userobj.Password = obj.Password;
                    userobj.PDistrictId = obj.PDistrictId;
                    userobj.PhotoUpload = obj.PhotoUpload;
                    userobj.PSuburbId = obj.PSuburbId;
                    userobj.PPinCode = obj.PPinCode;
                    userobj.PStateId = obj.PStateId;
                    userobj.PTalukaId = obj.PTalukaId;
                    userobj.QualificationId = obj.QualificationId;
                    userobj.SalaryType = obj.SalaryType;
                    userobj.TransactionLimit = obj.TransactionLimit;
                    if (UsersBL.Add(userobj) == true)
                    {
                        RemoveStaff();
                    }
                    SweetAlert("Delete", "User Saved Successfully", NotificationType.success);
                    return RedirectToAction("Success");
                }
            }

            if (prevBtn != null)
            {
                StaffWizardFormModel data = new StaffWizardFormModel();
                data.DesignationId = Convert.ToInt32(obj.DesignationId);
                data.DesignationNames = PopulateDesignation();
                SalaryType salarytype = (SalaryType)System.Enum.Parse(typeof(SalaryType), obj.SalaryType);
                data.salaryType = salarytype;
                data.TransactionLimit = obj.TransactionLimit.ToString();

                data.PhotoUpload = obj.PhotoUpload;
                string photoPath = obj.PhotoUpload;
                string photofilename = null;
                photofilename = Path.GetFileName(photoPath);
                data.Photofilename = photofilename;

                data.Aadhar = obj.Aadhar;
                // data.DOB = obj.DOB;
                if (obj.DOB != null)
                {
                    DateTime? myDate = obj.DOB;
                    data.DOB = myDate.Value.ToString("MM/dd/yyyy");
                }

                data.Email = obj.Username;
                data.FirstName = obj.FirstName;

                data.Gender = obj.Gender;
                data.LastName = obj.LastName;

                data.MarritalStatus = obj.MarritalStatus;
                data.QualificationNames = PopulateQualification();
                data.QualificationId = obj.QualificationId;
                data.MiddleName = obj.MiddleName;
                data.PAN = obj.PAN;
                data.AlternateMobile = obj.AlternateMobileNo;
                data.AreaNames = PopulateAreaNames();
                data.DistrictNames = PopulateDistrictNames();
                data.Mobile = obj.MobileNo;
                data.PAreaNames = PopulateAreaNames();
                data.PDistrictNames = PopulateDistrictNames();
                data.PinCode = obj.CPinCode;
                data.PPinCode = obj.PPinCode;
                data.PStateNames = PopulateStateNames();
                data.PTahsilNames = PopulateTalukaNames();
                data.StateNames = PopulateStateNames();
                data.TahsilNames = PopulateTalukaNames();
                data.DistrictId = Convert.ToInt32(obj.CDistrictId);
                //Suburb suburb1 = SuburbBL.GetByName(obj.CLocationArea);
                data.AreaId = Convert.ToInt32(obj.CSuburbId);
                data.PStateId = obj.PStateId;
                data.PDistrictId = obj.PDistrictId;
                data.PTahsilId = obj.PTalukaId;
                data.StateId = Convert.ToInt32(obj.CStateId);
                data.TahsilId = Convert.ToInt32(obj.CTalukaId);
                //  Suburb suburb2 = SuburbBL.GetByName(obj.PLocationArea);
                data.PAreaId = Convert.ToInt32(obj.PSuburbId);
                data.AccountHolderName = obj.AccountHolderName;
                data.BankAccountNo = obj.BankAccountNo;
                data.BankBranch = obj.BankBranch;
                data.BankName = obj.BankName;
                data.BankProofUpload = obj.BankProofUpload;
                data.IFSCCode = obj.IFSCCode;

                data.PhotoUpload = obj.PhotoUpload;
                data.CAddress = obj.CAddress;
                data.PAddress = obj.PAddress;

                string bankProofPath = obj.BankProofUpload;
                string Bankfilename = null;
                Bankfilename = Path.GetFileName(bankProofPath);
                data.BProofFilename = Bankfilename;


                return RedirectToAction("TestStaffWizard", data);
            }
            else
            {
                return PartialView("_ReviewPartial", model);
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            int instituteid = Convert.ToInt32(Usermodel.InstituteId);
            User obj = UsersBL.GetById(Convert.ToInt32(id), instituteid);
            if (Usermodel == null)
            {
                return HttpNotFound();
            }
            else
            {
                StaffWizardFormModel data = new StaffWizardFormModel();
                data.StaffId = Convert.ToInt32(id);
                if (obj.DesignationId != null)
                {
                    data.DesignationId = Convert.ToInt32(obj.DesignationId);
                }
                data.DesignationNames = PopulateDesignation();
                if (obj.SalaryType != null)
                {
                    SalaryType salarytype = (SalaryType)System.Enum.Parse(typeof(SalaryType), obj.SalaryType);
                    data.salaryType = salarytype;
                }

                data.TransactionLimit = obj.TransactionLimit.ToString();
                if (obj.PhotoUpload != null)
                {
                    data.PhotoUpload = obj.PhotoUpload;
                    string photoPath = obj.PhotoUpload;
                    string photofilename = null;
                    photofilename = Path.GetFileName(photoPath);
                    data.Photofilename = photofilename;
                }
                data.Aadhar = obj.Aadhar;
                if (obj.DOB != null)
                {
                    DateTime? myDate = obj.DOB;
                    data.DOB = myDate.Value.ToShortDateString();
                }
                data.Email = obj.Username;
                data.FirstName = obj.FirstName;
                data.Gender = obj.Gender;
                data.LastName = obj.LastName;
                data.MarritalStatus = obj.MarritalStatus;
                data.QualificationNames = PopulateQualification();
                data.QualificationId = obj.QualificationId;
                data.MiddleName = obj.MiddleName;
                data.PAN = obj.PAN;
                data.AlternateMobile = obj.AlternateMobileNo;

                data.StateNames = PopulateStateNames();
                if (obj.CStateId != null)
                {
                    int stateId = Convert.ToInt32(obj.CStateId);
                    List<SelectListItem> districtNames = new List<SelectListItem>();
                    if (!string.IsNullOrEmpty(stateId.ToString()))
                    {
                        List<District> districts = DistrictBL.GetByStateId(stateId);
                        districts.ForEach(x =>
                        {
                            districtNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                        });
                    }
                    data.DistrictNames = districtNames;
                }
                else
                {
                    data.DistrictNames = PopulateDistrictNames();
                }

                if (obj.CDistrictId != null)
                {
                    int DistrictId = Convert.ToInt32(obj.CDistrictId);
                    List<SelectListItem> tahsilNames = new List<SelectListItem>();
                    if (!string.IsNullOrEmpty(DistrictId.ToString()))
                    {
                        List<Taluka> tahsils = TalukaBL.GetTalukaByDistrictId(DistrictId);
                        tahsils.ForEach(x =>
                        {
                            tahsilNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                        });
                    }
                    data.TahsilNames = tahsilNames;
                }
                else
                {
                    data.TahsilNames = PopulateTalukaNames();
                }

                if (obj.CTalukaId != null)
                {
                    int TalukaId = Convert.ToInt32(obj.CTalukaId);
                    List<SelectListItem> subUrbanAreas = new List<SelectListItem>();
                    if (!string.IsNullOrEmpty(TalukaId.ToString()))
                    {
                        List<Suburb> areas = SuburbBL.GetSubUrbanByCity(TalukaId);
                        areas.ForEach(x =>
                        {
                            subUrbanAreas.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                        });
                    }
                    data.AreaNames = subUrbanAreas;
                }
                else
                {
                    data.AreaNames = PopulateAreaNames();
                }
                data.PStateNames = PopulateStateNames();
                if (obj.PStateId != null)
                {
                    int stateId = Convert.ToInt32(obj.PStateId);
                    List<SelectListItem> districtNames = new List<SelectListItem>();
                    if (!string.IsNullOrEmpty(stateId.ToString()))
                    {
                        List<District> districts = DistrictBL.GetByStateId(stateId);
                        districts.ForEach(x =>
                        {
                            districtNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                        });
                    }
                    data.PDistrictNames = districtNames;
                }
                else
                {
                    data.PDistrictNames = PopulateDistrictNames();
                }

                if (obj.PDistrictId != null)
                {
                    int DistrictId = Convert.ToInt32(obj.PDistrictId);
                    List<SelectListItem> tahsilNames = new List<SelectListItem>();
                    if (!string.IsNullOrEmpty(DistrictId.ToString()))
                    {
                        List<Taluka> tahsils = TalukaBL.GetTalukaByDistrictId(DistrictId);
                        tahsils.ForEach(x =>
                        {
                            tahsilNames.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                        });
                    }
                    data.PTahsilNames = tahsilNames;
                }
                else
                {
                    data.PTahsilNames = PopulateTalukaNames();
                }

                if (obj.PTalukaId != null)
                {
                    int TalukaId = Convert.ToInt32(obj.PTalukaId);
                    List<SelectListItem> subUrbanAreas = new List<SelectListItem>();
                    if (!string.IsNullOrEmpty(TalukaId.ToString()))
                    {
                        List<Suburb> areas = SuburbBL.GetSubUrbanByCity(TalukaId);
                        areas.ForEach(x =>
                        {
                            subUrbanAreas.Add(new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
                        });
                    }
                    data.PAreaNames = subUrbanAreas;
                }
                else
                {
                    data.PAreaNames = PopulateAreaNames();
                }
                data.Mobile = obj.MobileNo;
                data.PinCode = obj.CPinCode;
                data.PPinCode = obj.PPinCode;
                data.DistrictId = Convert.ToInt32(obj.CDistrictId);
                data.AreaId = Convert.ToInt32(obj.CSuburbId);
                data.PStateId = obj.PStateId;
                data.PDistrictId = obj.PDistrictId;
                data.PTahsilId = obj.PTalukaId;
                data.StateId = Convert.ToInt32(obj.CStateId);
                data.TahsilId = Convert.ToInt32(obj.CTalukaId);
                data.PAreaId = Convert.ToInt32(obj.PSuburbId);
                data.AccountHolderName = obj.AccountHolderName;
                data.BankAccountNo = obj.BankAccountNo;
                data.BankBranch = obj.BankBranch;
                data.BankName = obj.BankName;
                data.IFSCCode = obj.IFSCCode;
                data.CAddress = obj.CAddress;
                data.PAddress = obj.PAddress;
                if (obj.BankProofUpload != null)
                {
                    string bankProofPath = obj.BankProofUpload;
                    data.BankProofUpload = obj.BankProofUpload;
                    string Bankfilename = null;
                    Bankfilename = Path.GetFileName(bankProofPath);
                    data.BProofFilename = Bankfilename;
                }
                return View("_EditPartial", data);
            }

        }
        [HttpPost]
        public ActionResult Edit(StaffWizardFormModel staffVM)
        {
            if (staffVM.StaffId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            int instituteid = Convert.ToInt32(Usermodel.InstituteId);
            User obj = UsersBL.GetById(Convert.ToInt32(staffVM.StaffId), instituteid);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    obj.FirstName = staffVM.FirstName;
                    obj.MiddleName = staffVM.MiddleName;
                    obj.LastName = staffVM.LastName;
                    obj.Gender = staffVM.Gender.ToString();
                    obj.Username = staffVM.Email;
                    obj.DOB = Convert.ToDateTime(staffVM.DOB);
                    obj.QualificationId = Convert.ToInt32(staffVM.QualificationId);
                    obj.MarritalStatus = staffVM.MarritalStatus.ToString();
                    obj.Aadhar = staffVM.Aadhar;
                    obj.PAN = staffVM.PAN;
                    obj.CAddress = staffVM.CAddress;
                    obj.CStateId = staffVM.StateId;
                    obj.CDistrictId = staffVM.DistrictId;
                    obj.PDistrictId = staffVM.PDistrictId;
                    staffVM.CLocationArea = SuburbBL.GetById(staffVM.AreaId).Name;
                    obj.CSuburbId = staffVM.AreaId;
                    obj.CPinCode = staffVM.PinCode;
                    obj.CStateId = staffVM.StateId;
                    obj.CTalukaId = staffVM.TahsilId;
                    if (staffVM.PAreaId == 0)
                    {
                        staffVM.PLocationArea = null;
                    }
                    else
                    {
                        staffVM.PLocationArea = SuburbBL.GetById(staffVM.PAreaId).Name;
                    }

                    obj.PAddress = staffVM.PAddress;
                    obj.PPinCode = staffVM.PPinCode;
                    obj.PStateId = staffVM.PStateId;
                    obj.PTalukaId = staffVM.PTahsilId;
                    obj.PSuburbId = staffVM.PAreaId;
                    obj.AlternateMobileNo = staffVM.AlternateMobile;
                    obj.MobileNo = staffVM.Mobile;
                    if (staffVM.HaveBankDetails == "Yes")
                    {
                        obj.AccountHolderName = staffVM.AccountHolderName;
                        obj.BankAccountNo = staffVM.BankAccountNo;
                        obj.BankBranch = staffVM.BankBranch;
                        obj.BankName = staffVM.BankName;
                        HttpPostedFileBase Imagefile = staffVM.ImageFile;
                        if (Imagefile != null && Imagefile.ContentLength > 0)
                        {
                            string FileName = Path.GetFileNameWithoutExtension(Imagefile.FileName);
                            string FileExtension = Path.GetExtension(Imagefile.FileName);
                            FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                            string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
                            staffVM.PhotoUpload = UploadPath.ToString();
                            Imagefile.SaveAs(staffVM.PhotoUpload);
                            obj.BankProofUpload = staffVM.PhotoUpload.ToString();
                            staffVM.BProofFilename = FileName;
                            obj.IsBankProofUpload = true;
                        }
                        else
                        {
                            obj.BankProofUpload = null;
                            obj.IsBankProofUpload = false;
                        }
                        obj.IFSCCode = staffVM.IFSCCode;
                    }
                    else
                    {
                        obj.AccountHolderName = null;
                        obj.BankAccountNo = null;
                        obj.BankBranch = null;
                        obj.BankName = null;
                        obj.BankProofUpload = null;
                        obj.IsBankProofUpload = false;
                        obj.IFSCCode = null;
                    }
                    obj.DesignationId = staffVM.DesignationId;
                    obj.SalaryType = staffVM.salaryType.ToString();
                    obj.TransactionLimit = Convert.ToDecimal(staffVM.TransactionLimit);
                    obj.DateOfJioning = DateTime.Now;
                    obj.DateOfLeaving = null;

                    obj.CashInHand = Convert.ToDecimal(0.00);
                    obj.IsDeleted = false;
                    HttpPostedFileBase Photofile = staffVM.PhotoImageFile;
                    if (Photofile != null && Photofile.ContentLength > 0)
                    {
                        string FileName = Path.GetFileNameWithoutExtension(Photofile.FileName);
                        string FileExtension = Path.GetExtension(Photofile.FileName);
                        FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                        string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
                        staffVM.PhotoUpload = UploadPath.ToString();
                        Photofile.SaveAs(staffVM.PhotoUpload);
                        obj.PhotoUpload = staffVM.PhotoUpload.ToString();
                        staffVM.Photofilename = FileName;
                        obj.IsPhotoUpload = true;
                    }
                    else
                    {
                        obj.PhotoUpload = null;
                        obj.IsPhotoUpload = false;
                    }
                    obj.InstituteId = instituteid;
                    if (UsersBL.Edit(obj) == true)
                    {
                        if (Usermodel.RoleId == 1)
                        {
                            Institute instObj = InstituteBL.GetById(instituteid);
                            instObj.FirstName = staffVM.FirstName;
                            instObj.MiddleName = staffVM.MiddleName;
                            instObj.LastName = staffVM.LastName;
                            instObj.Mobile = staffVM.Mobile;
                            instObj.Email = staffVM.Email;
                            InstituteBL.Edit(instObj);
                        }
                        SweetAlert("Done", "User updated Successfully", NotificationType.success);
                        return RedirectToAction("Index", "StaffDataTable");
                    }

                }
                else
                {
                    SweetAlert("Oops...", "Something went wrong!", NotificationType.error);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            int instituteid = Convert.ToInt32(Usermodel.InstituteId);
            User obj = UsersBL.GetById(Convert.ToInt32(id), instituteid);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                StaffWizardFormModel data = new StaffWizardFormModel();
                data.StaffId = Convert.ToInt32(id);
                if (obj.SalaryType != null)
                {
                    SalaryType salarytype = (SalaryType)System.Enum.Parse(typeof(SalaryType), obj.SalaryType);
                    data.salaryType = salarytype;
                }
                data.TransactionLimit = obj.TransactionLimit.ToString();
                if (obj.PhotoUpload != null)
                {
                    data.PhotoUpload = obj.PhotoUpload;
                    string photoPath = obj.PhotoUpload;
                    string photofilename = null;
                    photofilename = Path.GetFileName(photoPath);
                    data.Photofilename = photofilename;
                }

                data.Aadhar = obj.Aadhar;
                if (obj.DOB != null)
                {
                    DateTime? myDate = obj.DOB;
                    data.DOB = myDate.Value.ToString("MM/dd/yyyy");
                }
                data.Email = obj.Username;
                data.FirstName = obj.FirstName;
                data.Gender = obj.Gender;
                data.LastName = obj.LastName;
                data.MarritalStatus = obj.MarritalStatus;
                data.MiddleName = obj.MiddleName;
                data.PAN = obj.PAN;
                data.AlternateMobile = obj.AlternateMobileNo;

                data.Mobile = obj.MobileNo;

                data.PinCode = obj.CPinCode;
                data.PPinCode = obj.PPinCode;

                if (obj.AccountHolderName != null && obj.BankAccountNo != null)
                {
                    data.HaveBankDetails = "Yes";
                    data.AccountHolderName = obj.AccountHolderName;
                    data.BankAccountNo = obj.BankAccountNo;
                    data.BankBranch = obj.BankBranch;
                    data.BankName = obj.BankName;
                    if (obj.BankProofUpload != null)
                    {
                        data.BankProofUpload = obj.BankProofUpload;
                        string bankProofPath = obj.BankProofUpload;
                        string Bankfilename = null;
                        Bankfilename = Path.GetFileName(bankProofPath);
                        data.BProofFilename = Bankfilename;
                    }
                    data.IFSCCode = obj.IFSCCode;
                }
                else
                {
                    data.HaveBankDetails = "No";
                    data.AccountHolderName = "";
                    data.BankAccountNo = "";
                    data.BankBranch = "";
                    data.BankName = "";
                    data.BankProofUpload = "";
                    data.IFSCCode = "";
                }

                data.CAddress = obj.CAddress;
                data.PAddress = obj.PAddress;
                if (obj.DesignationId != null)
                    data.Designation = DesignationBL.GetById(Convert.ToInt32(obj.DesignationId)).Name;
                if (obj.QualificationId != null)
                    data.Qualification = QualificationBL.GetById(Convert.ToInt32(obj.QualificationId)).Name;
                data.CAddress = obj.CAddress;
                data.PAddress = obj.PAddress;
                if (obj.CDistrictId != null)
                    data.CDistrict = DistrictBL.GetById(Convert.ToInt32(obj.CDistrictId)).Name;
                if (obj.CSuburbId != null)
                    data.CLocationArea = SuburbBL.GetById(Convert.ToInt32(obj.CSuburbId)).Name;
                if (obj.PSuburbId != null)
                    data.PLocationArea = SuburbBL.GetById(Convert.ToInt32(obj.PSuburbId)).Name;
                if (obj.CStateId != null)
                    data.CState = StateBL.GetById(Convert.ToInt32(obj.CStateId)).Name;
                if (obj.CTalukaId != null)
                    data.CTaluka = TalukaBL.GetById(Convert.ToInt32(obj.CTalukaId)).Name;
                return View("_ReviewPartial", data);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            int instituteid = Convert.ToInt32(Usermodel.InstituteId);
            User obj = UsersBL.GetById(Convert.ToInt32(id), instituteid);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                StaffWizardFormModel data = new StaffWizardFormModel();
                data.StaffId = Convert.ToInt32(id);
                if (obj.SalaryType != null)
                {
                    SalaryType salarytype = (SalaryType)System.Enum.Parse(typeof(SalaryType), obj.SalaryType);
                    data.salaryType = salarytype;
                }
                data.TransactionLimit = obj.TransactionLimit.ToString();
                if (obj.PhotoUpload != null)
                {
                    data.PhotoUpload = obj.PhotoUpload;
                    string photoPath = obj.PhotoUpload;
                    string photofilename = null;
                    photofilename = Path.GetFileName(photoPath);
                    data.Photofilename = photofilename;
                }

                data.Aadhar = obj.Aadhar;
                if (obj.DOB != null)
                {
                    DateTime? myDate = obj.DOB;
                    data.DOB = myDate.Value.ToString("MM/dd/yyyy");
                }
                data.Email = obj.Username;
                data.FirstName = obj.FirstName;
                data.Gender = obj.Gender;
                data.LastName = obj.LastName;
                data.MarritalStatus = obj.MarritalStatus;
                data.MiddleName = obj.MiddleName;
                data.PAN = obj.PAN;
                data.AlternateMobile = obj.AlternateMobileNo;

                data.Mobile = obj.MobileNo;

                data.PinCode = obj.CPinCode;
                data.PPinCode = obj.PPinCode;

                if (obj.AccountHolderName != null && obj.BankAccountNo != null)
                {
                    data.HaveBankDetails = "Yes";
                    data.AccountHolderName = obj.AccountHolderName;
                    data.BankAccountNo = obj.BankAccountNo;
                    data.BankBranch = obj.BankBranch;
                    data.BankName = obj.BankName;
                    if (obj.BankProofUpload != null)
                    {
                        data.BankProofUpload = obj.BankProofUpload;
                        string bankProofPath = obj.BankProofUpload;
                        string Bankfilename = null;
                        Bankfilename = Path.GetFileName(bankProofPath);
                        data.BProofFilename = Bankfilename;
                    }
                    data.IFSCCode = obj.IFSCCode;
                }
                else
                {
                    data.HaveBankDetails = "No";
                    data.AccountHolderName = "";
                    data.BankAccountNo = "";
                    data.BankBranch = "";
                    data.BankName = "";
                    data.BankProofUpload = "";
                    data.IFSCCode = "";
                }

                data.CAddress = obj.CAddress;
                data.PAddress = obj.PAddress;
                if (obj.DesignationId != null)
                    data.Designation = DesignationBL.GetById(Convert.ToInt32(obj.DesignationId)).Name;
                if (obj.QualificationId != null)
                    data.Qualification = QualificationBL.GetById(Convert.ToInt32(obj.QualificationId)).Name;
                data.CAddress = obj.CAddress;
                data.PAddress = obj.PAddress;
                if (obj.CDistrictId != null)
                    data.CDistrict = DistrictBL.GetById(Convert.ToInt32(obj.CDistrictId)).Name;
                if (obj.CSuburbId != null)
                    data.CLocationArea = SuburbBL.GetById(Convert.ToInt32(obj.CSuburbId)).Name;
                if (obj.PSuburbId != null)
                    data.PLocationArea = SuburbBL.GetById(Convert.ToInt32(obj.PSuburbId)).Name;
                if (obj.CStateId != null)
                    data.CState = StateBL.GetById(Convert.ToInt32(obj.CStateId)).Name;
                if (obj.CTalukaId != null)
                    data.CTaluka = TalukaBL.GetById(Convert.ToInt32(obj.CTalukaId)).Name;
                return View("_DeletePartial", data);
            }
        }
        [HttpPost]
        public ActionResult Delete(StaffWizardFormModel staffVM)
        {
            if (staffVM.StaffId == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            int instituteid = Convert.ToInt32(Usermodel.InstituteId);
            User obj = UsersBL.GetById(Convert.ToInt32(staffVM.StaffId), instituteid);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                UsersBL.Delete(obj);
                SweetAlert("Delete", "User deleted Successfully", NotificationType.success);
                return RedirectToAction("Index");
            }
        }
    }
}