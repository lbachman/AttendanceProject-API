using System;
using System.Collections.Generic;

namespace AttendanceProjectAPI_v2.Entites;

public partial class Student
{
    public string StudentUuid { get; set; } = null!;

    public string StudentUserName { get; set; } = null!;

    public virtual ICollection<Attend> Attends { get; set; } = new List<Attend>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
