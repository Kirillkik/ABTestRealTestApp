using ABTestRealTestApp.Interfaces;
using ABTestRealTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABTestRealTestApp.Memory
{
    public class UserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User(1, DateTime.Parse("1.1.1999"), DateTime.Parse("1.10.1999")),
            new User(2, DateTime.Parse("1.1.2000"), DateTime.Parse("1.10.2000")),
            new User(3, DateTime.Parse("1.1.2001"), DateTime.Parse("1.10.2001")),
        };

        public void AddUser(User user)
        {
            user.Id = Users.Last().Id + 1;
            Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            Users.Remove(user);
        }

        public User[] GetAllUsers()
        {
            return Users.ToArray();
        }

        public User GetUserById(int id)
        {
            return Users.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateUsers(User[] users)
        {
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }
    }
}
