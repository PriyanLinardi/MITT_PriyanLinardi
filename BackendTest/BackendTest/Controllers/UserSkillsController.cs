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
    public class UserSkillsController : ApiController
    {
        [Route("api/UserSkills/GetUserSkill/{username}")]
        [HttpGet]
        public UserSkills GetUserSkill(string userName)
        {
            return UserSkills.GetList().Where(a => a.UserName == userName).FirstOrDefault();
        }

        [Route("api/UserSkills/Create/{userSkillID}/{skillId:int}/{skillLevelId:int}")]
        [HttpGet]
        public HttpResponseMessage Create(string userSkillID, int skillId, int skillLevelId)
        {
            try
            {
                string userName = Service.GetUserNameLogin();
                string queryText = "INSERT INTO UserSkills VALUES(@userSkillId,@userName,@skillId,@skillLevelId)";
                SqlParameter[] param =
                {   new SqlParameter("@userSkillId", userSkillID),
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@skillId", skillId),
                    new SqlParameter("@skillLevelId", skillLevelId)
            };
                ExecuteDb exec = new ExecuteDb(queryText, param);
                exec.ExecuteInsertUpdateDelete();
                return Request.CreateResponse(HttpStatusCode.OK, "New user skill has been created");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "error! Please contact your administrator");
            }
        }

        [Route("api/UserSkills/Update/{userSkillID}/{skillId:int}/{skillLevelId:int}")]
        [HttpGet]
        public HttpResponseMessage Update(string userSkillID, int skillId, int skillLevelId)
        {
            string userName = Service.GetUserNameLogin();
            string queryText = $@"UPDATE UserSkills SET
                skillId=@skillId, skillLevelId=@skillLevelId
                WHERE userSkillID=@id AND userName=@UserName";
            SqlParameter[] param =
            {   new SqlParameter("@userSkillId", userSkillID),
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@skillId", skillId),
                    new SqlParameter("@skillLevelId", skillLevelId)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "skill updated");
        }

        [Route("api/UserSkills/Delete/{skillLevelId:int}")]
        [HttpGet]
        public HttpResponseMessage Delete(int skillLevelId)
        {
            string userName = Service.GetUserNameLogin();
            string queryText = $@"Delete From UserSkills WHERE skillLevelId=@skillLevelId AND UserName=@userName";
            SqlParameter[] param =
            {   new SqlParameter("@skillLevelId", skillLevelId),
                new SqlParameter("@userName", userName)
            };
            ExecuteDb exec = new ExecuteDb(queryText, param);
            exec.ExecuteInsertUpdateDelete();
            return Request.CreateResponse(HttpStatusCode.OK, "skill deleted");
        }
    }
}
