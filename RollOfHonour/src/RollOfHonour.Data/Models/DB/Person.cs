using System;
using System.Collections.Generic;

namespace RollOfHonour.Data.Models.DB;

public partial class Person
{
    public Core.Models.Person ToDomainModel()
    {
        var person = new Core.Models.Person() 
        {
            Id = this.Id,
            Comments = this.Comments,
            Cwgc = this.Cwgc,
            Deleted = this.Deleted,
            Initials = this.Initials ?? string.Empty,
            Rank = this.Rank ?? string.Empty,
            EmploymentHobbies = this.EmploymentHobbies,
            ExtraInfo = this.ExtraInfo,
            FamilyHistory = this.FamilyHistory,
            FirstNames = this.FirstNames ?? string.Empty,
            LastName = this.LastName ?? string.Empty,
            MilitaryHistory = this.MilitaryHistory,
            ServiceNumber = this.ServiceNumber ?? string.Empty,
            AddressAtEnlistment = this.AddressAtEnlistment,
            AgeAtDeath = this.AgeAtDeath,
            DateOfBirth = this.DateOfBirth,
            DateOfDeath = this.DateOfDeath,
            MainPhotoId = this.MainPhotoId,
            PlaceOfBirth = this.PlaceOfBirth,
            Unit = this.SubUnit?.Name,
            Regiment = this.SubUnit?.Regiment?.Name
        };

        return person;
    }

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

    public int? MainPhotoId { get; set; }

    public int? SubUnitId { get; set; }

    public int? WarId { get; set; }

    public int? ArmedServiceId { get; set; }

    public bool Deleted { get; set; }

    public string? AddressAtEnlistment { get; set; }

    public int? Cwgc { get; set; }

    public string? PlaceOfBirth { get; set; }

    public string? EmploymentHobbies { get; set; }

    public string? FamilyHistory { get; set; }

    public string? MilitaryHistory { get; set; }

    public string? ExtraInfo { get; set; }

    public virtual ArmedService? ArmedService { get; set; }

    public virtual Photo? MainPhoto { get; set; }

    public virtual ICollection<PersonAuditItem> PersonAuditItems { get; } = new List<PersonAuditItem>();

    public virtual ICollection<PersonModeration> PersonModerations { get; } = new List<PersonModeration>();

    public virtual ICollection<PhotoModeration> PhotoModerations { get; } = new List<PhotoModeration>();

    public virtual ICollection<Photo> Photos { get; } = new List<Photo>();

    public virtual ICollection<RecordedName> RecordedNames { get; } = new List<RecordedName>();

    public virtual SubUnit? SubUnit { get; set; }

    public virtual War? War { get; set; }

    public virtual ICollection<Decoration> Decorations { get; } = new List<Decoration>();
}
