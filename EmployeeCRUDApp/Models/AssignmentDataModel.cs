namespace EmployeeCRUDApp.Models
{
    public class AssignmentDataModel
    {
        public int a_id { get; set; }
        public int p_id { get; set; }
        public int e_id { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int AssignmentId { get;  set; }




        //constructor
        public AssignmentDataModel()
        {
            a_id = 0;
            p_id= 0;
            e_id = 0;
            startdate = DateTime.Now;
            enddate = DateTime.Now;




        }
    }


}