using System;
using System.Collections.Generic;

namespace AttendanceProjectAPI_v2.Models;

public partial class User
{
    public uint UserId { get; set; }

    public string UserName { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual UserLevel? UserLevel { get; set; }
}
