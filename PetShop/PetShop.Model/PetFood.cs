namespace PetShop.Model
{
    public class PetFood : BaseEntity
    {
        public AnimalType AnimalType { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public List<Transaction> Transactions { get; set; }
        public PetFood()
        {

        }
    }
}
