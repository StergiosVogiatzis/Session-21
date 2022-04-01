using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.EF.Repos
{
    public class EmployeeRepo : IEntityRepo<Employee>
    {
        private readonly PetShopContext context;
        public EmployeeRepo(PetShopContext dbcontext)
        {
            context = dbcontext;
        }
        public async Task AddAsync(Employee entity)
        {
            AddLogic(entity, context);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            DeleteLogic(id, context);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(Guid id)
        {
            return await context.Employees.SingleOrDefaultAsync(employee => employee.ID == id);
        }

        public async Task UpdateAsync(Guid id, Employee entity)
        {
            UpdateLogic(id, entity, context);
            await context.SaveChangesAsync();
        }
        private void AddLogic(Employee entity, PetShopContext context)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            context.Employees.Add(entity);
        }
        private void DeleteLogic(Guid id, PetShopContext context)
        {
            var currentEmployee = context.Employees.SingleOrDefault(employee => employee.ID == id);
            if (currentEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            context.Employees.Remove(currentEmployee);
        }
        private void UpdateLogic(Guid id, Employee entity, PetShopContext context)
        {
            var currentEmployee = context.Employees.SingleOrDefault(employee => employee.ID == id);
            if (currentEmployee is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");
            currentEmployee.Name = entity.Name;
            currentEmployee.Surname = entity.Surname;
            currentEmployee.SallaryPerMonth = entity.SallaryPerMonth;
            currentEmployee.EmployeeType = entity.EmployeeType;
        }
    }
}
