using ASNTechnosoftWithJWT.Areas.Admin.Models;
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnosoftModel;
using static ASNTechnosoftWithJWT.Areas.Admin.Models.Enum;

namespace ASNTechnosoftWithJWT.Areas.Admin.Controllers
{
    public class InstituteController : BaseController
    {
        // GET: InstituteAdmin/Institute
        public ActionResult Index()

        {
            return View();
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
        [HttpGet]
        public ActionResult UpdateInstitute()
        {
            InstituteModel data = new InstituteModel();
            data.StateNames = PopulateStateNames();
            data.AreaNames = PopulateAreaNames();
            data.DistrictNames = PopulateDistrictNames();
            data.TahsilNames = PopulateTalukaNames();
            User Usermodel = TempData["UserObject"] as User;
            int instituteId = Convert.ToInt32(Usermodel.InstituteId);
            TempData.Keep();
            Institute obj = InstituteBL.GetById(instituteId);
            data.FirstName = obj.FirstName;
            data.MiddleName = obj.MiddleName;
            data.LastName = obj.LastName;
            data.Email = obj.Email;
            data.InstituteName = obj.InstituteName;
            data.Mobile = obj.Mobile;
            data.InstituteId = instituteId;
            return View("UpdateProfile", data);
        }
        [HttpPost]
        public ActionResult UpdateInstitute(InstituteModel data)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            Institute obj = InstituteBL.GetById(Convert.ToInt32(Usermodel.InstituteId));
            //  obj.AADHAR = data.Aadhar;
            //  obj.Address = data.Address;
            //  obj.AlternateMobile = data.AlternateMobile;
            //  obj.BirthDate =Convert.ToDateTime(data.DOB);
            //  obj.CountryId = 1;
            //  obj.DistrictId = data.DistrictId;
            // // obj.DivisionOrZone =;
            //  obj.Email = data.Email;
            //  obj.FirstName = data.FirstName;
            //  obj.Gender = data.Gender.ToString();
            //  obj.InstituteName = data.InstituteName;
            //  obj.LastName = data.LastName;
            //  obj.Location = data.Area;
            //  obj.MiddleName = data.MiddleName;
            //  obj.Mobile = data.Mobile;
            //  obj.PAN = data.PAN;
            //  obj.pincode = data.PinCode;
            ////  obj.RuralOrUrban =;
            //  obj.StateId = data.StateId;
            //  obj.SuburbId = data.AreaId;
            //  obj.TalukaId = data.TahsilId;

            //  if (data.PhotoImageFile != null && data.PhotoImageFile.ContentLength > 0)
            //  {
            //      string FileName = Path.GetFileNameWithoutExtension(data.PhotoImageFile.FileName);
            //      string FileExtension = Path.GetExtension(data.PhotoImageFile.FileName);
            //      FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
            //      string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
            //      data.PhotoImageFile.SaveAs(UploadPath);
            //      obj.Photo = UploadPath.ToString();
            //      obj.PhotoFileName = FileName;
            //  }

            //  if (data.LogoImageFile != null && data.LogoImageFile.ContentLength > 0)
            //  {
            //      string FileName = Path.GetFileNameWithoutExtension(data.LogoImageFile.FileName);
            //      string FileExtension = Path.GetExtension(data.LogoImageFile.FileName);
            //      FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
            //      string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
            //      data.LogoImageFile.SaveAs(UploadPath);
            //      obj.Logo = UploadPath.ToString();
            //      obj.LogoFileName = FileName;
            //  }

            //  if (InstituteBL.Edit(obj) == true)
            //  {

            //      SweetAlert("Done", "Profile details updated Successfully", NotificationType.success);
            //      return RedirectToAction("Index", "Home");
            //  }
            return View();

        }
        [HttpGet]
        public ActionResult PersonalInfo()
        {
            InstituteModel data = new InstituteModel();
            User Usermodel = TempData["UserObject"] as User;
            int instituteId = Convert.ToInt32(Usermodel.InstituteId);
            TempData.Keep();
            Institute Instobj = InstituteBL.GetById(instituteId);

            data.InstituteName = Instobj.InstituteName;
            //data.Mobile = Instobj.Mobile;
            data.InstituteId = instituteId;

            User userobj = UsersBL.GetById(Usermodel.Id, instituteId);
            data.Aadhar = userobj.Aadhar;
            data.MarritalStatus = userobj.MarritalStatus;
            data.Gender = userobj.Gender;
            data.PAN = userobj.PAN;
            data.FirstName = userobj.FirstName;
            data.MiddleName = userobj.MiddleName;
            data.LastName = userobj.LastName;
            data.Email = userobj.Username;
            data.Mobile = userobj.MobileNo;
            if (userobj.PhotoUpload != null)
            {
                data.PhotoUpload = Usermodel.PhotoUpload;
                data.Photofilename = Usermodel.PhotpFIleName;
            }
            if (userobj.DOB != null)
            {
                DateTime? myDate = Usermodel.DOB;
                data.DOB = myDate.Value.ToString("MM/dd/yyyy");
            }

            return View("PersonalInfo", data);
        }
        [HttpPost]
        public ActionResult PersonalInfo(InstituteModel data)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();

