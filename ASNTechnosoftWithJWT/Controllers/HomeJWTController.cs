using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASNTechnosoftWithJWT.Controllers
{
    public class HomeJWTController : Controller
    {
        private static string WebAPIURL = "https://localhost:44325/";
        
        public async Task<ActionResult> Index()
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                var responseMessage = await client.GetAsync(requestUri: "Account/ValidLogin?userName=vaishalimule2011@gmail.com&userPassword=9657855570");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenBased = JsonConvert.DeserializeObject<string>(resultMessage);
                    Session["TokenNumber"] = tokenBased;
                }
            }
        // return Content(tokenBased);
          return View();
            

        }



        public async Task<ActionResult> GetUser()
        {
            string ReturnMessage = string.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType: "application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "Bearer", parameter: Session["TokenNumber"].ToString());

                var responseMessage = await client.GetAsync(requestUri: "Account/GetUser");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    ReturnMessage = JsonConvert.DeserializeObject<string>(resultMessage);

                }
            }
            return Content(ReturnMessage);

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