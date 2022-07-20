using BackendTest.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTest.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }

        public static List<Skill> GetList()
        {
            List<Skill> skills = new List<Skill>();
            string queryCheck = "SELECT * FROM Skill";
            ExecuteDb execCheck = new ExecuteDb(queryCheck);
            skills = execCheck.GetList<Skill>();
            return skills;
        }
    }
}