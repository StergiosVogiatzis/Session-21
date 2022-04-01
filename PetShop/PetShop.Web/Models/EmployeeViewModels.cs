using PetShop.Model;

namespace PetShop.Web.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }

    public class EmployeeCreateViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }

    public class EmployeeUpdateViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
    public class EmployeeDeleteViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
