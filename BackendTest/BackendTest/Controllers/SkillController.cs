using System;
using System.Collections.Generic;
using System.Data;
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
    public class SkillController : ApiController
    {
        [Route("api/Skill/GetSkills")]
        [HttpGet]
        public List<Skill> GetSkills()
        {
            return Skill.GetList();
        }

        [Route("api/Skill/GetSkills/{id:int}")]
        [HttpGet]
        public Skill GetSkills(int id)
        {
            return Skill.GetList().Where(a=> a.SkillId == id).FirstOrDefault();
        }

        [Route("api/Skill/Create/{name}")]
        [HttpGet]
        public HttpResponseMessage Create(string name)
        {
            string queryCheck = "SELECT 1 FROM Skill WHERE SkillName=@name";
            SqlParameter[] paramCheck = { new SqlParameter("@name", name) };
            ExecuteDb execCheck = new ExecuteDb(queryCheck, paramCheck);
            bool isExists = Convert.ToBoolean(execCheck.ExecuteScalar());

            if (!isExists)
            {
                string queryText = "INSERT INTO Skill VALUES(@name)";
                SqlParameter[] param =
                {   new SqlParameter("@name", name)
                };
                ExecuteDb exec = new ExecuteDb(queryText, param);
                exec.ExecuteInsertUpdateDelete();
                return Request.CreateResponse(HttpStatusCode.OK, "New skill has been created");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Skill existed");
            }
        }

        [Route("api/Skill/Update/{id:int}/{name}")]
        [HttpGet]
        public HttpResponseMessage Update(int id, string name)
        {
            string queryText = $@"UPDATE Skill SET
                SkillName=@name
                WHERE SkillId=@id";
            SqlParameter[] param =
            {   new SqlParameter("@id", id),
                    new SqlParameter("@name", name)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "skill updated");
        }

        [Route("api/Skill/Delete/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Delete(int id)
        {
            string queryText = $@"Delete From Skill WHERE SkillId=@id";
            SqlParameter[] param =
            {   new SqlParameter("@id", id)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "skill deleted");
        }
    }
}
