using System;
using System.Collections.Generic;

namespace Attendance_API_v3.AttendanceModels;

public partial class UserLevel
{
    public uint UserId { get; set; }

    public string Level { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
