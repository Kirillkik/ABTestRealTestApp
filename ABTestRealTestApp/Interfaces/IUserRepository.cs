using ABTestRealTestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABTestRealTestApp.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User[] GetAllUsers();
        void AddUser(User user);
        void DeleteUser(User user);
    }
}
