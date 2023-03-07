using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class SubUnit
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? RegimentId { get; set; }

    public virtual ICollection<Person> People { get; } = new List<Person>();

    public virtual ICollection<PersonModeration> PersonModerations { get; } = new List<PersonModeration>();

    public virtual ICollection<RecordedName> RecordedNames { get; } = new List<RecordedName>();

    public virtual Regiment? Regiment { get; set; }
}
