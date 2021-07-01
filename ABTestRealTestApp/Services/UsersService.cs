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

        public double GetRollingRetentionXDay(int xDay)
        {
            double retutnUsersNumber = 0;
            double installAppUsersNumber = 0;
            var users = userRepository.GetAllUsers();
            foreach (var user in users)
            {
                if ((user.LastActivityDate - user.RegistrationDate).TotalDays >= xDay) retutnUsersNumber++;
                if ((DateTime.Now - user.RegistrationDate).TotalDays >= xDay) installAppUsersNumber++;
            }
            var result = retutnUsersNumber / installAppUsersNumber * 100;
            return installAppUsersNumber == 0 ? 0 : result;
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
