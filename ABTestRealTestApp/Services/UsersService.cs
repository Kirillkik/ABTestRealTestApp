using ABTestRealTestApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABTestRealTestApp.Services
{
    public class UsersService
    {
        private readonly IUserRepository userRepository;
        public UsersService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Dictionary<int, int> GetUsersLifeTimeDuration()
        {
            Dictionary<int, int> dataPoints = new Dictionary<int, int>();

            foreach (var user in userRepository.GetAllUsers())
            {
                int key = (int)(user.LastActivityDate - user.RegistrationDate).TotalDays;
                int value;
                if(dataPoints.TryGetValue(key, out value))
                {
                    dataPoints[key]++;
                }
                else
                {
                    dataPoints.Add(key, 1);
                }

            }

            return dataPoints;
        }
    }
}
