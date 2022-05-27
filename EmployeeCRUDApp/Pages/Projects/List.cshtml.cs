using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;

namespace EmployeeCRUDApp.Pages.Projects
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public List<ProjectDataModel> Projects { get; set; }
        public void OnGet()
        {
            var projectDataAccess = new ProjectDataAccess();
            Projects = projectDataAccess.GetAll();

        }
        public ListModel()
        {
            Projects = new List<ProjectDataModel>();
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
            Dataaccess.ProjectDataAccess projectDataAccess = new Dataaccess.ProjectDataAccess();
            Projects = projectDataAccess.GetProjectsByName(SearchText);
            if (Projects != null && (Projects.Count > 0))
            {
                SuccessMessage = $"{Projects.Count()} Projects with '{SearchText}' found";

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
            var projectDataAccess = new Dataaccess.ProjectDataAccess();
            Projects = projectDataAccess.GetAll();
            OnGet();
        }
    }
}
    

