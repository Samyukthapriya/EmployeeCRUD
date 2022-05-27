using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace EmployeeCRUDApp.Helpers
{
    public class DataBase
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Data Source=ENWIN-539\\SQLEXPRESS;Initial Catalog=TrainingDB;Integrated Security=True;";
            return new SqlConnection(connectionString);
        }
    }
}