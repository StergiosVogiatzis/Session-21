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
    public class TransactionRepo : IEntityRepo<Transaction>
    {
        private readonly PetShopContext _context;

        public TransactionRepo(PetShopContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Transaction entity)
        {
            AddLogic(entity, _context);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            DeleteLogic(id, _context);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.Include(transaction => transaction.Pet).Include(transaction => transaction.Customer).Include(transaction => transaction.Employee).Include(transaction => transaction.PetFood).ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.SingleOrDefaultAsync(transaction => transaction.ID == id);
        }

        public async Task UpdateAsync(Guid id, Transaction entity)
        {
            await _context.SaveChangesAsync();
        }
        private void AddLogic(Transaction entity, PetShopContext context)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Transactions.Add(entity);
        }
        private void DeleteLogic(Guid id, PetShopContext context)
        {
            var currentTransaction = context.Transactions.SingleOrDefault(transaction => transaction.ID == id);
            if (currentTransaction is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Transactions.Remove(currentTransaction);
        }


        
    }
}
