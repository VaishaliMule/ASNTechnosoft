
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechnosoftModel;
using WebIMSWithJWT.Models;

namespace WebIMSWithJWT.Controllers
{
  //  [Authorize]
    public class HomeController : Controller
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}