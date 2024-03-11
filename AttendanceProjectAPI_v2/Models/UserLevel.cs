using System;
using System.Collections.Generic;

namespace AttendanceProjectAPI_v2.Models;

public partial class UserLevel
{
    public uint UserId { get; set; }

    public string Level { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
