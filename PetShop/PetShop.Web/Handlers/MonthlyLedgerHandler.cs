using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.Web.Handlers
{
    public class MonthlyLedgerHandler
    {
        private readonly PetShopContext _context;
        private readonly MonthlyLedger _monthlyLedger;

        public MonthlyLedgerHandler(PetShopContext context, MonthlyLedger monthlyLedger)
        {
            _context = context;
            _monthlyLedger = monthlyLedger;
        }

        public async Task<decimal> GetIncome(MonthlyLedger monthlyLedger)
        {
            int year = Int32.Parse(monthlyLedger.Year);
            int month = Int32.Parse(monthlyLedger.Month);
            return await _context.Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).SumAsync(t => t.TotalPrice);
        }

        private async Task<decimal> GetPetAndPetFoodExpences(MonthlyLedger monthlyLedger)
        {
            int year = Int32.Parse(monthlyLedger.Year);
            int month = Int32.Parse(monthlyLedger.Month);
            var monthlyTransactions = await _context.Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).ToListAsync();
            decimal expences = 0;
            foreach (var t in monthlyTransactions)
            {
                var pet = await _context.Pets.FirstOrDefaultAsync(p => p.ID == t.PetID);
                var petFood = await _context.PetFoods.FirstOrDefaultAsync(p => p.ID == t.PetFoodID);
                expences += pet.Cost + petFood.Cost * t.PetFoodQty;
            }
            return expences;
        }

        private async Task<decimal> GetStuffExpences()
        {
            return await _context.Employees.SumAsync(e => e.SallaryPerMonth);
        }

        public async Task<decimal> GetMonthlyExpenses(MonthlyLedger monthlyLedger)
        {
            return await GetPetAndPetFoodExpences(monthlyLedger) +  await GetStuffExpences() + 1200; 
        }

        public decimal GetTotal(MonthlyLedger monthlyLedger)
        {
            return monthlyLedger.Income - monthlyLedger.Expenses;
        }
    }
}
