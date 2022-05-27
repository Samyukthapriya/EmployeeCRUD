
using EmployeeCRUDApp.Helpers;
using EmployeeCRUDApp.Models;
using EmployeeCRUDApp.Pages.Projects;
using System.Data.SqlClient;

namespace EmployeeCRUDApp.Dataaccess
{
    internal class ProjectDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<ProjectDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<ProjectDataModel> projects = new List<ProjectDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select p_id,p_name,startdate,enddate from Project";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProjectDataModel project = new ProjectDataModel();
                                project.p_id = reader.GetInt32(0);
                                project.p_name = reader.GetString(1);
                                project.startdate =reader.GetDateTime(2);
                                project.enddate = reader.GetDateTime(3);
                               

                                projects.Add(project);
                            }
                        }
                    }
                }
                return projects;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Project by Id
        public ProjectDataModel GetProjectById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                ProjectDataModel project = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select p_id,p_name,startdate,enddate from Project where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                project = new ProjectDataModel();
                                project.p_id= reader.GetInt32(0);
                                project.p_name= reader.GetString(1);
                                project.startdate = reader.GetDateTime(2);
                                project.enddate = reader.GetDateTime(3);
                               

                            }
                        }
                    }
                }
                return project;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

        internal object Update(Project projectToUpdate)
        {
            throw new NotImplementedException();
        }

        public List<ProjectDataModel> GetProjectsByName(string name)
        {
            try
            {
                List<ProjectDataModel> projects = new List<ProjectDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select p_id,p_name,startdate,enddate from Project where Name like '%{name}%' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                ProjectDataModel project = new ProjectDataModel();
                                project.p_id = reader.GetInt32(0);
                                project.p_name = reader.GetString(1);
                                project.startdate = reader.GetDateTime(2);
                                project.enddate= reader.GetDateTime(3);
                                

                                projects.Add(project);
                            }
                        }
                    }
                }

                return projects;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public ProjectDataModel Insert(ProjectDataModel newProject)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Project(p_name,startdate,enddate) VALUES('{newProject.p_name}','{newProject.startdate.ToString("yyyy-MM-dd")}','{newProject.enddate.ToString("yyyy-MM-dd")}');SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newProject.p_id = idInserted;
                            return newProject;
                        }
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public ProjectDataModel Update(ProjectDataModel updProject)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Project SET p_name='{updProject.p_name}',"+
                       
                        $"startdate= '{updProject.startdate}'," +
                        $"enddate= '{updProject.enddate}'," +
                        $"where p_id = '{updProject.p_id}'";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updProject;
                        }
                    }
                }
                return updProject;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}