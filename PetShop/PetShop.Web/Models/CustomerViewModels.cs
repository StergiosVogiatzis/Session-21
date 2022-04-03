using System.ComponentModel.DataAnnotations;
namespace PetShop.Web.Models
{
    public class CustomerViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(10)]
        public string TIN { get; set; }
    }

    public class CustomerCreateViewModel : CustomerViewModel
    {
    }

    public class CustomerUpdateViewModel : CustomerViewModel
    {
        public Guid ID { get; set; }
    } 
    public class CustomerDeleteViewModel : CustomerViewModel
    {
        public Guid ID { get; set; }
    } 
 }
