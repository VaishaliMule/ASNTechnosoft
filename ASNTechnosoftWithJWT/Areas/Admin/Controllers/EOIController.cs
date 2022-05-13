using ASNTechnosoftWithJWT.Areas.Admin.Models;
using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnosoftModel;
using static ASNTechnosoftWithJWT.Areas.Admin.Models.Enum;

namespace ASNTechnosoftWithJWT.Areas.Admin.Controllers
{
    public class EOIController : BaseController
    {
        // GET: InstituteAdmin/EOI
        public ActionResult Index()
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            List<AllEOIDetailsByInstituteid_Result> listItems = PurchasedServicesBL.GetAllEOIByInstituteId(Convert.ToInt32(Usermodel.InstituteId));
            return View(listItems);
        }

        private static List<SelectListItem> PopulateServices()
        {
            List<SelectListItem> ServicesNames = new List<SelectListItem>();
            List<SubscriptionService> durations = SubscriptionServicesBL.GetAll();
            durations.ForEach(x =>
            {
                ServicesNames.Add(new SelectListItem { Text = x.ServiceName.ToString(), Value = x.Id.ToString() });
            });
            return ServicesNames;
        }

        [HttpGet]
        public ActionResult Create()
        {
            EOIModel dataModel = new EOIModel();
            dataModel.ServicesNames = PopulateServices();
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            dataModel.InstituteId = Convert.ToInt32(Usermodel.InstituteId);
            dataModel.walletAmount = EWalletBL.GetByInstituteId(Convert.ToInt32(Usermodel.InstituteId)).AvailableAmount.ToString();
            return View("_CreateEOI", dataModel);
        }

        [HttpPost]
        public ActionResult Create(EOIModel data)
        {
            PurchasedSerivce obj = new PurchasedSerivce();
            obj.Amount = Convert.ToDecimal(data.Amount);
            obj.ApproveDate = DateTime.Now;
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            obj.InstituteId = Usermodel.InstituteId;
            obj.IsApprove = true;
            obj.IsDeleted = false;
            obj.PurchaseDate = DateTime.Now;
            obj.Quantity = Convert.ToInt32(data.Quantity);
            obj.Remark = data.Remark;
            obj.RenewalDate = DateTime.Today.AddYears(1);
            obj.SubscriptionServiceId = data.SubscriptionServiceId;

            if (PurchasedServicesBL.Add(obj) == true)
            {
                EWallet wallet = EWalletBL.GetByInstituteId(Usermodel.InstituteId);
                if (wallet != null)
                {
                    wallet.AvailableAmount -= obj.Amount;
                    EWalletBL.Edit(wallet);

                    EWalletTransation txn = new EWalletTransation();
                    txn.AdminRemark = "Service Subscription.";
                    txn.Amount = obj.Amount;
                    txn.EWalletId = wallet.Id;
                    txn.IsApproved = true;
                    txn.IsDeleted = false;
                    txn.PaymentType = "Wallet";
                    txn.TransactionDate = DateTime.Now;
                    txn.TransactionType = "Debit";
                    EWalletTransactionBL.Add(txn);
                    Institute instituteObj = InstituteBL.GetById(Usermodel.InstituteId);
                    string smsText = "Dear " + instituteObj.InstituteName + Environment.NewLine + "Thank you, " + SubscriptionServicesBL.GetById(data.SubscriptionServiceId).ServiceName + " EOI Successfully Submitted!" + Environment.NewLine + "ASN Technosoft" + Environment.NewLine + "7083528282";
                    SMSBL.SendSMS(Usermodel.MobileNo, smsText);
                }
                SweetAlert("Done", "Service added Successfully", NotificationType.success);
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        [HttpPost]
        public ActionResult ExistOrNot(string serviceid)
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            PurchasedSerivce service = PurchasedServicesBL.GetBySubscriptionId(Convert.ToInt32(serviceid), Usermodel.InstituteId);
            return Json(service.Id, JsonRequestBehavior.AllowGet);
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
            PurchasedSerivce obj = PurchasedServicesBL.GetById(Convert.ToInt32(id), Usermodel.InstituteId);
            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                EOIModel data = new EOIModel();
                data.PurchasedId = Convert.ToInt32(id);
                data.InstituteName = InstituteBL.GetById(Usermodel.InstituteId).InstituteName;
                data.ServiceName = SubscriptionServicesBL.GetById(obj.SubscriptionServiceId).ServiceName;
                data.SubcribedDate = obj.PurchaseDate.ToShortDateString();
                data.Quantity = obj.Quantity.ToString();
                data.Amount = obj.Amount.ToString();
                data.RenewalDate = obj.RenewalDate.ToShortDateString();
                return View("_ViewEOI", data);
            }
        }

        [HttpPost]
        public JsonResult GetServiceAmount(string serviceid)
        {
            SubscriptionService service = SubscriptionServicesBL.GetById(Convert.ToInt32(serviceid));
            if (service != null)
            {
                return Json(new { success = true, Amount = service.ServiceAmount, Qty = service.Quantity, descptn = service.Description });
            }
            return Json(new { success = false });
        }
    }
}