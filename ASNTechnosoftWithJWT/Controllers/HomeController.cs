using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASNTechnosoftWithJWT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult IMSDetails()
        {
            return View("IMSDetails");
        }

        public ActionResult PANdetails()
        {
            return View("PANdetails");
        }
        public ActionResult PMJAYdetails()
        {
            return View("PMJAYdetails");
        }
        public ActionResult Pricing()
        {
            return View("Pricing");
        }
        public ActionResult PricingPolicy()
        {
            return View("PricingPolicy");
        }
        public ActionResult PrivacyPolicy()
        {
            return View("PrivacyPolicy");
        }
        public ActionResult RefundPolicy()
        {
            return View("RefundPolicy");
        }
        public ActionResult Services()
        {
            return View("Services");
        }

        public ActionResult Team()
        {
            return View("Team");
        }
        public ActionResult TermsAndConditions()
        {
            return View("TermsAndConditions");
        }
    }
}