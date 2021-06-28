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
            optionsBuilder.UseNpgsql("Host=ec2-54-155-226-153.eu-west-1.compute.amazonaws.com;Port=5432;Database=d5eq1qihhq1qn5;Username=hzbqqkwbltarhw;Password=fbcc46e0c7fed98fa174d948099426fca5ea79a020b58180b59fbac4d6499f5f; TrustServerCertificate = true; SslMode = Require; ");
        }
    }
}
