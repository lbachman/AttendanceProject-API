namespace AttendanceAPI.Models
{
    public class Class
    {
        int Class_Id  { get; set; }
        string Semester_Code { get; set; }
        string Room { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Days { get; set; }
        
    }
}
