using PetShop.Model;

namespace PetShop.Web.Models
{
    public class PetReportViewModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public AnimalType AnimalType { get; set; }
        public int TotalSold { get; set; }
    }
    public class PetReportCreateViewModel 
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public AnimalType AnimalType { get; set; }
    }

    public class PetReportDeleteViewModel : PetReportViewModel
    {
        public Guid ID { get; set; }
    }

}
