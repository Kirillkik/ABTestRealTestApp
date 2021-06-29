using ABTestRealTestApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABTestRealTestApp.Memory
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Uid=postgres;Pwd=Rbhbkk9105.;Host=34.142.36.147; Database=ABTestRealTestApp-fe1316f5-48e2-4739-ae44-eccbe217ab10 ");
        }
    }
}
