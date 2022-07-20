using BackendTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTest.Models
{
    public class UserSkills
    {
        public string UserSkillId { get; set; }
        public string UserName { get; set; }
        public int SkillId { get; set; }
        public int SkillLevelId { get; set; }

        public static List<UserSkills> GetList()
        {
            List<UserSkills> skills = new List<UserSkills>();
            string queryCheck = "SELECT * FROM UserSkills";
            ExecuteDb execCheck = new ExecuteDb(queryCheck);
            skills = execCheck.GetList<UserSkills>();
            return skills;
        }
    }
}