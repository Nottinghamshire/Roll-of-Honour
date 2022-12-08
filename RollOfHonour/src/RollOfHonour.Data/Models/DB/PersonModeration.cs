using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class PersonModeration
{
    public int Id { get; set; }

    public string? FirstNames { get; set; }

    public string? Initials { get; set; }

    public string? LastName { get; set; }

    public string? Rank { get; set; }

    public string? ServiceNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public DateTime? DateOfDeath { get; set; }

    public int? AgeAtDeath { get; set; }

    public string? Comments { get; set; }

    public bool Deleted { get; set; }

    public string? AddressAtEnlistment { get; set; }

    public int? Cwgc { get; set; }

    public string? PlaceOfBirth { get; set; }

    public string? EmploymentHobbies { get; set; }

    public string? FamilyHistory { get; set; }

    public string? MilitaryHistory { get; set; }

    public string? ExtraInfo { get; set; }

    public int? SubUnitId { get; set; }

    public int PersonId { get; set; }

    public bool? Accepted { get; set; }

    public DateTime UploadDate { get; set; }

    public int UserId { get; set; }

    public int? ModeratorId { get; set; }

    public DateTime? ModerationComplete { get; set; }

    public string? ModeratorFeedback { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual SubUnit? SubUnit { get; set; }

    public virtual UserProfile User { get; set; } = null!;
}
