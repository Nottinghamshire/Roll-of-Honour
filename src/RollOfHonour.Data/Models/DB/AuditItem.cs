using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class AuditItem
{
    public int Id { get; set; }

    public string? Action { get; set; }

    public string? Note { get; set; }

    public DateTime When { get; set; }

    public int UserId { get; set; }

    public virtual DataProblemAuditItem? DataProblemAuditItem { get; set; }

    public virtual PersonAuditItem? PersonAuditItem { get; set; }

    public virtual PhotoAuditItem? PhotoAuditItem { get; set; }

    public virtual RecordedNameAuditItem? RecordedNameAuditItem { get; set; }

    public virtual UserProfile User { get; set; } = null!;

    public virtual WarMemorialAuditItem? WarMemorialAuditItem { get; set; }
}
