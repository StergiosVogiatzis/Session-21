namespace PetShop.Model
{
    public class Pet : BaseEntity
    {
        public string Breed { get; set; }
        public AnimalType AnimalType { get; set; }
        public PetStatus PetStatus { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public Guid TransactionID { get; set; }
        public Transaction Transaction { get; set; }
        public Pet()
        {

        }
    }
}
