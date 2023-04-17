using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class RecordedName
{
    public int Id { get; set; }

    public int? IwmnameRefNo { get; set; }

    public string? AsRecorded { get; set; }

    public string? FirstName { get; set; }

    public string? Initials { get; set; }

    public string? LastName { get; set; }

    public string? Rank { get; set; }

    public string? Sex { get; set; }

    public string? ServiceNumber { get; set; }

    public int WarMemorialId { get; set; }

    public int? WarId { get; set; }

    public int? PersonId { get; set; }

    public int? SubUnitId { get; set; }

    public int? ArmedServiceId { get; set; }

    public virtual ArmedService? ArmedService { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<RecordedNameAuditItem> RecordedNameAuditItems { get; } = new List<RecordedNameAuditItem>();

    public virtual SubUnit? SubUnit { get; set; }

    public virtual War? War { get; set; }

    public virtual WarMemorial WarMemorial { get; set; } = null!;

    public virtual ICollection<Decoration> Decorations { get; } = new List<Decoration>();
}
