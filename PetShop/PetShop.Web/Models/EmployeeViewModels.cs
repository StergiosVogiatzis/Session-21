using PetShop.Model;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Web.Models
{
    public class EmployeeViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [Range(0, 9999999)]
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }

    public class EmployeeCreateViewModel : EmployeeViewModel
    {
    }

    public class EmployeeUpdateViewModel : EmployeeViewModel
    {
        public Guid ID { get; set; }
    }
    public class EmployeeDeleteViewModel : EmployeeViewModel
    {
        public Guid ID { get; set; }
    }
}
