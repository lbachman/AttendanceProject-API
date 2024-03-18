using System;
using System.Collections.Generic;

namespace Attendance_API_v3.AttendanceModels;

public partial class Student
{
    public string StudentUuid { get; set; } = null!;

    public string StudentUserName { get; set; } = null!;

    public virtual ICollection<Attend> Attends { get; set; } = new List<Attend>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
