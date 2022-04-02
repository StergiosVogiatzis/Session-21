using PetShop.Model;

namespace PetShop.Web.Models
{
    public class MonthlyLedgerViewModel
    {
        public string Month { get; set; }

        public string Year { get; set; }

        public decimal Income { get; set; } 
        public decimal Expenses { get; set; }

        public decimal Total { get; set; }

    }

    public class MonthlyLedgerCreateViewModel :MonthlyLedgerViewModel
    { 
    }
}
