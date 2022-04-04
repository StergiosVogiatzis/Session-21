using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Model;

namespace PetShop.EF.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.Property(transaction => transaction.ID);
            builder.Property(transaction => transaction.PetFoodPrice).HasPrecision(10, 3);
            builder.Property(transaction => transaction.PetPrice).HasPrecision(10, 3);
            builder.Property(transaction => transaction.CustomerID).IsRequired();
            builder.Property(transaction => transaction.EmployeeID).IsRequired();
            builder.Property(transaction => transaction.PetID).IsRequired();
            builder.Property(transaction => transaction.PetFoodID).IsRequired();

            builder.HasOne(transaction => transaction.Customer)
                .WithMany(customer => customer.Transactions)
                .HasForeignKey(transaction => transaction.CustomerID);

            builder.HasOne(transaction => transaction.Pet)
                .WithOne(pet => pet.Transaction)
                .HasForeignKey<Transaction>(transaction => transaction.PetID);

            builder.HasOne(transaction => transaction.PetFood)
                .WithMany(petFood => petFood.Transactions)
                .HasForeignKey(transaction => transaction.PetFoodID);

            builder.HasOne(transaction => transaction.Employee)
                .WithMany(employee => employee.Transactions)
                .HasForeignKey(transaction => transaction.EmployeeID);
        }
    }
}
