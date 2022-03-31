namespace PetShop.Model
{
    public class Transaction : BaseEntity
    {
        
        public DateTime Date { get; set; }
        public Guid PetID { get; set; }
        public Pet Pet { get; set; }
        public Guid CustomerID { get; set; }
        public Customer Customer { get; set; }
        public Guid EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public Guid PetFoodID { get; set; }
        public PetFood PetFood { get; set; }
        public decimal PetPrice { get; set; }
        public int PetFoodQty { get; set;}
        public decimal PetFoodPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public Transaction()
        {
            Date = DateTime.Now;
        }
    }
}
