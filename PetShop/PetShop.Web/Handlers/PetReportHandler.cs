using Microsoft.EntityFrameworkCore;
using PetShop.EF.Context;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Web.Handlers
{
    public class PetReportHandler
    {
        private readonly PetShopContext _context;
 
        public PetReportHandler(PetShopContext context)
        {
            _context = context; 
        }

        public int TotalSold(PetReport petReport)
        {
            int year = int.Parse(petReport.Year);
            int month = int.Parse(petReport.Month);
            return _context.Transactions.Include(transaction => transaction.Pet).Where(t => t.Date.Year == year && t.Date.Month == month && t.Pet.AnimalType == petReport.AnimalType).Count();
        }

        public async Task<bool> PetReportExists(PetReport petReport )
        {
            return await _context.PetReports.FirstOrDefaultAsync(petR => petR.Year == petReport.Year && petR.Month == petReport.Month && petR.AnimalType == petReport.AnimalType) is not null;
        }
    }
}
