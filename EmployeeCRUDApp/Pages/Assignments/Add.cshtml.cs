using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmployeeCRUDApp.Dataaccess;
using EmployeeCRUDApp.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCRUDApp.Pages.Assignments
{

    public class AddModel : PageModel
    {
        public int p_id;

        public List<SelectListItem>EmployeeList { get; set; }
        [BindProperty]
        [Display(Name = "e_id")]
        [Required]
        public int SelectEmployeeId { get; set; }
        public List<SelectListItem> ProjectList { get; set; }
        [BindProperty]
        [Display(Name = "p_id")]
        [Required]
        public int SelectProjectId { get; set; }
        public int e_id { get; set; }
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

        private List<SelectListItem> GetAssignments()
        {
            var assignmentDataAccess = new AssignmentDataAccess();
            var assignmentList = assignmentDataAccess.GetAll();

            var assignmentSelectList = new List<SelectListItem>();
            foreach (var assignment in assignmentList)
            {
                assignmentSelectList.Add(new SelectListItem
                {
                    Text = $"{assignment.p_id}-{assignment.e_id}",
                    Value = assignment.p_id.ToString(),
                });
            }
            return assignmentSelectList;
        }



        public  AddModel()
        {

           
            EmployeeList = GetEmployees();
            ProjectList = GetProjects();
            startdate = DateTime.Now;
            enddate = DateTime.Now;
         }
        public void OnGet()
        {

        }
        private List<SelectListItem> GetEmployees()
        {
            //Get Data from Data Access
            var EmployeeData = new EmployeeData();
            var employeeList = EmployeeData.GetAll();

            //Create SelectListItem
            var employeeSelectList = new List<SelectListItem>();
            foreach (var employee in employeeList)
            {
                employeeSelectList.Add(new SelectListItem
                {
                    Text = $"{employee.Id} - {employee.Phonenumber}",
                    Value = employee.Id.ToString(),
                });
            }
            return employeeSelectList;
        }
                private List<SelectListItem> GetProjects()
                {
                    var ProjectData = new ProjectDataAccess();
                var projectList = ProjectData.GetAll();

                //Create SelectListItem
                var projectSelectList = new List<SelectListItem>();
                foreach (var Project in projectList)
                {
                    projectSelectList.Add(new SelectListItem
                    {
                        Text = $"{Project.p_id} - {Project.p_name}",
                        Value = Project.p_id.ToString(),
                    });
                }
            return projectSelectList;
        }



        public void OnPost()
        {
            EmployeeList = GetEmployees();
            ProjectList = GetProjects();
            //Rooms = GetRooms();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Error!! Add failed.Please try Again";
                return;
            }
            
            var assignmentDataAccess = new AssignmentDataAccess();
            var newassignment = new AssignmentDataModel
            {

                p_id = SelectProjectId,
                e_id = SelectEmployeeId,
                startdate = startdate,
                enddate = enddate,
            };
            var insertedAssignment = assignmentDataAccess.Insert(newassignment);
            if (insertedAssignment != null)
            {
                SuccessMessage = $"successfully Inserted Assignment {insertedAssignment.a_id}";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = $"Error!! Add failed.Please try Again - {assignmentDataAccess.ErrorMessage}";
            }


        }
    }
}

