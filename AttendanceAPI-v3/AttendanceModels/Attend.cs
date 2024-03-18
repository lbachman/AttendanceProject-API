using System;
using System.Collections.Generic;

namespace AttendanceAPI_v3.AttendanceModels;

public partial class Attend
{
    public string StudentUuid { get; set; } = null!;

    public uint ClassId { get; set; }

    public DateTime AttendanceDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student StudentUu { get; set; } = null!;
}
