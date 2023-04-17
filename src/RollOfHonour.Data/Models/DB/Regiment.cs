using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class Regiment
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<SubUnit> SubUnits { get; } = new List<SubUnit>();
}
