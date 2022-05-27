using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

using EmployeeCRUDApp.Models;
using EmployeeCRUDApp.Helpers;

namespace EmployeeCRUDApp.Dataaccess
{
    public class EmployeeData
    {
        public string ErrorMessage { get; private set; }
        //GetAll
        public List<Employee> GetAll()
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                List<Employee> employees = new List<Employee>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    var sqlStmt = "Select Id,Name,Gender,Phonenumber,Dob,Pincode,Address,City from dbo.Employee1";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader Reader = cmd.ExecuteReader())
                        {
                            while (Reader.Read() == true)
                            {
                                Employee emp = new Employee();
                                emp.Id = Reader.GetInt32(0);
                                emp.Name = Reader.GetString(1);
                                emp.Gender = Reader.GetString(2);
                                emp.Phonenumber = Reader.GetString(3);
                                emp.Dob = Reader.GetDateTime(4);
                                
                                emp.Pincode = Reader.GetString(5);
                              
                                emp.Address = Reader.GetString(6);
                                emp.City = Reader.GetString(7);

                                employees.Add(emp);
                            }
                        }
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        //Get by ID
        public Employee GetEmployeeById(int id)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                Employee emp = null;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id,Name,Gender,Dob,Phonenumber,Pincode,Address,City from dbo.Employee1 where Id={id}";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() == true)
                            {
                                emp = new Employee();
                                emp.Id = reader.GetInt32(0);
                                emp.Name = reader.GetString(1);
                                emp.Gender = reader.GetString(2);
                                emp.Dob = reader.GetDateTime(3);
                                emp.Phonenumber = reader.GetString(4);
                              
                                emp.Pincode = reader.GetString(5);
                             
                                emp.Address = reader.GetString(6);
                                emp.City = reader.GetString(7);
                            }
                        }
                    }
                }
                return emp;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        //Get Department By Name,Address
        public List<Employee> GetDepartmentsByName(string name, string address)
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"Select Id, Name,Gender,Dob,Phonenumber,Pincode,Address,City from Employee1 where Name like '%{name}%' AND Location like '%{address}%'";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read() == true)
                            {
                                Employee emp = new Employee();
                                emp.Id = reader.GetInt32(0);
                                emp.Name = reader.GetString(1);
                                emp.Gender = reader.GetString(2);
                                emp.Dob = reader.GetDateTime(3);
                                emp.Phonenumber = reader.GetString(4);
                                emp.Pincode = reader.GetString(5);

                              
                                emp.Address = reader.GetString(6);

                                emp.City = reader.GetString(7);

                                employees.Add(emp);
                            }
                        }
                    }
                }

                return employees;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }
        //INSERT
        public Employee Insert(Employee newEmployee)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"INSERT INTO dbo.Employee1(Name,Gender,Dob,Phonenumber,Pincode,Address,City) VALUES('{newEmployee.Name}','{newEmployee.Gender}','{newEmployee.Dob.ToString("yyyy-MM-dd")}','{newEmployee.Phonenumber}','{newEmployee.Pincode}','{newEmployee.Address}','{newEmployee.City}');SELECT SCOPE_IDENTITY();";
                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int idInserted = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idInserted > 0)
                        {
                            newEmployee.Id = idInserted;
                            return newEmployee;
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
        //Update employee
        public Employee Update(Employee updEmployee)
        {
            try
            {
                ErrorMessage = String.Empty;
                ErrorMessage = "";
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"UPDATE dbo.Employee1 SET Name = '{updEmployee.Name}', " +
                        $"Gender= '{updEmployee.Gender}'," +

                        $"Phonenumber= '{updEmployee.Phonenumber}'," +
                        $"Dob='{updEmployee.Dob.ToString("yyyy-MM-dd")}'," +
                      
                        $"Pincode= '{updEmployee.Pincode}'" +
                        $"Address= '{updEmployee.Address}'," +
                        $"City= '{updEmployee.City}'" +
                        $"where Id = '{updEmployee.Id}'";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        int numOfRows = cmd.ExecuteNonQuery();
                        if (numOfRows > 0)
                        {
                            return updEmployee;
                        }
                    }
                }
                return updEmployee;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }

        }
        //Delete Employee
        public int Delete(int id)
        {
            try
            {
                ErrorMessage = string.Empty;
                ErrorMessage = "";
                int numOfRows = 0;
                using (SqlConnection conn = DataBase.GetConnection())
                {
                    conn.Open();
                    string sqlStmt = $"DELETE FROM Employee1 Where Id = {id}";

                    using (SqlCommand cmd = new SqlCommand(sqlStmt, conn))
                    {
                        numOfRows = cmd.ExecuteNonQuery();

                    }
                }
                return numOfRows;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return 0;
            }
        }


    }
}