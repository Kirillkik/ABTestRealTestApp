using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [RegularExpression("^(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)[0-9][0-9]$")]
        [Required]
        public string RegistrationDate { get; set; }

        [RegularExpression("^(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)[0-9][0-9]$")]
        [Required]
        public string LastActivityDate { get; set; }
    }
}
