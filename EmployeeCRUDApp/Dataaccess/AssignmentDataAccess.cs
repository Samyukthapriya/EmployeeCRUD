
using EmployeeCRUDApp.Helpers;
using EmployeeCRUDApp.Models;
using System.Data.SqlClient;

namespace EmployeeCRUDApp.Dataaccess
{
    internal class AssignmentDataAccess
    {
        public string ErrorMessage { get; set; }
        public List<AssignmentDataModel> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<AssignmentDataModel> assignments = new List<AssignmentDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "SELECT AG.a_id AS a_id,E.Id AS e_id,P.p_id AS p_id,AG.startdate,AG.enddate " +

                        "FROM[dbo].[Assignment] AS AG " +




                               "INNER JOIN[dbo].Employee1 AS E ON E.Id = AG.e_id " +
                               "INNER JOIN[dbo].Project AS P ON P.p_id = AG.p_id " +
                                "ORDER BY a_id ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AssignmentDataModel assignment = new AssignmentDataModel();
                                assignment.a_id = reader.GetInt32(0);
                              
                                assignment.e_id = reader.GetInt32(1);
                                assignment.p_id = reader.GetInt32(2);
                                assignment.startdate = reader.GetDateTime(3);
                                assignment.enddate = reader.GetDateTime(4);


                                assignments.Add(assignment);
                            }
                        }
                    }
                }
                return assignments;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

        //Get Project by Id
        public AssignmentDataModel GetAssignmentById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                AssignmentDataModel assignment = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select a_id,p_id,e_id,startdate,enddate from Assignment where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                assignment = new AssignmentDataModel();
                                assignment.a_id = reader.GetInt32(0);
                                assignment.p_id = reader.GetInt32(1);
                                assignment.e_id = reader.GetInt32(2);
                                assignment.startdate = reader.GetDateTime(3);
                                assignment.enddate = reader.GetDateTime(4);


                            }
                        }
                    }
                }
                return assignment;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }

        public List<AssignmentDataModel> GetAssignmentsByName(string name)
        {
            try
            {
                List<AssignmentDataModel> assignments = new List<AssignmentDataModel>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select a_id,p_id,e_id,startdate,enddate from Assignment where Name like '%{name}%' ";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                AssignmentDataModel assignment = new AssignmentDataModel();
                                assignment.a_id = reader.GetInt32(0);
                                assignment.p_id = reader.GetInt32(1);
                                assignment.e_id = reader.GetInt32(2);
                                assignment.startdate = reader.GetDateTime(3);
                                assignment.enddate = reader.GetDateTime(4);


                                assignments.Add(assignment);
                            }
                        }
                    }
                }

                return assignments;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        public AssignmentDataModel Insert(AssignmentDataModel newAssignment)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = $"INSERT INTO dbo.Assignment(p_id,e_id,startdate,enddate) VALUES('{newAssignment.p_id}','{newAssignment.e_id}','{newAssignment.startdate.ToString("yyyy-MM-dd")}','{newAssignment.enddate.ToString("yyyy-MM-dd")}');SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newAssignment.AssignmentId = idInserted;
                            return newAssignment;
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
        public AssignmentDataModel Update(AssignmentDataModel updAssignment)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Assignment SET p_id='{updAssignment.p_id}'," +
                        $"e_id = '{updAssignment.e_id}'," +
                       

                        $"startdate= '{updAssignment.startdate}'," +
                        $"enddate= '{updAssignment.enddate}'," +
                        $"where p_id = '{updAssignment.p_id}'";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updAssignment;
                        }
                    }
                }
                return updAssignment;

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
    }
}