            User userobj = UsersBL.GetByUserName_instituteid(Usermodel.Username, Convert.ToInt32(Usermodel.InstituteId));
            userobj.Aadhar = data.Aadhar;
            userobj.DOB = Convert.ToDateTime(data.DOB);
            userobj.Gender = data.Gender.ToString();
            userobj.PAN = data.PAN;

            userobj.MarritalStatus = data.MarritalStatus;
            if (data.PhotoImageFile != null && data.PhotoImageFile.ContentLength > 0)
            {
                string FileName = Path.GetFileNameWithoutExtension(data.PhotoImageFile.FileName);
                string FileExtension = Path.GetExtension(data.PhotoImageFile.FileName);
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
                data.PhotoImageFile.SaveAs(UploadPath);
                userobj.PhotoUpload = UploadPath.ToString();
                userobj.PhotpFIleName = FileName;
            }

            if (UsersBL.Edit(userobj) == true)
            {
                SweetAlert("Done", "Personal details updated Successfully", NotificationType.success);
                return RedirectToAction("PersonalInfo", "Institute");
            }

            return View();

        }
        [HttpGet]
        public ActionResult ContactInfo()
        {
            InstituteModel data = new InstituteModel();
            User Usermodel = TempData["UserObject"] as User;
            int instituteId = Convert.ToInt32(Usermodel.InstituteId);
            TempData.Keep();
            User userobj = UsersBL.GetByUserName_instituteid(Usermodel.Username, Convert.ToInt32(Usermodel.InstituteId));
            data.StateNames = PopulateStateNames();
            int stateId = Convert.ToInt32(userobj.CStateId);
            if (stateId != 0)
            {
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

            int DistrictId = Convert.ToInt32(userobj.CDistrictId);
            if (DistrictId != 0)
            {
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

            int TalukaId = Convert.ToInt32(userobj.CTalukaId);
            if (TalukaId != 0)
            {
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

            Institute obj = InstituteBL.GetById(instituteId);
            //data.FirstName = obj.FirstName;
            //data.MiddleName = obj.MiddleName;
            //data.LastName = obj.LastName;
            //data.Email = obj.Email;
            //data.InstituteName = obj.InstituteName;
            data.Mobile = userobj.MobileNo;
            data.InstituteId = instituteId;
            data.StateId = Convert.ToInt32(userobj.CStateId);
            data.DistrictId = Convert.ToInt32(userobj.CDistrictId);
            data.TahsilId = Convert.ToInt32(userobj.CTalukaId);
            data.AreaId = Convert.ToInt32(userobj.CSuburbId);

            data.PinCode = userobj.CPinCode;
            data.Address = userobj.CAddress;
            data.AlternateMobile = userobj.AlternateMobileNo;
            return View("ContactInfo", data);
        }
        [HttpPost]
        public ActionResult ContactInfo(InstituteModel data)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            User userobj = UsersBL.GetByUserName_instituteid(Usermodel.Username, Convert.ToInt32(Usermodel.InstituteId));
            userobj.CAddress = data.Address;
            userobj.AlternateMobileNo = data.AlternateMobile;
            userobj.CDistrictId = data.DistrictId;
            // userobj.MobileNo = data.Mobile;
            userobj.CPinCode = data.PinCode;
            userobj.CStateId = data.StateId;
            userobj.CSuburbId = data.AreaId;
            userobj.CTalukaId = data.TahsilId;
            if (UsersBL.Edit(userobj) == true)
            {
                SweetAlert("Done", "Contact details updated Successfully", NotificationType.success);
                return RedirectToAction("ContactInfo", "Institute");
            }
            return View();
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View("ChangePassword");
        }
        [HttpPost]
        public ActionResult ChangePassword(InstituteModel data)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            Institute obj = InstituteBL.GetById(Convert.ToInt32(Usermodel.InstituteId));
            if (Usermodel.Username == obj.Email)
            {
                obj.Password = data.NewPassword;
                if (InstituteBL.Edit(obj) == true)
                {
                    User userobj = UsersBL.GetByUserName_instituteid(Usermodel.Username, Convert.ToInt32(Usermodel.InstituteId));
                    userobj.Password = data.NewPassword;
                    UsersBL.Edit(userobj);
                    EmailBL.SendResetPasswordMail(userobj);
                    SweetAlert("Done", "Password changed Successfully", NotificationType.success);
                    return
                        RedirectToAction("LogOut", "Account", new { area = "" });//RedirectToAction("ChangePassword", "Institute");
                }
            }
            else
            {
                User userobj = UsersBL.GetByUserName_instituteid(Usermodel.Username, Convert.ToInt32(Usermodel.InstituteId));
                userobj.Password = data.NewPassword;
                UsersBL.Edit(userobj);
                EmailBL.SendResetPasswordMail(userobj);
                SweetAlert("Done", "Password changed Successfully", NotificationType.success);
                return
                    RedirectToAction("LogOut", "Account", new { area = "" });//RedirectToAction("ChangePassword", "Institute");
            }

            return View();
        }
        [HttpGet]
        public ActionResult EmailSetting()
        {
            InstituteModel data = new InstituteModel();

            User Usermodel = TempData["UserObject"] as User;
            int instituteId = Convert.ToInt32(Usermodel.InstituteId);
            TempData.Keep();
            Institute obj = InstituteBL.GetById(instituteId);
            data.FirstName = obj.FirstName;
            data.MiddleName = obj.MiddleName;
            data.LastName = obj.LastName;
            data.Email = obj.Email;
            data.Mobile = obj.Mobile;
            data.InstituteId = instituteId;
            data.Address = Usermodel.CAddress;
            if (Usermodel.PhotoUpload != null)
            {
                data.PhotoUpload = Usermodel.PhotoUpload;
                data.Photofilename = Usermodel.PhotpFIleName;
            }
            return View("EmailSetting", data);
        }
        [HttpPost]
        public ActionResult ExistOrNot(string password)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            bool isValid = false;
            if (Usermodel.Password == password)
            {
                isValid = true;
            }
            return Json(isValid);
        }

