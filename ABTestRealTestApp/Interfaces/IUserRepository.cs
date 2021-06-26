using ABTestRealTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABTestRealTestApp.Interfaces
{
    public interface IUserRepository
    {
        User[] GetAllUsers();
        void AddUser(User user);
        void AddUsers(User[] users);
    }
}
