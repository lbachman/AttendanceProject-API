using System;
using System.Collections.Generic;

namespace AttendanceAPI_v3.AttendanceModels;

public partial class Message
{
    public uint MessageId { get; set; }

    public string Message1 { get; set; } = null!;

    public virtual ICollection<Communication> Communications { get; set; } = new List<Communication>();
}
