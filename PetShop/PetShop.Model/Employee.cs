﻿namespace PetShop.Model
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Employee()
        {

        }
    }
}
