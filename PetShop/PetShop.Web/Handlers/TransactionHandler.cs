using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.Web.Handlers
{
    public class TransactionHandler
    {
        private readonly Transaction _transaction;
        private readonly PetShopContext _context;

        public TransactionHandler(Transaction transaction, PetShopContext context)
        {
            _transaction = transaction;
            _context = context;
        }

        public async Task<List<Pet>> GetAvailablePets()
        {
            return await _context.Pets.Where(p => p.PetStatus == PetStatus.Recovering || p.PetStatus == PetStatus.OK).ToListAsync();
        }

        public decimal GetTotalPrice()
        {
            return _transaction.PetPrice + (_transaction.PetFoodQty - 1) * _transaction.PetFoodPrice;
        }

        public async Task SetPetFood()
        {
            var petFood = await _context.PetFoods.FirstOrDefaultAsync(p => p.AnimalType == _transaction.Pet.AnimalType);
            _transaction.PetFood.ID = petFood.ID;
            _transaction.PetFood.Price = petFood.Price;
            _transaction.PetFood.Cost = petFood.Cost;
            _transaction.PetFood.AnimalType = petFood.AnimalType;
            _transaction.PetFoodID = petFood.ID;
        }
    }
}
