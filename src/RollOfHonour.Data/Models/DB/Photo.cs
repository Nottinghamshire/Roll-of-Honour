using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public class Photo
{
  public Core.Models.Photo ToDomainModel(string imageUrlPrefix)
  {
    var photo = new Core.Models.Photo(imageUrlPrefix)
    {
      Id = this.Id,
      Name = this.Name ?? string.Empty,
      Description = this.Description ?? string.Empty,
    };

    return photo;
  }
  
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
