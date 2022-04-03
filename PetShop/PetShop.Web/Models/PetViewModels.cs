using PetShop.Model;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Web.Models
{
    public class PetViewModel
    {
        [MaxLength(20)]
        public string Breed { get; set; }
        public AnimalType AnimalType { get; set; }
        public PetStatus PetStatus { get; set; }
        [Range(0, 9999999)]
        public decimal Cost { get; set; }
        [Range(0, 9999999)]
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
