using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;

namespace EmployeeCRUDApp.Pages.Assignments
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<AssignmentDataModel> Assignments { get; set; }
        public void OnGet()
        {
            var assignmentDataAccess = new AssignmentDataAccess();
            Assignments = assignmentDataAccess.GetAll();

        }
        public ListModel()
        {
            Assignments = new List<AssignmentDataModel>();
            ErrorMessage = "";
            SuccessMessage = "";
            SearchText = "";
        }
        public void OnPostSearch()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Enter the Valid Data";
            }
            if (string.IsNullOrEmpty(SearchText))
            {
                ErrorMessage = $"Please input more than 1 character";


            }
            Dataaccess.AssignmentDataAccess assignmentDataAccess = new Dataaccess.AssignmentDataAccess();
            Assignments = assignmentDataAccess.GetAssignmentsByName(SearchText);
            if (Assignments != null && (Assignments.Count > 0))
            {
                SuccessMessage = $"{Assignments.Count()} Assignments with '{SearchText}' found";

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
            var assignmentDataAccess = new Dataaccess.AssignmentDataAccess();
            Assignments = assignmentDataAccess.GetAll();
            OnGet();
        }
    }
}


