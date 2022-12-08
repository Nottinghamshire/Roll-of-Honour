using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class Decoration
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Initials { get; set; }

    public virtual ICollection<Person> People { get; } = new List<Person>();

    public virtual ICollection<RecordedName> RecordedNames { get; } = new List<RecordedName>();
}
