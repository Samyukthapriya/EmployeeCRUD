namespace EmployeeCRUDApp.Models
{
    public class ProjectDataModel
    {
        public int p_id { get; set; }
        public string p_name { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }




        //constructor
        public ProjectDataModel()
        {
            p_id = 0;
            p_name = "";
            startdate = DateTime.Now;
            enddate = DateTime.Now;




        }

      
    }
}


