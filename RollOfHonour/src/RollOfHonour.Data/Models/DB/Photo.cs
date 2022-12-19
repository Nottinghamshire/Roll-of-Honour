using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class Photo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? WarMemorialId { get; set; }

    public int? PersonId { get; set; }

    public virtual ICollection<Person> People { get; } = new List<Person>();

    public virtual Person? Person { get; set; }

    public virtual ICollection<PhotoAuditItem> PhotoAuditItems { get; } = new List<PhotoAuditItem>();

    public virtual WarMemorial? WarMemorial { get; set; }
}