        public ActionResult AsideMenuOfProfile()
        {
            InstituteModel data = new InstituteModel();

            User Usermodel = TempData["UserObject"] as User;
            int instituteId = Convert.ToInt32(Usermodel.InstituteId);
            TempData.Keep();
            Institute obj = InstituteBL.GetById(instituteId);

            data.InstituteId = instituteId;
            data.Address = Usermodel.CAddress;
            data.FirstName = Usermodel.FirstName;
            data.MiddleName = Usermodel.MiddleName;
            data.LastName = Usermodel.LastName;
            data.Email = Usermodel.Username;
            data.Mobile = Usermodel.MobileNo;
            if (Usermodel.PhotoUpload != null)
            {
                data.PhotoUpload = Usermodel.PhotoUpload;
                data.Photofilename = Usermodel.PhotpFIleName;
            }
            if (Usermodel.RoleId == 1)
            {
                ViewBag.InstituteInfo = true;
            }
            else
            {
                ViewBag.InstituteInfo = false;
            }
            return PartialView(data);
        }

        [HttpGet]
        public ActionResult InstituteInfo()
        {
            InstituteModel data = new InstituteModel();
            User Usermodel = TempData["UserObject"] as User;
            int instituteId = Convert.ToInt32(Usermodel.InstituteId);
            TempData.Keep();
            Institute obj = InstituteBL.GetById(instituteId);
            data.StateNames = PopulateStateNames();

            int stateId = Convert.ToInt32(obj.StateId);
            if (stateId != 0)
            {
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

            int DistrictId = Convert.ToInt32(obj.DistrictId);
            if (DistrictId != 0)
            {
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

            int TalukaId = Convert.ToInt32(obj.TalukaId);
            if (TalukaId != 0)
            {
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

            data.InstituteName = obj.InstituteName;
            data.InstituteId = instituteId;
            data.StateId = Convert.ToInt32(obj.StateId);
            data.DistrictId = Convert.ToInt32(obj.DistrictId);
            data.TahsilId = Convert.ToInt32(obj.TalukaId);
            data.AreaId = Convert.ToInt32(obj.SubUrbanId);
            data.PinCode = obj.PinCode;
            data.Address = obj.Address;
            if (obj.Logo != null)
            {
                data.LogoUpload = obj.Logo;
                data.Logofilename = obj.LogoFileName;
            }

            return View("InstituteDetails", data);
        }

        public ActionResult InstituteInfo(InstituteModel data)
        {
            int instituteId = data.InstituteId;
            Institute obj = InstituteBL.GetById(instituteId);
            obj.Address = data.Address;
            obj.StateId = data.StateId;
            obj.DistrictId = data.DistrictId;
            obj.TalukaId = data.TahsilId;
            obj.SubUrbanId = data.AreaId;
            obj.PinCode = data.PinCode;
            if (data.LogoImageFile != null && data.LogoImageFile.ContentLength > 0)
            {
                string FileName = Path.GetFileNameWithoutExtension(data.LogoImageFile.FileName);
                string FileExtension = Path.GetExtension(data.LogoImageFile.FileName);
                FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                string UploadPath = Path.Combine(Server.MapPath("~/UserImages"), FileName);
                data.LogoImageFile.SaveAs(UploadPath);
                obj.Logo = UploadPath.ToString();
                obj.LogoFileName = FileName;
            }
            if (InstituteBL.Edit(obj) == true)
            {
                SweetAlert("Done", "Institute details updated Successfully", NotificationType.success);
                return RedirectToAction("InstituteInfo", "Institute");
            }
            return View();
        }
    }
}