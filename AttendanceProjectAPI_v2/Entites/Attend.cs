using System;
using System.Collections.Generic;

namespace AttendanceProjectAPI_v2.Entites;

public partial class Attend
{
    public string StudentUuid { get; set; } = null!;

    public uint ClassId { get; set; }

    public DateTime AttendanceDate { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual Student StudentUu { get; set; } = null!;
}
