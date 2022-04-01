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
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("Pets");
            builder.HasKey(pet => pet.ID);
            builder.Property(pet => pet.Breed).HasMaxLength(20);
            builder.Property(pet => pet.AnimalType).HasConversion(animalType => animalType.ToString(), animalType => (AnimalType)Enum.Parse(typeof(AnimalType), animalType)).HasMaxLength(15);
            builder.Property(pet => pet.PetStatus).HasConversion(petStatus => petStatus.ToString(), petStatus => (PetStatus)Enum.Parse(typeof(PetStatus), petStatus)).HasMaxLength(15);
            builder.Property(pet => pet.Price).HasPrecision(10, 3);
            builder.Property(pet => pet.Cost).HasPrecision(10, 3);
           

            builder.HasOne(pet => pet.Transaction)
                .WithOne(transaction => transaction.Pet)
                .HasForeignKey<Pet>(pet => pet.TransactionID);
        }
    }
}
