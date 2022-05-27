using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUDApp.Pages.Projects
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        [Display(Name = "p_id")]
        [Required]
        [MinLength(3)]
        public int p_id { get; set; }

        [BindProperty]
        [Display(Name = "p_name")]
        [Required]
        [MinLength(3)]
        public string p_name{ get; set; }

        [BindProperty]
        [Display(Name = "startdate")]
        [Required]
        [MinLength(3)]
        public DateTime startdate{ get; set; }

        [BindProperty]
        [Display(Name = "enddate")]
        [Required]
        [MinLength(3)]
        public DateTime enddate { get; set; }
       
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
            var projectData = new ProjectDataAccess();
            var project = projectData.GetProjectById(id);
            if (project != null)
            {
                p_id = project.p_id;
                p_name = project.p_name;
                startdate = project.startdate;
                enddate = project.enddate;
               
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
            var projectData = new ProjectDataAccess();
            var projectToUpdate = new Project { Id = Id, p_name = p_name, startdate = startdate, enddate= enddate };
            var updatedProject = projectData.Update(projectToUpdate);

            //check result
            if (updatedProject != null)
            {
                SuccessMessage = $"Projects updated Successfully";

            }
            else
            {
                ErrorMessage = $"Error..!!!  Updating Project.";
            }
        }
    }
}