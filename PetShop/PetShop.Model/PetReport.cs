namespace PetShop.Model
{
    public class PetReport : BaseEntity
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public AnimalType AnimalType { get; set; }
        public int TotalSold { get; set; }
        public PetReport()
        {
            
        }
    }
}
