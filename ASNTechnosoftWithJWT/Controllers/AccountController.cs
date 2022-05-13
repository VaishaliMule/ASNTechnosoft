using ASNTechnosoftWithJWT.Models;
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnosoftModel;
using static ASNTechnosoftWithJWT.Areas.Admin.Models.Enum;

namespace ASNTechnosoftWithJWT.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult PersonalDetails()
        {
            return View("PersonalDetails");
        }

        private Institute GetUser()
        {
            if (Session["user"] == null)
            {
                Session["user"] = new Institute();
            }
            return (Institute)Session["user"];
        }
        private void RemoveUser()
        {
            Session.Remove("user");
        }

        [HttpPost]
        public ActionResult PersonalDetails(InstituteRegistration model, string nextBtn)
        {
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    Institute obj = GetUser();
                    obj.FirstName = model.FirstName;
                    obj.Email = model.Email;
                    obj.InstituteName = model.InstituteName;
                    obj.LastName = model.LastName;
                    obj.MiddleName = model.MiddleName;
                    obj.Mobile = model.Mobile;

                    //string strNewPassword = GeneratePassword().ToString();
                    //obj.Password = strNewPassword;

                    string smsOtp = SendSMSToUSerForOTPVerification(model.Mobile);
                    obj.SMSOtp = smsOtp.ToString();
                    string emailOtp = SendEmailToUserForVerification(model.Email);
                    obj.EmailOTP = emailOtp.ToString();
                    return RedirectToAction("OTPVerification");
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
        public string SendSMSToUSerForOTPVerification(string number)
        {
            Random r = new Random();
            string OTP = "2511";  // r.Next(1000, 9999).ToString();

            ////Send message
            //string userName = "ASNTECHNOSOFT";
            //string password = "Password@2017";
            //string senderId = "ASNTEC";
            //string msgType = "TXT";
            //// string number = "9876543210";
            //string smsText = "Your OTP code for Mobile verification is : " + OTP + Environment.NewLine + "Regards," + Environment.NewLine + "ASN Technosoft" + Environment.NewLine + "7083528282";
            //string uri = "http://203.129.203.243/blank/sms/user/urlsms.php?username={0}&pass={1}&senderid={2}&dest_mobileno={3}&message={4}&msgtype={5}&response=Y";
            //string url = string.Format(uri, userName, password, senderId, number, smsText, msgType);

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //StreamReader sr = new StreamReader(resp.GetResponseStream());
            //string results = sr.ReadToEnd();
            //sr.Close();


            return OTP;
        }

        public string SendEmailToUserForVerification(string emailId)
        {
            Random r = new Random();
            string OTP = r.Next(1000, 9999).ToString();
            if (emailId != null)
            {
                string FROM_EMAIL = "asntechnosoft@gmail.com";
                string PASSWORD = "7066922020";
                string DISPLAY_NAME = "ASN Technosoft";

                MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(emailId);
                mail.From = new MailAddress(FROM_EMAIL, DISPLAY_NAME, System.Text.Encoding.UTF8);
                mail.Subject = "Email Verification!";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "<br/> Your Email OTP is: " + OTP;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(FROM_EMAIL, PASSWORD);
                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }
            return OTP;
        }

        [HttpGet]
        public ActionResult OTPVerification(InstituteRegistration model)
        {
            return View("OTPVerification");
        }

        [HttpPost]
        public ActionResult OTPVerification(InstituteRegistration model, string prevBtn, string nextBtn)
        {
            Institute obj = GetUser();
            if (prevBtn != null)
            {
                model.InstituteName = obj.InstituteName;
                model.FirstName = obj.FirstName;
                model.MiddleName = obj.MiddleName;
                model.LastName = obj.LastName;
                model.Email = obj.Email;
                model.Mobile = obj.Mobile;
                model.GeneratedEmailOTP = obj.EmailOTP;
                model.GeneratedMobileOTP = obj.SMSOtp;
                return View("PersonalDetails", model);
            }
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {

                    if (obj.EmailOTP == model.EmailOTP && obj.SMSOtp == model.SMSOTP)
                    {
                        obj.EmailOTP = model.EmailOTP;
                        obj.SMSOtp = model.SMSOTP;
                        model.InstituteName = obj.InstituteName;
                        model.FirstName = obj.FirstName;
                        model.MiddleName = obj.MiddleName;
                        model.LastName = obj.LastName;
                        model.Email = obj.Email;
                        model.Mobile = obj.Mobile;
                        TempData["Review"] = model;
                        //var message = "Email amd Mobile number Verified successfully ";
                        //ViewBag.Message = message;
                        SweetAlert("Done", "Email amd Mobile number Verified successfully.!", NotificationType.success);
                        return RedirectToAction("PreviewAndSubmit");
                    }
                    else
                    {
                        //var message = "Email and mobile number verification failed for " + obj.Email +" And " +obj.Mobile;
                        //ViewBag.Message = message;
                        SweetAlert("Error", "Email and mobile number verification failed for " + obj.Email + " And " + obj.Mobile, NotificationType.error);
                        return View("OTPVerification");
                    }

                }
            }
            return View();
        }

        public ActionResult PreviewAndSubmit(string prevBtn, string nextBtn)
        {
            Institute obj = GetUser();
            InstituteRegistration model = TempData["Review"] as InstituteRegistration;
            if (nextBtn != null)
            {
                if (ModelState.IsValid)
                {
                    obj.EmailVerified = true;
                    obj.CenterType = "Parent";
                    obj.CenterTypeId = null;
                    obj.RegistrationDate = DateTime.Now;
                    obj.IsActive = true;
                    obj.IsApproved = true;
                    obj.IsDeleted = false;
                    obj.MobileVerified = true;
                    string strNewPassword = GeneratePassword().ToString();
                    obj.Password = strNewPassword;
                    if (InstituteBL.Add(obj) == true)
                    {
                        User userObj = new User();
                        userObj.InstituteId = obj.Id;
                        userObj.IsActive = true;
                        userObj.Password = obj.Password;
                        userObj.RoleId = 1;
                        userObj.Username = obj.Email;
                        userObj.FirstName = obj.FirstName;
                        userObj.MiddleName = obj.MiddleName;
                        userObj.LastName = obj.LastName;
                        userObj.MobileNo = obj.Mobile;
                        userObj.IsDeleted = false;
                        userObj.DesignationId = 2;
                        UsersBL.Add(userObj);
                        string smsText = "Thank you for Registration!" + Environment.NewLine + "For Login Details visit your Email id." + Environment.NewLine + "Regards," + Environment.NewLine + "ASN Technosoft" + Environment.NewLine + "7083528282";
                        // SMSBL.SendSMS(obj.Mobile, smsText);
                        EmailBL.SendRegistrationEmail(userObj);
                        RemoveUser();
                        return RedirectToAction("Success");
                    }


                }
            }

            if (prevBtn != null)
            {
                InstituteRegistration Viewmodel = new InstituteRegistration();
                Viewmodel.EmailOTP = obj.EmailOTP;
                Viewmodel.SMSOTP = obj.SMSOtp;
                return View("OTPVerification", Viewmodel);
            }
            else
            {
                return View(model);
            }



        }

        [HttpPost]
        public ActionResult ExistOrNot(string Email)
        {
            Institute data = InstituteBL.GetByEmailId(Email);
            return Json(data.Id, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult login(string returnUrl)
        {
            UserLogin userModel = new UserLogin();
            try
            {
                // Verification.    
                if (this.Request.IsAuthenticated)
                {
                    // Info.    
                    return RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info.    
            // return this.View();
            // userModel.EmailId = "vaishalimule2011@gmail.com";

            return this.View(userModel);
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserLogin UserModel)
        {
            try
            {
                bool isAuthenticated = false;

                //if(!ModelState.IsValid)
                //{
                //    SweetAlert("Error", "Authentication errror.", NotificationType.error);
                //    return View(UserModel);
                //}
                // Logger.Debug("Log before going to database");

                #region DB Check and Authentication Cookie
                User user = UsersBL.GetByUserNamePassword(UserModel.EmailId, UserModel.Password);
                //  Logger.Error("After database genarated log");
                if (user != null)
                {
                    if (user.Password == UserModel.Password)
                    {
                        if (user.IsActive == false)
                        {
                            //Access error
                            isAuthenticated = false;
                            // ModelState.AddModelError("", "Inactive User.Invalid login attempt.");
                            // TempData["message"] = "Error Occured. User status is not active. Please contact administrator.";
                            SweetAlert("Info", "Error Occured. User status is not active. Please contact administrator.", NotificationType.warning);
                            return View(UserModel);
                        }
                        else
                        {
                            isAuthenticated = true;
                            #region Success   : set authentication cookie
                            FormsAuthentication.SetAuthCookie(UserModel.EmailId, false);
                            var authTicket = new FormsAuthenticationTicket(1, UserModel.EmailId, DateTime.Now, DateTime.Now.AddMinutes(720), false, user.RoleId.ToString());
                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            HttpContext.Response.Cookies.Add(authCookie);


                            TempData["RoleId"] = user.RoleId;
                            TempData["UserObject"] = user;
                            Session["UserId"] = user.Id;

                            #endregion
                        }
                    }
                    else
                    {
                        //Access error
                        isAuthenticated = false;
                        //ModelState.AddModelError("", "Invalid login attempt.");
                        //TempData["message"] = "Invalid login attempt.";

                        SweetAlert("Error", "Invalid login attempt.", NotificationType.error);
                        return View(UserModel);
                    }

                }
                else
                {
                    isAuthenticated = false;
                    ModelState.AddModelError("", "Invalid login attempt.");
                }

                if (isAuthenticated)
                {
                    switch (RoleBL.GetById(user.RoleId).Role1)
                    {
                        case "InstituteAdmin":
                            {
                                return RedirectToAction("Index", "EOI", new { area = "Admin" });
                                break;
                            }
                        case "ASNAdmin":
                            {
                                return RedirectToAction("Index", "Home", new { areas = "Admin" });
                                break;
                            }
                        case "Student":
                            {
                                return RedirectToAction("Index", "Home", new { areas = "Student" });
                                break;
                            }
                        case "Faculty":
                            {
                                return RedirectToAction("Index", "Home", new { areas = "InstituteAdmin" });
                                break;
                            }
                        default:
                            {
                                ModelState.AddModelError("", "Invalid login attempt.");
                                TempData["message"] = "Invalid login attempt.";
                                return View(UserModel);
                                break;
                            }
                    }

                }
                else
                {
                    // ModelState.AddModelError("", "Invalid login attempt.");
                    //TempData["message"] = "Invalid username or password.";
                    SweetAlert("Error", "Invalid username or password.", NotificationType.error);
                    return View(UserModel);
                }
                #endregion

            }
            catch (Exception ex)
            {
                //// Info    
                Console.Write(ex.InnerException.Message + ex.StackTrace);
                //  Logger.Error(ex, "Hi I am NLog Error Level");


            }

            return View(UserModel);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                // Verification.    
                if (Url.IsLocalUrl(returnUrl))
                {
                    // Info.    
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                // Info    
                throw ex;
            }
            // Info.    
            return this.RedirectToAction("Index", "Account");
        }


    }
}