
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ASNTechnosoftWithJWT.Areas.Admin.Models
{
    //[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CheckSessionOutAttribute : ActionFilterAttribute
    {
        public object SessionStore { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            if (HttpContext.Current.Session["UserId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }
            base.OnActionExecuting(filterContext);

        }
    }
}