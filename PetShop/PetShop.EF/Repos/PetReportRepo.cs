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
    public class PetReportRepo : IEntityRepo<PetReport>
    {
        private readonly PetShopContext _context;

        public PetReportRepo(PetShopContext context)
        {
            _context = context;
        }
        public async Task AddAsync(PetReport entity)
        {
            AddLogic(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            DeleteLogic(id);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PetReport>> GetAllAsync()
        {
            return await _context.PetReports.ToListAsync();
        }

        public async Task<PetReport?> GetByIdAsync(Guid id)
        {
            return await _context.PetReports.SingleOrDefaultAsync(petReport => petReport.ID == id);
        }

        public Task UpdateAsync(Guid id, PetReport entity)
        {
            throw new NotImplementedException();
        }

        private void AddLogic(PetReport entity)
        {
            if (entity.ID == Guid.Empty)
                throw new ArgumentException("Given entity should not have Id set", nameof(entity));

            _context.PetReports.Add(entity);
        }

        private void DeleteLogic(Guid id)
        {
            var currentPetReport = _context.PetReports.SingleOrDefault(PetReport => PetReport.ID == id);
            if (currentPetReport is null)
                throw new KeyNotFoundException($"Given id '{id}' was not found in database");

            _context.PetReports.Remove(currentPetReport);
        }

    }
}
