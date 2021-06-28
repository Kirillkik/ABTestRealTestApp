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

        public void AddUser(User user)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public User[] GetAllUsers()
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                return db.Users.ToArray();
            }
                
        }

        public User GetUserById(int id)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                return db.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public void UpdateUsers(User[] users)
        {
            using (DataBaseContext db = new DataBaseContext())
            {
                db.Users.UpdateRange(users);
                db.SaveChanges();
            }
        }
    }
}
