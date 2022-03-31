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
    public class PetFoodConfiguration : IEntityTypeConfiguration<PetFood>
    {
        public void Configure(EntityTypeBuilder<PetFood> builder)
        {
            builder.ToTable("PetFoods");
            builder.Property(petFood => petFood.ID);
            builder.Property(petFood => petFood.AnimalType).HasConversion(animalType => animalType.ToString(), animalType => (AnimalType)Enum.Parse(typeof(AnimalType), animalType)).HasMaxLength(15);
            builder.Property(petFood => petFood.Price).HasPrecision(10, 3);
            builder.Property(petFood => petFood.Cost).HasPrecision(10, 3);

            builder.HasMany(petFood => petFood.Transactions)
                .WithOne(transaction => transaction.PetFood)
                .HasForeignKey(transaction => transaction.PetFoodID);
        }
    }
}
