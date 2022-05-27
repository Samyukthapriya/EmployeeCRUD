using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using EmployeeCRUDApp.DataAccess;
using EmployeeCRUDApp.Models;

namespace EmployeeCRUDApp.Pages
{
    public class IndexModel : PageModel
    {
        public int Employeecount { get; set; }
        public int Projectcount { get; set; }
        public int Assignmentcount { get; set; }

        public int CompletedAssignmentcount { get; set; }

        public string ErrorMessage { get; set; }

        [FromQuery(Name = "action")]
        public string Action { get; set; }
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Employeecount = 0;
            Projectcount = 0;
            Assignmentcount = 0;
            ErrorMessage = "";

        }
        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
            {
                Logout();
                return;
            }

            var dashBoardData = new DashboardDataAccess();
            var dashboard = dashBoardData.GetAll();
            if (dashboard != null)
            {
                Employeecount = dashboard.Employeecount;
                Projectcount = dashboard.Projectcount;
                Assignmentcount = dashboard.Assignmentcount;

            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashBoardData.ErrorMessage}";
            }
        }
        public void OnPost()
        {
            Logout();
        }
        private void Logout()
        {
            HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }
    }
}

