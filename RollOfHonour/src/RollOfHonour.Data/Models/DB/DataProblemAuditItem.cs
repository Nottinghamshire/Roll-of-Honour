using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class DataProblemAuditItem
{
    public int Id { get; set; }

    public int DataProblemId { get; set; }

    public virtual DataProblem DataProblem { get; set; } = null!;

    public virtual AuditItem IdNavigation { get; set; } = null!;
}
