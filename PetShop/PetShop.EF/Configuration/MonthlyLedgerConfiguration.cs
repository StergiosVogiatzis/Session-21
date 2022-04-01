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
    public class MonthlyLedgerConfiguration : IEntityTypeConfiguration<MonthlyLedger>
    {
        public void Configure(EntityTypeBuilder<MonthlyLedger> builder)
        {

            builder.ToTable("MonthlyLedgers");
            builder.HasKey(monthlyLedger => monthlyLedger.ID);
            builder.Property(monthlyLedger => monthlyLedger.Year).HasMaxLength(4);
            builder.Property(monthlyLedger => monthlyLedger.Month).HasMaxLength(2);
            builder.Property(monthlyLedger => monthlyLedger.Expenses).HasPrecision(10, 3);
            builder.Property(monthlyLedger => monthlyLedger.Income).HasPrecision(10, 3);
            builder.Property(monthlyLedger => monthlyLedger.Total).HasPrecision(10, 3);


        }
    }
}
