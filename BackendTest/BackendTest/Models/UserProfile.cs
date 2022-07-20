using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackendTest.Models
{
    public class UserProfile
    {
        public UserProfile(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BOD { get; set; }
        public string Email { get; set; }
    }
}