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
    public class PetFoodRepo : IEntityRepo<PetFood>
    {
        private readonly PetShopContext context;

        public PetFoodRepo(PetShopContext dbcontext)
        {
            context = dbcontext;
        }

        public async Task<IEnumerable<PetFood>> GetAllAsync()
        {
            return await context.PetFoods.ToListAsync();
        }

        public async Task<PetFood?> GetByIdAsync(Guid id)
        {
            return await context.PetFoods.SingleOrDefaultAsync(pet => pet.ID == id);
        }

        public async Task AddAsync(PetFood entity)
        {
            AddLogic(entity, context);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, PetFood entity)
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
            var currentPetFood = context.PetFoods.SingleOrDefault(petFood => petFood.ID == id);
            if (currentPetFood is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.PetFoods.Remove(currentPetFood);
        }

        private void UpdateLogic(Guid id, PetFood entity, PetShopContext context)
        {
            var currentPetFood = context.PetFoods.SingleOrDefault(petFood => petFood.ID == id);
            if (currentPetFood is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            currentPetFood.AnimalType = entity.AnimalType;
            currentPetFood.Price = entity.Price;
            currentPetFood.Cost = entity.Cost;
        }

        private void AddLogic(PetFood entity, PetShopContext context)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.PetFoods.Add(entity);
        }

    }
}
