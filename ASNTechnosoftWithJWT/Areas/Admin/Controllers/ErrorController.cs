using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASNTechnosoftWithJWT.Areas.Admin.Controllers
{
    public class ErrorController : Controller
    {
        // GET: InstituteAdmin/Error
        public ActionResult NotFound()
        {
            return View("Error");
        }
    }
}