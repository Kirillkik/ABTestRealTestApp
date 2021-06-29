using ABTestRealTestApp.Interfaces;
using ABTestRealTestApp.Models;
using ABTestRealTestApp.Services;
using ABTestRealTestApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                                                                             x.RegistrationDate.Date.ToString("dd-MM-yyyy"),
                                                                             x.LastActivityDate.Date.ToString("dd-MM-yyyy")));
        }

        [HttpPost]
        public IActionResult Post()
        {
            var user = new User(0, DateTime.Now, DateTime.Now);
            userRepository.AddUser(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            userRepository.DeleteUser(user);
            return Ok(user);
        }

        [HttpPost("update")]
        public IActionResult Update(UserForTable[] users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data format");
            }
            userRepository.UpdateUsers(users.Select(x => new User(x.Id,
                                                                  DateTime.ParseExact(x.RegistrationDate, "dd-MM-yyyy", null),
                                                                  DateTime.ParseExact(x.LastActivityDate, "dd-MM-yyyy", null))).ToArray());
            return Ok("Data saved");
        }

        [HttpGet("durationoflifehistogram")]
        public IEnumerable<DataPoint> GetDurationOfLifeHistogramm()
        {
            var usersService = new UsersService(userRepository);
            var usersLifeTimeDuration = usersService.GetUsersLifeTimeDuration();
            List<DataPoint> dataPoints = usersLifeTimeDuration.Select(x => new DataPoint(x.Key, x.Value)).ToList();
            return dataPoints;
        }

        [HttpGet("getrollingretentionsevenday")]
        public double GetRollingRetentionSevenDay()
        {
            var usersService = new UsersService(userRepository);
            var rollingRetentionSevenDay = usersService.GetRollingRetentionXDay(7);
            return  rollingRetentionSevenDay;
        }
    }
}
