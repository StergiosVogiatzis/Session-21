using PetShop.EF.Context;
using PetShop.Model;

namespace PetShop.Web.Handlers
{
    public class PetReportHandler
    {
        private readonly PetShopContext _context;
        private readonly PetReport _petReport;

        public PetReportHandler(PetShopContext context, PetReport petReport)
        {
            _context = context;
            _petReport = petReport;
        }

        public int TotalSold()
        {
            int year = Int32.Parse(_petReport.Year);
            int month = Int32.Parse(_petReport.Month);
            return _context.Transactions.Where(t => t.Date.Year == year && t.Date.Month == month && t.Pet.AnimalType == _petReport.AnimalType).Count();
        }
    }
}
