using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechnosoftModel;
using WebIMSWithJWT.Models;

namespace WebIMSWithJWT.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ValidLogin(string userName, string userPassword)
        {
            User user = UsersBL.GetByUserNamePassword(userName, userPassword);
            if (user!=null)
            {
               
              return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(user.Username));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "User name and password is invalid");
            }
        }

        [HttpGet]
        public HttpResponseMessage ValidLoginToken(string token)
        {
            
            if (token != null)
            {

                return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.ValidateToken(token));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "User name and password is invalid");
            }
        }


        [HttpGet]
        [CustomAuthenticationFilter]
        public HttpResponseMessage GetUser()
        {
            return Request.CreateResponse(HttpStatusCode.OK, value: "Successfully valid");
        }
    }
}
