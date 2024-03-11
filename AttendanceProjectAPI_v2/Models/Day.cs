using System;
using System.Collections.Generic;

namespace AttendanceProjectAPI_v2.Models;

public partial class Day
{
    public uint ClassId { get; set; }

    public string Day1 { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;
}
