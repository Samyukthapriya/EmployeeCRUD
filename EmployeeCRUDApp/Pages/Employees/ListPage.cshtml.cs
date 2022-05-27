using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeCRUDApp.Pages.Employees
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public List<Employee> Employees { get; set; }
        public void OnGet()
        {
            var employeeData = new EmployeeData();
            Employees = employeeData.GetAll();
        }
        public ListModel()
        {
            SearchText = "";
            SuccessMessage = "";
            ErrorMessage = "";
            Employees = new List<Employee>();

        }
        public void OnPostSearch()
        {

            if (!ModelState.IsValid)
            {
                ErrorMessage = $"Enter the Valid Data";
                return;
            }
            if (string.IsNullOrEmpty(SearchText))
            {
                ErrorMessage = $"Please input more than 1 character";
                return;
            }
            var employeeData = new EmployeeData();
            Employees = employeeData.GetDepartmentsByName(SearchText, SearchText);
            if (Employees != null && (Employees.Count > 0))
            {
                SuccessMessage = $"{Employees.Count()}  Employees with '{SearchText}' found";

            }
            else
            {
                ErrorMessage = $"Records with '{SearchText}' Not Found";

            }

        }
        public void OnPostClear()
        {
            SearchText = "";
            ModelState.Clear();
            var employeeData = new Dataaccess.EmployeeData();
            Employees = employeeData.GetAll();
            OnGet();
        }
    }
}