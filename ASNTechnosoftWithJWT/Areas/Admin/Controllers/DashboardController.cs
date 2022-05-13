
using ASNTechnosoftWithJWT.Areas.Admin.Models;
using BussinessLayer;
using Paytm.Checksum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TechnosoftModel;
using static ASNTechnosoftWithJWT.Areas.Admin.Models.Enum;

namespace ASNTechnosoftWithJWT.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        //// GET: Admin/Dashboard
        //public ActionResult Index()
        //{
        //    return View();
        //}


        public ActionResult Index()
        {
            User model = TempData["UserObject"] as User;
            TempData.Keep();
            Institute instituteObj = InstituteBL.GetById(model.InstituteId);
            //if (instituteObj.IsPayment == true)
            //{
            //    ViewBag.AdmissionCount = AdmissionBL.GetAll(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.PendingEnquiryCount = EnquiryBL.GetAllPendingEnquiries(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.OverdueEnquiryCount = EnquiryBL.GetAllOverdueEnquiries(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.TodaysEnquiryCount = EnquiryBL.GetAllTodaysFollowupEnquiries(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.PendingFeesCount = FeesFollowupHistoryBL.GetAllPendingFees(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.OverdueFeesCount = FeesFollowupHistoryBL.GetAllOverdueFees(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.TodaysFeesCount = FeesFollowupHistoryBL.GetAllTodaysFollowupFees(Convert.ToInt32(model.InstituteId)).Count();
            //    ViewBag.BirthdayCount = StudentBL.GetAllTodaysBirthday(Convert.ToInt32(model.InstituteId)).Count();
            //    List<GetStudentListForAdmissionDetailsByInstituteId_Result> allStudents = AdmissionBL.GetStudentAdmissionByInstituteId(Convert.ToInt32(model.InstituteId));
            //    ViewBag.UploadPhotoANdSign = allStudents.Where(a => a.Photo == null || a.Signature == null).Count();

            //    List<DataPoint> dataPoints = new List<DataPoint>();
            //    List<CoursewiseStudentCount_PieChart_Result> results = CourseBL.GetAllCoursesStudentCount(model.InstituteId, DateTime.Now.Year);
            //    foreach (var item in results)
            //    {
            //        dataPoints.Add(new DataPoint(item.COURSENAME, Convert.ToDouble(item.STUDENTCOUNT)));
            //    }
            //    ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            //    EWallet wallet = EWalletBL.GetByInstituteId(model.InstituteId);
            //    if (wallet != null)
            //    {
            //        ViewBag.WalletBalence = wallet.AvailableAmount;
            //    }
            //    else
            //    {
            //        ViewBag.WalletBalence = 0;
            //    }


            //    ViewBag.DataPoint = null;
            //    logger.Info("Hello You have visited the Index view" + Environment.NewLine + DateTime.Now);
            //    return View();
            //}
            //else
            //{
            //    PaymentInitiateModel data = new PaymentInitiateModel();
            //    data.InstituteId = model.InstituteId;
            //    data.name = model.FirstName + " " + model.MiddleName + " " + model.LastName;
            //    data.email = model.Username;
            //    data.contactNumber = model.MobileNo;
            //    data.amount = 5000;
            //    return View("MakePayment", data);
            //}
            return View();
        }

        [HttpPost]
        public ActionResult CreatePayment(PaymentInitiateModel requestData)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            EWallet ewalletObj = EWalletBL.GetByInstituteId(Usermodel.InstituteId);
            int walletId = 0;
            if (ewalletObj == null)
            {
                EWallet newwallet = new EWallet();
                newwallet.AvailableAmount = 0;
                newwallet.InstituteId = Usermodel.InstituteId;
                newwallet.IsActive = false;
                EWalletBL.Add(newwallet);
                walletId = newwallet.Id;
            }
            else
            {
                // ewalletObj.AvailableAmount += Convert.ToDecimal(_requestData.amount);
                // EWalletBL.Edit(ewalletObj);
                walletId = ewalletObj.Id;
            }
            EWalletTransation transaction = new EWalletTransation();
            transaction.AdminRemark = "Online Payment by Paytm Gateway";
            transaction.Amount = requestData.amount;
            transaction.EWalletId = walletId;
            transaction.IsApproved = false;
            transaction.IsDeleted = false;
            transaction.PaymentType = "Online";
            transaction.TransactionDate = DateTime.Now;
            transaction.TransactionType = "Credit";
            EWalletTransactionBL.Add(transaction);
            // Generate random receipt number for order

            string transactionId = transaction.Id.ToString();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Key.merchantId);
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", requestData.email);
            parameters.Add("MOBILE_NO", requestData.contactNumber);
            parameters.Add("CUST_ID", requestData.InstituteId.ToString());
            parameters.Add("ORDER_ID", "ORDER" + transactionId);
            parameters.Add("TXN_AMOUNT", requestData.amount.ToString());
            parameters.Add("CALLBACK_URL", "http://ims.asntechnosoft.com/Account/PaytmResponse");

            //   parameters.Add("CALLBACK_URL", "https://localhost:44301/Account/PaytmResponse");

            string checksum = CheckSum.GenerateCheckSum(Key.merchantKey, parameters);

            //   string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

            string paytmURL = "https://securegw.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;
            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";

            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }

            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;

            return View("PaytmPaymentPage");
        }
        [HttpPost]
        public ActionResult PaytmResponse(PaytmResponse response)
        {
            string value = response.ORDERID;
            int startIndex = 5;
            string substring = value.Substring(startIndex, value.Length - 5);
            Console.WriteLine(substring);
            EWalletTransation walltxn = EWalletTransactionBL.GetById(Convert.ToInt32(substring));
            EWallet wallet = EWalletBL.GetById(walltxn.EWalletId);
            Institute instObj = InstituteBL.GetById(wallet.InstituteId);
            PaytmResponse paytmResponse = new PaytmResponse();
            paytmResponse.ORDERID = response.ORDERID;
            paytmResponse.BANKNAME = response.BANKNAME;
            paytmResponse.BANKTXNID = response.BANKTXNID;
            paytmResponse.CHECKSUMHASH = response.CHECKSUMHASH;
            paytmResponse.CURRENCY = response.CURRENCY;
            paytmResponse.GATEWAYNAME = response.GATEWAYNAME;
            paytmResponse.InstituteId = wallet.InstituteId;
            paytmResponse.InstituteName = instObj.InstituteName;
            paytmResponse.MID = response.MID;
            paytmResponse.PAYMENTMODE = response.PAYMENTMODE;
            paytmResponse.RESPCODE = response.RESPCODE;
            paytmResponse.RESPMSG = response.RESPMSG;
            paytmResponse.STATUS = response.STATUS;
            paytmResponse.TXNAMOUNT = response.TXNAMOUNT;
            paytmResponse.TXNDATE = response.TXNDATE;
            paytmResponse.TXNID = response.TXNID;


            //Save Data in Database
            PaytmPaymentResponse dbResponse = new PaytmPaymentResponse();
            dbResponse.ORDERID = response.ORDERID;
            dbResponse.BANKNAME = response.BANKNAME;
            dbResponse.BANKTXNID = response.BANKTXNID;
            dbResponse.CHECKSUMHASH = response.CHECKSUMHASH;
            dbResponse.CURRENCY = response.CURRENCY;
            dbResponse.GATEWAYNAME = response.GATEWAYNAME;
            dbResponse.EWalletTransactionId = walltxn.Id;
            dbResponse.MID = response.MID;
            dbResponse.PAYMENTMODE = response.PAYMENTMODE;
            dbResponse.RESPCODE = response.RESPCODE;
            dbResponse.RESPMSG = response.RESPMSG;
            dbResponse.STATUS = response.STATUS;
            dbResponse.TXNAMOUNT = response.TXNAMOUNT;
            dbResponse.TXNDATE = response.TXNDATE;
            dbResponse.TXNID = response.TXNID;

            PaytmPaymentResponse paytmPaymentResponse = PaytmPaymentResponseBL.GetById(walltxn.Id);
            if (paytmPaymentResponse == null)
            {
                PaytmPaymentResponseBL.Add(dbResponse);
                if (response.RESPCODE == "01")
                {
                    if (wallet.Id != 0)
                    {
                        // wallet.AvailableAmount += Convert.ToDecimal(response.TXNAMOUNT);
                        wallet.IsActive = true;
                        EWalletBL.Edit(wallet);
                    }
                    if (walltxn.Id != 0)
                    {
                        walltxn.Amount = Convert.ToDecimal(response.TXNAMOUNT);
                        walltxn.EWalletId = wallet.Id;
                        walltxn.IsApproved = true;
                        walltxn.IsDeleted = false;
                        walltxn.TransactionDate = DateTime.Now;
                        EWalletTransactionBL.Edit(walltxn);
                    }



                    PurchasedSerivce obj = new PurchasedSerivce();
                    obj.Amount = Convert.ToDecimal(5000);
                    obj.ApproveDate = DateTime.Now;

                    obj.InstituteId = wallet.InstituteId;
                    obj.IsApprove = true;
                    obj.IsDeleted = false;
                    obj.PurchaseDate = DateTime.Now;
                    obj.Quantity = 1;
                    obj.Remark = "IMS Service Subscribed";
                    obj.RenewalDate = DateTime.Today.AddYears(1);
                    obj.SubscriptionServiceId = 1;

                    if (PurchasedServicesBL.Add(obj) == true)
                    {
                        //  EWallet wallet = EWalletBL.GetByInstituteId(Usermodel.InstituteId);
                        if (wallet != null)
                        {
                            //wallet.AvailableAmount -= obj.Amount;
                            //EWalletBL.Edit(wallet);

                            EWalletTransation txn = new EWalletTransation();
                            txn.AdminRemark = "IMS Service Subscription.";
                            txn.Amount = obj.Amount;
                            txn.EWalletId = wallet.Id;
                            txn.IsApproved = true;
                            txn.IsDeleted = false;
                            txn.PaymentType = "Online";
                            txn.TransactionDate = DateTime.Now;
                            txn.TransactionType = "Debit";
                            EWalletTransactionBL.Add(txn);
                            //  Institute instituteObj = InstituteBL.GetById(Usermodel.InstituteId);


                            instObj.IsPayment = true;
                            InstituteBL.Edit(instObj);



                            string smsText = "Thank you for Payment!" + Environment.NewLine + "For any query contact to ASN Technosoft" + Environment.NewLine + "Regards," + Environment.NewLine + "ASN Technosoft" + Environment.NewLine + "7083528282";
                            SMSBL.SendSMS(instObj.Mobile, smsText);
                            // User userObj = UsersBL.GetByUserName(instObj.Email,instObj.Id);
                            //userObj.IsActive = true;
                            //UsersBL.Edit(userObj);
                            //  EmailBL.SendRegistrationEmail(userObj);



                            //string smsText = "Dear " + instObj.InstituteName + Environment.NewLine + "Thank you, " + SubscriptionServicesBL.GetById(1).ServiceName + " EOI Successfully Submitted!" + Environment.NewLine + "ASN Technosoft" + Environment.NewLine + "7083528282";
                            //SMSBL.SendSMS(instObj.Mobile, smsText);
                        }
                        // SweetAlert("Done", "Service added Successfully", NotificationType.success);
                        // return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return View("paytmResponse", paytmResponse);
        }

        public ActionResult PartialUserPanel()
        {
            User model = TempData["UserObject"] as User;
            TempData.Keep();
            if (model.RoleId == 1)
            {
                ViewBag.Designation = RoleBL.GetById(model.RoleId).Role1;
            }
            else
            {
                if (model.DesignationId != 0)
                {
                    ViewBag.Designation = DesignationBL.GetById(Convert.ToInt32(model.DesignationId)).Name;
                }

            }

            return PartialView(model);
        }
        public ActionResult PartialMenuContent()
        {
            User user = TempData["UserObject"] as User;
            TempData.Keep();
            List<PurchasedSerivce> menuaccess = PurchasedServicesBL.GetByInstituteId(user.InstituteId);
            //here if purchased item found then operation to do....
            if (menuaccess.Count() != 0)
            {
                ViewBag.MenuContent = true;
            }
            else
            {
                ViewBag.MenuContent = false;
            }


            return PartialView(user);
        }

        public ActionResult PartialTopbar()
        {
            User model = TempData["UserObject"] as User;
            TempData.Keep();
            if (model.RoleId == 1)
            {
                ViewBag.Designation = RoleBL.GetById(model.RoleId).Role1;
            }
            else
            {
                if (model.DesignationId != 0)
                {
                    ViewBag.Designation = DesignationBL.GetById(Convert.ToInt32(model.DesignationId)).Name;
                }

            }

            return PartialView(model);
        }

        [HttpPost]
        public static List<DataPoint> GetChartData()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("MSCIT", 25));
            dataPoints.Add(new DataPoint("Klic Diploma", 13));
            dataPoints.Add(new DataPoint("Klic English", 8));
            dataPoints.Add(new DataPoint("Tally", 7));
            dataPoints.Add(new DataPoint("Autocard", 6.8));
            dataPoints.Add(new DataPoint("Others", 40.2));

            return dataPoints;

        }


        [HttpPost]
        public ActionResult IMSLogin()
        {
            try
            {
                bool isAuthenticated = false;
                #region DB Check and Authentication Cookie
                User model = TempData["UserObject"] as User;

                User user = UsersBL.GetByUserNamePassword(model.Username, model.Password);
                //  Logger.Error("After database genarated log");
                if (user != null)
                {
                        if (user.IsActive == false)
                        {
                            //Access error
                            isAuthenticated = false;
                            // ModelState.AddModelError("", "Inactive User.Invalid login attempt.");
                            // TempData["message"] = "Error Occured. User status is not active. Please contact administrator.";
                            SweetAlert("Info", "Error Occured. User status is not active. Please contact administrator.", NotificationType.warning);
                            return View();
                        }
                        else
                        {
                            isAuthenticated = true;
                            #region Success   : set authentication cookie
                            FormsAuthentication.SetAuthCookie(model.Username, false);
                            var authTicket = new FormsAuthenticationTicket(1, model.Username, DateTime.Now, DateTime.Now.AddMinutes(720), false, user.RoleId.ToString());
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
                    isAuthenticated = false;
                    ModelState.AddModelError("", "Invalid login attempt.");
                }

                if (isAuthenticated)
                {
                    return Redirect("https://localhost:44349/IMS/Home/");
                    //switch (RoleBL.GetById(user.RoleId).Role1)
                    //{
                    //    case "InstituteAdmin":
                    //        {
                    //            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    //            break;
                    //        }
                    //    case "ASNAdmin":
                    //        {
                    //            return RedirectToAction("Index", "Home", new { area = "Admin" });
                    //            break;
                    //        }
                    //    case "Student":
                    //        {
                    //            return RedirectToAction("Index", "Home", new { area = "Student" });
                    //            break;
                    //        }
                    //    case "Faculty":
                    //        {
                    //            return RedirectToAction("Index", "Home", new { area = "InstituteAdmin" });
                    //            break;
                    //        }
                    //    default:
                    //        {
                    //            ModelState.AddModelError("", "Invalid login attempt.");
                    //            TempData["message"] = "Invalid login attempt.";
                    //            return View(UserModel);
                    //            break;
                    //        }
                    //}

                }
                else
                {
                    // ModelState.AddModelError("", "Invalid login attempt.");
                    //TempData["message"] = "Invalid username or password.";
                    SweetAlert("Error", "Invalid username or password.", NotificationType.error);
                    return View(model);
                }
                #endregion

            }
            catch (Exception ex)
            {
                //// Info    
                Console.Write(ex.InnerException.Message + ex.StackTrace);
                //  Logger.Error(ex, "Hi I am NLog Error Level");


            }

            return View();
        }
    }
}