using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace EmployeeCRUDApp.Pages.Employees
{
    public class AddModel : PageModel
    {

        [BindProperty]
        [Display(Name = "Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [BindProperty]
        [Required]
        public string Gender { get; set; }
        public string[] Genders = new[] { "M", "F", "U" };

        [BindProperty]
        [Display(Name = "DOB")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [BindProperty]
        [Display(Name = "Phonenumber")]
        [Required]

        public string Phonenumber { get; set; }

        
        [BindProperty]
        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }
        [BindProperty]
        [Display(Name = "City")]
        [Required]
        public string City { get; set; }

        [BindProperty]
        [Display(Name = "Pincode")]


        public string Pincode { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        private List<SelectListItem> GetLocation()
        {
            var SelectItem = new List<SelectListItem>();
            SelectItem.Add(new SelectListItem { Text = "Bengaluru", Value = "Bengaluru" });
            SelectItem.Add(new SelectListItem { Text = "Coimabatore", Value = "Coimbatore" });
            SelectItem.Add(new SelectListItem { Text = "Chennai", Value = "Chennai" });
            return SelectItem;
        }

        public void OnGet()
        {
            Name = "";
            Gender = "";
            Dob = DateTime.Now.AddYears(-20);
            Phonenumber = "";
          
            Pincode = "";
            Address = "";
            City = "";
            //SuccessMessage = "";
            //ErrorMessage = "";
            ModelState.Clear();
        }
        public AddModel()
        {
            SuccessMessage = "";
            ErrorMessage = "";
            
            Dob = DateTime.Now.AddYears(-10);
            ModelState.Clear();
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Add failed.Please try Again";
                return;
            }
            var employeeData = new EmployeeData();
            var newEmployee = new Employee { Name = Name, Gender = Gender, Dob = Dob,Phonenumber = Phonenumber, Pincode = Pincode,Address=Address,City=City };
            //if(!newEmployee.IsValid())
            //{
            // ErrorMessage = "Error!! Please check data try Again";
            //return;
            //}

            var insertedEmployee = employeeData.Insert(newEmployee);
            if (insertedEmployee != null)
            {
                SuccessMessage = $"successfully Inserted Employee {insertedEmployee.Id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error!! Add failed.Please try Again- {employeeData.ErrorMessage}";
            }
        }
    }
}