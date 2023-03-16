using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class DataProblem
{
    public int DataProblemId { get; set; }

    public int? WarMemorialId { get; set; }

    public int? PersonId { get; set; }

    public int? RecordedNameId { get; set; }

    public string? Description { get; set; }

    public int? SubmittedByUserProfileId { get; set; }

    public DateTime SubmittedOn { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<DataProblemAuditItem> DataProblemAuditItems { get; } = new List<DataProblemAuditItem>();
}
