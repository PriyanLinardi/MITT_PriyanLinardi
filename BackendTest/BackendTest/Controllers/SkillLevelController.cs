using BackendTest.Models;
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
    public class SkillLevelController : ApiController
    {
        [Route("api/SkillLevel/GetSkillLevels")]
        [HttpGet]
        public List<SkillLevel> GetSkillLevels()
        {
            return SkillLevel.GetList();
        }

        [Route("api/SkillLevel/GetSkillLevel/{id:int}")]
        [HttpGet]
        public SkillLevel GetSkillLevel(int id)
        {
            return SkillLevel.GetList().Where(a => a.SkillLevelId == id).FirstOrDefault();
        }

        [Route("api/SkillLevel/Create/{name}")]
        [HttpGet]
        public HttpResponseMessage Create(string name)
        {
            string queryCheck = "SELECT 1 FROM SkillLevel WHERE SkillLevelName=@name";
            SqlParameter[] paramCheck = { new SqlParameter("@name", name) };
            ExecuteDb execCheck = new ExecuteDb(queryCheck, paramCheck);
            bool isExists = Convert.ToBoolean(execCheck.ExecuteScalar());

            if (!isExists)
            {
                string queryText = "INSERT INTO SkillLevel VALUES(@name)";
                SqlParameter[] param =
                {   new SqlParameter("@name", name)
                };
                ExecuteDb exec = new ExecuteDb(queryText, param);
                exec.ExecuteInsertUpdateDelete();
                return Request.CreateResponse(HttpStatusCode.OK, "New skill level has been created");
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Skill level existed");
            }
        }

        [Route("api/SkillLevel/Update/{id:int}/{name}")]
        [HttpGet]
        public HttpResponseMessage Update(int id, string name)
        {
            string queryText = $@"UPDATE SkillLevel SET
                SkillLevelName=@name
                WHERE SkillLevelId=@id";
            SqlParameter[] param =
            {   new SqlParameter("@id", id),
                    new SqlParameter("@name", name)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "skill level updated");
        }

        [Route("api/SkillLevel/Delete/{id:int}")]
        [HttpGet]
        public HttpResponseMessage Delete(int id)
        {
            string queryText = $@"Delete From SkillLevel WHERE SkillLevelId=@id";
            SqlParameter[] param =
            {   new SqlParameter("@id", id)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "skill level deleted");
        }
    }
}
