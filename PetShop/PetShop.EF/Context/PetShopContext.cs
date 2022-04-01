using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.EF.Configuration;
using PetShop.Model;

namespace PetShop.EF.Context
{
    public class PetShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PetFood> PetFoods { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PetReport> PetReports { get; set; }
        public DbSet<MonthlyLedger> MonthlyLedgers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new PetFoodConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new MonthlyLedgerConfiguration());
            modelBuilder.ApplyConfiguration(new PetReportConfiguration());






        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=DbPetShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connString);
        }
    }
}
