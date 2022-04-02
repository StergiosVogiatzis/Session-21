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
    public class MonthlyLedgerRepo : IEntityRepo<MonthlyLedger>
    {
        private readonly PetShopContext context;

        public MonthlyLedgerRepo(PetShopContext context)
        {
            this.context = context; 
        }

        public async Task<IEnumerable<MonthlyLedger>> GetAllAsync()
        {
            return await context.MonthlyLedgers.ToListAsync();
        }

        public async Task<MonthlyLedger?> GetByIdAsync(Guid id)
        {
            return await context.MonthlyLedgers.SingleOrDefaultAsync(mothlyLedger => mothlyLedger.ID == id);
        }

        public async Task AddAsync(MonthlyLedger entity)
        {
            AddLogic(entity, context);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, MonthlyLedger entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid id)
        {
            DeleteLogic(id, context);
            await context.SaveChangesAsync();
        }

        private void AddLogic(MonthlyLedger entity, PetShopContext context)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.MonthlyLedgers.Add(entity);
        }

        private void DeleteLogic(Guid id, PetShopContext context)
        {
            var ledger = context.MonthlyLedgers.SingleOrDefault(mledger => mledger.ID == id);
            if (ledger is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.MonthlyLedgers.Remove(ledger);
        }
    }
}
