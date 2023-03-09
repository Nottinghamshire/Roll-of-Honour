﻿namespace RollOfHonour.Data.Models.DB;

public partial class WarMemorial
{
  public Core.Models.Memorial ToDomainModel()
  {
    var domainRecordedNames = this.RecordedNames.Select(recordedName => new Core.Models.RecordedName(recordedName.AsRecorded)
      {
        Id = recordedName.Id,
        PersonId = recordedName.PersonId,
        Initials = recordedName.Initials,
        FirstName = recordedName.FirstName,
        LastName = recordedName.LastName,
        ServiceNumber = recordedName.ServiceNumber,
        Rank = recordedName.Rank,
        Sex = recordedName.Sex,
        IWMNameRefNo = recordedName.IwmnameRefNo
      })
      .ToList();

    var memorial = new Core.Models.Memorial(this.Name, domainRecordedNames)
    {
      Id = this.Id,
      UKNIWMRef = this.Ukniwmref,
      Description = this.Description,
      //Location = //TODO: Convert to LatLong from East/North 
      MainPhotoId = this.MainPhotoId,
      NamesCount = this.NamesCount,
      District = this.District,
      Postcode = this.Postcode,
    };

    return memorial;
  }

  public int Id { get; set; }

  public string? Ukniwmref { get; set; }

  public string Name { get; set; } = null!;

  public string? Description { get; set; }

  public int Easting { get; set; }

  public int Northing { get; set; }

  public int? MainPhotoId { get; set; }

  public int NamesCount { get; set; }

  public string? District { get; set; }

  public string? Postcode { get; set; }

  public virtual ICollection<PhotoModeration> PhotoModerations { get; } = new List<PhotoModeration>();

  public virtual ICollection<Photo> Photos { get; } = new List<Photo>();

  public virtual ICollection<RecordedName> RecordedNames { get; } = new List<RecordedName>();

  public virtual ICollection<WarMemorialAuditItem> WarMemorialAuditItems { get; } = new List<WarMemorialAuditItem>();

  public virtual ICollection<UserProfile> UserProfileUsers { get; } = new List<UserProfile>();
}
