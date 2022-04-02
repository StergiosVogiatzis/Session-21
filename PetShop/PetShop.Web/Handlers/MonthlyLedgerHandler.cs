using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.Web.Handlers
{
    public class MonthlyLedgerHandler
    {
        private const int _RENT_COST = 2000;
        private readonly PetShopContext _context;
        private readonly MonthlyLedger _monthlyLedger;

        public MonthlyLedgerHandler(PetShopContext context)
        {
            _context = context;
        }

        public MonthlyLedgerHandler(PetShopContext context, MonthlyLedger monthlyLedger) : this(context)
        {
            _monthlyLedger = monthlyLedger;
        }

        public async Task<decimal> GetIncome()
        {
            int year = Int32.Parse(_monthlyLedger.Year);
            int month = Int32.Parse(_monthlyLedger.Month);
            var final=await _context.Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).SumAsync(t => t.TotalPrice);
            return final;
        }

        private async Task<decimal> GetPetAndPetFoodExpences()
        {
            decimal year = Decimal.Parse(_monthlyLedger.Year);
            decimal month = Decimal.Parse(_monthlyLedger.Month);
            var monthlyTransactions = await _context.Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).ToListAsync();
            decimal expences = 0;
            foreach (var t in monthlyTransactions)
            {
                var pet = await _context.Pets.FirstOrDefaultAsync(p => p.ID == t.PetID);
                var petFood = await _context.PetFoods.FirstOrDefaultAsync(p => p.ID == t.PetFoodID);
                expences += pet.Cost + (petFood.Cost * t.PetFoodQty);
            }
            return expences;
        }

        private async Task<decimal> GetStuffExpences()
        {
            var final= await _context.Employees.SumAsync(e => e.SallaryPerMonth);
            return final;
        }

        public async Task<decimal> GetMonthlyExpenses()
        {
            return await GetPetAndPetFoodExpences() +  await GetStuffExpences() + _RENT_COST; 
        }

        public decimal GetTotal(MonthlyLedger monthlyLedger)
        {
            return monthlyLedger.Income - monthlyLedger.Expenses;
        }
    }
}
