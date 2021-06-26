using ABTestRealTestApp.Interfaces;
using ABTestRealTestApp.Models;
using ABTestRealTestApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABTestRealTestApp.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<UserForTable> Get()
        {
            return userRepository.GetAllUsers().Select(x => new UserForTable(x.Id,
                                                                             x.RegistrationDate.Date.ToShortDateString(),
                                                                             x.LastActivityDate.Date.ToShortDateString()));
        }
    }
}
