using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class WarMemorialAuditItem
{
    public int Id { get; set; }

    public int WarMemorialId { get; set; }

    public virtual AuditItem IdNavigation { get; set; } = null!;

    public virtual WarMemorial WarMemorial { get; set; } = null!;
}
