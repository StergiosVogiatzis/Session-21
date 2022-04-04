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
    public class CustomerRepo : IEntityRepo<Customer>
    {
        private readonly PetShopContext context;

        public CustomerRepo(PetShopContext dbcontext)
        {
            context = dbcontext;
        }
        
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await context.Customers.SingleOrDefaultAsync(customer => customer.ID == id);
        }

        public async Task AddAsync(Customer entity)
        {
            await AddLogic(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid id, Customer entity)
        {
            UpdateLogic(id, entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            DeleteLogic(id);
            await context.SaveChangesAsync();
        }

        private void DeleteLogic(Guid id)
        {
            var currentCustomer = context.Customers.SingleOrDefault(customer => customer.ID == id);
            if (currentCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Customers.Remove(currentCustomer);
        }

        private void UpdateLogic(Guid id, Customer entity)
        {
            var currentCustomer = context.Customers.SingleOrDefault(customer => customer.ID == id);
            if (currentCustomer is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            
            currentCustomer.Name = entity.Name;
            currentCustomer.Surname = entity.Surname;
            currentCustomer.TIN = entity.TIN;
            currentCustomer.Phone = entity.Phone;
            
        }

        private async Task AddLogic(Customer entity)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));
            if (!(await TINExists(entity)))
            {
                context.Customers.Add(entity);
            }
            
        }

        public async Task<bool> TINExists(Customer entity)
        {
            return await context.Customers.SingleOrDefaultAsync(customer => customer.TIN == entity.TIN) is not null;
        }
    }
}
