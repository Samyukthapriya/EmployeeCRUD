using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUDApp.Pages.Projects
{

    public class AddModel : PageModel
    {
        
        [BindProperty]
        [Display(Name = "p_name")]
        [Required]
        public string p_name { get; set; }
        //gender
        [BindProperty]
        [Display(Name = "startdate")]
        [Required]
        public DateTime startdate { get; set; }





        [BindProperty]
        [Display(Name = "enddate")]
        [Required]
        public DateTime enddate { get; set; }


        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        private List<SelectListItem> GetProjects()
        {
            var projectDataAccess = new ProjectDataAccess();
            var projectList = projectDataAccess.GetAll();

            var projectSelectList = new List<SelectListItem>();
            foreach (var project in projectList)
            {
                projectSelectList.Add(new SelectListItem
                {
                    Text = $"{project.p_id}-{project.p_name}",
                    Value = project.p_id.ToString(),
                });
            }
            return projectSelectList;
        }



        public void OnGet()
        {
           
            p_name = "";
            startdate =DateTime.Now;
            enddate =DateTime.Now;




        }
        public void OnPost()
        {

            //Rooms = GetRooms();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Add failed.Please try Again";
                return;
            }
            var projectDataAccess = new ProjectDataAccess();
            var newproject = new ProjectDataModel
            {
                
                p_name = p_name,
                startdate = startdate,
                enddate = enddate,
            };
            var insertedProject = projectDataAccess.Insert(newproject);
            if (insertedProject != null)
            {
                SuccessMessage = $"successfully Inserted Project {insertedProject.p_id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error!! Add failed.Please try Again - {projectDataAccess.ErrorMessage}";
            }


        }
    }
}

