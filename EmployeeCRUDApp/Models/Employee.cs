
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmployeeCRUDApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }


        public string Phonenumber { get; set; }
        public DateTime Dob { get; set; }
        
        public string Pincode { get; set; }
             public string Address { get; set; }
        public string City { get; set; }
        //constructor
        public Employee()
        {
            Id = 0;
            Name = "";
            Gender = "";

            Phonenumber = "";
            Dob = DateTime.Now;
           
            Pincode = "";
            Address = "";
            City = "";
        }

        public bool IsValid()
        {
            if (Id <= 0)
            {
                return false;
            }
            if (Name == null || Name.Trim() == "" || Name.Trim().Length > 20)
            {
                return false;
            }
            if (Gender == null || Gender.Trim() == "" || Gender.Trim().Length > 10)
            {
                return false;
            }

            if (Phonenumber == null )
            {
                return false;
            }
            if (Dob == null)
            {
                return false;
            }
           
            if (Address == null)
            {
                return false;
            }
            if (City == null)
            {
                return false;
            }
            if (Pincode.Trim() == "" || Pincode.Trim().Length != 6)
            {
                return false;
            }
            return true;
        }
    }
}

