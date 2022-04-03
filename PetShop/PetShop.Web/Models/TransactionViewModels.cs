using PetShop.Model;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Web.Models
{
    public class TransactionViewModel
    {
        public DateTime Date { get; set; }
        public Guid PetID { get; set; }
        public Guid CustomerID { get; set; }
        public Guid EmployeeID { get; set; }
        //public Guid PetFoodID { get; set; }
        [Range(1,Int32.MaxValue)]
        public int PetFoodQty { get; set; }

    }

    public class TransactionCreateViewModel : TransactionViewModel
    {

    }
    public class TransactionDeleteViewModel : TransactionViewModel
    {
        public Guid ID { get; set; }
    }
}
