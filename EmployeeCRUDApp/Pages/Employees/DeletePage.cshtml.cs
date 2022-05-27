using EmployeeCRUDApp.Dataaccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeCRUDApp.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public bool ShowButton { get; set; }

        public string Name { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteModel()
        {
            Name = "";
            SuccessMessage = "";
            ErrorMessage = "";
            ShowButton = true;
        }
        public void OnGet(int id)
        {
            Id = Id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }
            var employeeData = new EmployeeData();
            var dept = employeeData.GetEmployeeById(id);
            if (dept != null)
            {
                Name = dept.Name;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }


        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }

            var employeeData = new EmployeeData();
            var numOfRows = employeeData.Delete(Id);
            if (numOfRows > 0)
            {
                SuccessMessage = $"Employee {Id} deleted successfully!";
                ShowButton = false;
            }
            else
            {
                ErrorMessage = $"Error! Unable to delete Employee Details {Id}";
            }
        }

    }
}