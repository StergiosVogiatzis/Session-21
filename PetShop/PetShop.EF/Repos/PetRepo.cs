using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.EF.Repos
{
    public class PetRepo : IEntityRepo<Pet>
    {
        private readonly PetShopContext context;

        public PetRepo(PetShopContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await context.Pets.ToListAsync();
        }

        public async Task<Pet?> GetByIdAsync(Guid id)
        {
            return await context.Pets.SingleOrDefaultAsync(pet => pet.ID == id);
        }

        public async Task AddAsync(Pet entity)
        {
            AddLogic(entity, context);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Pet entity)
        {
            UpdateLogic(id, entity, context);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            DeleteLogic(id, context);
            await context.SaveChangesAsync();
        }

        private void DeleteLogic(Guid id, PetShopContext context)
        {
            var currentPet = context.Pets.SingleOrDefault(pet => pet.ID == id);
            if (currentPet is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Pets.Remove(currentPet);
        }

        private void UpdateLogic(Guid id, Pet entity, PetShopContext context)
        {
            var currentPet = context.Pets.SingleOrDefault(pet => pet.ID == id);
            if (currentPet is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            currentPet.Breed = entity.Breed;
            currentPet.AnimalType = entity.AnimalType;
            currentPet.PetStatus = entity.PetStatus;
            currentPet.Price = entity.Price;
            currentPet.Cost = entity.Cost;
        }

        private void AddLogic(Pet entity, PetShopContext context)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Pets.Add(entity);
        }
    }
}
