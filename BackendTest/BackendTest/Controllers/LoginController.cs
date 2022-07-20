using BackendTest.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BackendTest.Controllers
{
    public class LoginController : ApiController
    {
        [Route("api/Login/{username}/{password}")]
        [HttpGet]
        public HttpResponseMessage Login(string userName, string password)
        {
            string queryText = "SELECT 1 FROM [User] WHERE username=@username AND password=@password";
            SqlParameter[] param =
            {   new SqlParameter("@username", userName),
                new SqlParameter("@password", password)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            bool isValid = Convert.ToBoolean(exec.ExecuteScalar());

            if (isValid)
            {
                Service.SetUserNameCache(userName);
                return Request.CreateResponse(HttpStatusCode.OK, "login success");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "invalid username or password");
            }
        }

        [Route("api/Logout")]
        [HttpGet]
        public HttpResponseMessage Logout()
        {
            Service.EmptyUserNameCache();
            return Request.CreateResponse(HttpStatusCode.OK, "logout success");
        }
    }
}
