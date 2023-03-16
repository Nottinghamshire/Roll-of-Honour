using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class PhotoModeration
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? WarMemorialId { get; set; }

    public int? PersonId { get; set; }

    public bool? Accepted { get; set; }

    public DateTime UploadDate { get; set; }

    public int UserId { get; set; }

    public int? ModeratorId { get; set; }

    public DateTime? ModerationComplete { get; set; }

    public string? ModeratorFeedback { get; set; }

    public string? Copyright { get; set; }

    public virtual Person? Person { get; set; }

    public virtual UserProfile User { get; set; } = null!;

    public virtual WarMemorial? WarMemorial { get; set; }
}
