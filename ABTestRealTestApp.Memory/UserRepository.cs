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
        private readonly List<User> Users = new List<User>()
        {
            new User(1, DateTime.Parse("1.1.1999"), DateTime.Parse("1.10.1999")),
            new User(2, DateTime.Parse("1.1.2000"), DateTime.Parse("1.10.2000")),
            new User(3, DateTime.Parse("1.1.2001"), DateTime.Parse("1.10.2001")),
        };

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void AddUsers(User[] users)
        {
            Users.AddRange(users);
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
    }
}
