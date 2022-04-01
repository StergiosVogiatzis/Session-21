using PetShop.Model;

namespace PetShop.Web.Models
{
    public class PetViewModel
    {
        public string Breed { get; set; }
        public AnimalType AnimalType { get; set; }
        public PetStatus PetStatus { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }

    public class PetCreateViewModel : PetViewModel
    {
    }

    public class PetUpdateViewModel : PetViewModel
    {
        public Guid ID { get; set; }
    }
    public class PetDeleteViewModel : PetViewModel
    {
        public Guid ID { get; set; }
    }
}
