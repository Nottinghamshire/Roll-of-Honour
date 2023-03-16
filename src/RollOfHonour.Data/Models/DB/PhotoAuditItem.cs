using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class PhotoAuditItem
{
    public int Id { get; set; }

    public int PhotoId { get; set; }

    public virtual AuditItem IdNavigation { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
