using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class RecordedNameAuditItem
{
    public int Id { get; set; }

    public int RecordedNameId { get; set; }

    public virtual AuditItem IdNavigation { get; set; } = null!;

    public virtual RecordedName RecordedName { get; set; } = null!;
}
