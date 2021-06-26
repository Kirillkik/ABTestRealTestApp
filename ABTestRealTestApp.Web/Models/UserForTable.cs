using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABTestRealTestApp.Web.Models
{
    public class UserForTable
    {
        public UserForTable(int id, string registrationDate, string lastActivityDate)
        {
            Id = id;
            RegistrationDate = registrationDate;
            LastActivityDate = lastActivityDate;
        }

        public int Id { get; set; }
        public string RegistrationDate { get; set; }
        public string LastActivityDate { get; set; }
    }
}
