using System;
using System.Collections.Generic;

namespace AttendanceAPI_v3.AttendanceModels;

public partial class Day
{
    public uint ClassId { get; set; }

    public string Day1 { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;
}
