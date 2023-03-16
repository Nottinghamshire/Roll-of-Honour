using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class PersonAuditItem
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public virtual AuditItem IdNavigation { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
