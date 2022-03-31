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
    public class PetReportConfiguration : IEntityTypeConfiguration<PetReport>
    {
        public void Configure(EntityTypeBuilder<PetReport> builder)
        {
            builder.ToTable("PetReports");
            builder.HasKey(petReport => petReport.ID);
            builder.Property(petReport => petReport.Year).HasMaxLength(4);
            builder.Property(petReport => petReport.Month).HasMaxLength(2);
            builder.Property(petReport => petReport.AnimalType).HasConversion(animalType => animalType.ToString(), animalType => (AnimalType)Enum.Parse(typeof(AnimalType), animalType)).HasMaxLength(15);
            
        }
    }
}
