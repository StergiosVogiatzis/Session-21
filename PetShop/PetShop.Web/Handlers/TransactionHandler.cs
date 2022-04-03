using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.Web.Handlers
{
    public class TransactionHandler
    {
        private readonly Transaction _transaction;
        private readonly PetShopContext _context;

        public TransactionHandler(PetShopContext context)
        {
            _context = context;
        }

        public TransactionHandler(Transaction transaction, PetShopContext context) :this(context)
        {
            _transaction = transaction;
        }

        public IEnumerable<Pet> GetAvailablePets()
        {
            return _context.Pets.Where(p => p.PetStatus == PetStatus.Recovering || p.PetStatus == PetStatus.OK).ToList();
        }

        public decimal GetTotalPrice()
        {
            return _transaction.PetPrice + (_transaction.PetFoodQty - 1) * _transaction.PetFoodPrice;
        }

        public async Task<bool> SetPetFood()
        {
            var petFood = await _context.PetFoods.FirstOrDefaultAsync(p => p.AnimalType == _transaction.Pet.AnimalType);
            if (petFood == null)
                return false;
            _transaction.PetFood= petFood;
            _transaction.PetFoodPrice=petFood.Price;
            return true;
        }
    }
}
