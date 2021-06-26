using System;
using System.Collections.Generic;
using System.Text;

namespace ABTestRealTestApp.Models
{
    public class User
    {
        public User(int id, DateTime registrationDate, DateTime lastActivityDate)
        {
            Id = id;
            RegistrationDate = registrationDate;
            LastActivityDate = lastActivityDate;
        }

        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastActivityDate { get; set; }
    }
}
