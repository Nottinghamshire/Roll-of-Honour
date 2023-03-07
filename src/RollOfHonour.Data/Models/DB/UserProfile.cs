using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class UserProfile
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<AuditItem> AuditItems { get; } = new List<AuditItem>();

    public virtual ICollection<PersonModeration> PersonModerations { get; } = new List<PersonModeration>();

    public virtual ICollection<PhotoModeration> PhotoModerations { get; } = new List<PhotoModeration>();

    //public virtual ICollection<WebpagesRole> Roles { get; } = new List<WebpagesRole>();

    public virtual ICollection<WarMemorial> WarMemorials { get; } = new List<WarMemorial>();
}
