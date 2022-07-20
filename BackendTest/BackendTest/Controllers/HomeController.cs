using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackendTest.Services;

namespace BackendTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string err)
        {
            ViewBag.Title = "Home Page";
            ViewBag.ErrMessage = err;

            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
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
                return Json(new { success = true });
            }
            else
                return Json(new { success = false, err = "Invalid username or password" });
        }
    }
}
