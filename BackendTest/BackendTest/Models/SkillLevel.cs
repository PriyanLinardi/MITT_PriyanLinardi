using BackendTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTest.Models
{
    public class SkillLevel
    {
        public int SkillLevelId { get; set; }
        public string SkillLevelName { get; set; }

        public static List<SkillLevel> GetList()
        {
            List<SkillLevel> skills = new List<SkillLevel>();
            string queryCheck = "SELECT * FROM SkillLevel";
            ExecuteDb execCheck = new ExecuteDb(queryCheck);
            skills = execCheck.GetList<SkillLevel>();
            return skills;
        }
    }
}