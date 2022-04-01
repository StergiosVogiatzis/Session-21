using PetShop.Model;

namespace PetShop.Web.Models
{
    public class PetFoodViewModel
    {
        public AnimalType AnimalType { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }

    public class PetFoodCreateViewModel : PetFoodViewModel
    {
    }

    public class PetFoodUpdateViewModel : PetFoodViewModel
    {
        public Guid ID { get; set; }
    }
    public class PetFoodDeleteViewModel : PetFoodViewModel
    {
        public Guid ID { get; set; }
    }
}
