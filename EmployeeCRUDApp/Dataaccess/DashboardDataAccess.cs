using System.Data.SqlClient;
using EmployeeCRUDApp.Helpers;
using EmployeeCRUDApp.Models;

namespace EmployeeCRUDApp.DataAccess
{
    public class DashboardDataAccess
    {
        public string ErrorMessage { get; private set; }
        public DashboardDataModel GetAll()
        {
            try
            {

                ErrorMessage = String.Empty;
                ErrorMessage = "";
                var d = new DashboardDataModel();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "select count(*) as Employeecount from Employee1";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.Employeecount = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    sqlStmt = "select count(*) as Projectcount from Project";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.Projectcount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    sqlStmt = "select count(*) as Assignmentcount from Assignment";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        d.Assignmentcount = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                   

                }

                return d;


            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

    }
}