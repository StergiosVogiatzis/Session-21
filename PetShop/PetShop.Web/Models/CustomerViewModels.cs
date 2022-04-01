namespace PetShop.Web.Models
{
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    }

    public class CustomerCreateViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    }

    public class CustomerUpdateViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    } 
    public class CustomerDeleteViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    } 
 }
