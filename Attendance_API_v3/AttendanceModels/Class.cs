using System;
using System.Collections.Generic;

namespace Attendance_API_v3.AttendanceModels;

public partial class Class
{
    public uint ClassId { get; set; }

    public string SemesterCode { get; set; } = null!;

    public string Room { get; set; } = null!;

    public TimeSpan StartTime { get; set; }

    public TimeSpan EndTime { get; set; }

    public string Days { get; set; } = null!;

    public uint? InstructorId { get; set; }

    public virtual ICollection<Attend> Attends { get; set; } = new List<Attend>();

    public virtual User? Instructor { get; set; }

    public virtual ICollection<Student> StudentUus { get; set; } = new List<Student>();
}
