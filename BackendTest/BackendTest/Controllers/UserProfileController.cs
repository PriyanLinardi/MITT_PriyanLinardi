using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BackendTest.Models;
using BackendTest.Services;

namespace BackendTest.Controllers
{
    public class UserProfileController : ApiController
    {
        // GET: api/UserProfile
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("api/UserProfile/Register/{name}/{address}/{email}/{bod}")]
        [HttpGet]
        public HttpResponseMessage Register(string name, string address, string email, string bod)
        {
            string userName = Service.GetUserNameLogin();

            string queryCheck = "SELECT 1 FROM UserProfiles WHERE username=@username";
            SqlParameter[] paramCheck = {   new SqlParameter("@userName", userName) };
            ExecuteDb execCheck = new ExecuteDb(queryCheck, paramCheck);
            bool isExists = Convert.ToBoolean(execCheck.ExecuteScalar());

            if (!isExists)
            {
                string queryText = "INSERT INTO UserProfiles VALUES(@userName, @name, @address, @bod, @email)";
                DateTime bodDate = Convert.ToDateTime(bod);
                SqlParameter[] param =
                {   new SqlParameter("@userName", userName),
                    new SqlParameter("@name", name),
                    new SqlParameter("@address", address),
                    new SqlParameter("@email", email),
                    new SqlParameter("@bod", bodDate)
                };
                ExecuteDb exec = new ExecuteDb(queryText, param);
                exec.ExecuteInsertUpdateDelete();
                return Request.CreateResponse(HttpStatusCode.OK, "username registered");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User profile already registered");
            }
        }

        [Route("api/UserProfile/Update/{name}/{address}/{email}/{bod}")]
        [HttpGet]
        public HttpResponseMessage Update(string name, string address, string email, string bod)
        {
            string userName = Service.GetUserNameLogin();
            string queryText = $@"UPDATE UserProfiles SET
                name=@name, address=@address, email=@email, bod=@bod
                WHERE userName=@userName";
            DateTime bodDate = Convert.ToDateTime(bod);
            SqlParameter[] param =
            {   new SqlParameter("@userName", userName),
                    new SqlParameter("@name", name),
                    new SqlParameter("@address", address),
                    new SqlParameter("@email", email),
                    new SqlParameter("@bod", bodDate)
                };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "username updated");
        }
    }
}
