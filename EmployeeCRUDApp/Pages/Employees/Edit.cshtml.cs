using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUDApp.Pages.Employees
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty]
        [Display(Name = "Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [BindProperty]
        [Display(Name = "Gender")]
        [Required]
        [MinLength(3)]
        public string Gender { get; set; }

        [BindProperty]
        [Display(Name = "Dob")]
        [Required]
        [MinLength(3)]
        public DateTime Dob { get; set; }

        [BindProperty]
        [Display(Name = "Phonenumber")]
        [Required]
        [MinLength(3)]
        public string Phonenumber { get; set; }
        [BindProperty]
        [Display(Name = "Pincode")]
        [Required]
        [MinLength(3)]
        public string Pincode { get; set; }

        [BindProperty]
        [Display(Name = "Address")]
        [Required]
        [MinLength(3)]
        public string Address { get; set; }

        [BindProperty]
        [Display(Name = "City")]
        [Required]
        [MinLength(3)]
        public string City { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }
            var employeeData = new EmployeeData();
            var emp = employeeData.GetEmployeeById(id);
            if (emp != null)
            {
                Name = emp.Name;
                Gender = emp.Gender;
                Dob = emp.Dob;
                Phonenumber = emp.Phonenumber;
                Pincode = emp.Pincode;
                Address = emp.Address;
                City = emp.City;
            }
            else
            {

                ErrorMessage = "No Records found with Id";
            }
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data. Please Try Again";
                return;
            }
            //update
            var employeeData = new EmployeeData();
            var empToUpdate = new Employee { Id = Id, Name = Name, Gender = Gender, Dob = Dob, Phonenumber = Phonenumber, Pincode = Pincode, Address = Address, City = City };
            var updatedEmployee = employeeData.Update(empToUpdate);

            //check result
            if (updatedEmployee != null)
            {
                SuccessMessage = $"Employees{updatedEmployee.Id} updated Successfully";

            }
            else
            {
                ErrorMessage = $"Error..!!!  Updating Employee.";
            }
        }
    }
}