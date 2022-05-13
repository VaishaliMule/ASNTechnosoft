using ASNTechnosoftWithJWT.Areas.Admin.Models;
using BussinessLayer;
using Paytm.Checksum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechnosoftModel;

namespace ASNTechnosoftWithJWT.Areas.Admin.Controllers
{
    public class PaytmPaymentController : Controller
    {
        // GET: InstituteAdmin/PaytmPayment
        public ActionResult Index()
        {
            User Usermodel = TempData["UserObject"] as User;
            TempData.Keep();
            PaymentInitiateModel data = new PaymentInitiateModel();
            data.InstituteId = Usermodel.InstituteId;
            data.name = Usermodel.FirstName + " " + Usermodel.MiddleName + " " + Usermodel.LastName;
            data.email = Usermodel.Username;
            data.contactNumber = Usermodel.MobileNo;
            return View(data);
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
            parameters.Add("CALLBACK_URL", "http://ims.ASNTechnosoftWithJWT.com/InstituteAdmin/PaytmPayment/PaytmResponse");
            //https://localhost:44301/InstituteAdmin/PaytmPayment/PaytmResponse  
            string checksum = CheckSum.GenerateCheckSum(Key.merchantKey, parameters);
            // string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + parameters.FirstOrDefault(x => x.Key == "ORDER_ID").Value;

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

            PaytmResponse paytmResponse = new PaytmResponse();
            paytmResponse.ORDERID = response.ORDERID;
            paytmResponse.BANKNAME = response.BANKNAME;
            paytmResponse.BANKTXNID = response.BANKTXNID;
            paytmResponse.CHECKSUMHASH = response.CHECKSUMHASH;
            paytmResponse.CURRENCY = response.CURRENCY;
            paytmResponse.GATEWAYNAME = response.GATEWAYNAME;
            paytmResponse.InstituteId = wallet.InstituteId;
            paytmResponse.InstituteName = InstituteBL.GetById(wallet.InstituteId).InstituteName;
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
            if (response.STATUS == "TXN_SUCCESS")
            {
                ViewBag.Show = true;
            }
            else
            {
                ViewBag.Show = false;
            }
            PaytmPaymentResponse paytmPaymentResponse = PaytmPaymentResponseBL.GetById(walltxn.Id);
            if (paytmPaymentResponse == null)
            {
                PaytmPaymentResponseBL.Add(dbResponse);
                if (response.RESPCODE == "01")
                {

                    if (wallet.Id != 0)
                    {
                        wallet.AvailableAmount += Convert.ToDecimal(response.TXNAMOUNT);
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
                }
            }
            return View("paytmResponse", paytmResponse);
        }


        public class TransactionModel
        {
            public int InstituteId { get; set; }
            public string InstituteName { get; set; }
            public string DepositedAmount { get; set; }
            public string UtilizedAmount { get; set; }
            public string AvailableAmount { get; set; }
            public IEnumerable<EWalletTransation> walletTransaction { get; set; }
        }

        [HttpGet]
        public ActionResult MyTransactions()
        {
            User model = TempData["UserObject"] as User;
            TempData.Keep();

            TransactionModel data = new TransactionModel
            {
                InstituteId = model.InstituteId,
                InstituteName = InstituteBL.GetById(Convert.ToInt32(model.InstituteId)).InstituteName,
                DepositedAmount = EWalletBL.GetTotalDeposited(Convert.ToInt32(model.InstituteId)).ToString(),
                UtilizedAmount = EWalletBL.GetTotalUtilised(Convert.ToInt32(model.InstituteId)).ToString(),
                AvailableAmount = EWalletBL.GetAvailableAmount(Convert.ToInt32(model.InstituteId)).ToString(),
                walletTransaction = EWalletBL.GetMyTransactions(Convert.ToInt32(model.InstituteId))
            };

            ViewBag.ShowDiv = false;
            return View("MyTransactions", data);
        }

        public ActionResult ViewStatement(int? instituteid)
        {
            if (instituteid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                TransactionModel data = new TransactionModel();
                data.InstituteId = Convert.ToInt32(instituteid);
                data.InstituteName = InstituteBL.GetById(Convert.ToInt32(instituteid)).InstituteName;
                data.DepositedAmount = EWalletBL.GetTotalDeposited(Convert.ToInt32(instituteid)).ToString();
                data.UtilizedAmount = EWalletBL.GetTotalUtilised(Convert.ToInt32(instituteid)).ToString();
                data.AvailableAmount = EWalletBL.GetAvailableAmount(Convert.ToInt32(instituteid)).ToString();
                data.walletTransaction = EWalletBL.GetMyTransactions(Convert.ToInt32(instituteid));
                if (data.walletTransaction != null)
                {
                    ViewBag.ShowDiv = true;
                }
                else
                {
                    ViewBag.ShowDiv = false;
                }
                return View("MyTransactions", data);
            }

        }
    }
}