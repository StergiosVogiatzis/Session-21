using PetShop.Model;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Web.Models
{
    public class PetFoodViewModel
    {
        public AnimalType AnimalType { get; set; }
        [Range(0, 9999999)]
        public decimal Cost { get; set; }
        [Range(0, 9999999)]
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
