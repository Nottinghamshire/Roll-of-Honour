using NetTopologySuite.Geometries;

namespace RollOfHonour.Data.Models.DB;

public partial class WarMemorial
{
    public Core.Models.Memorial ToDomainModel(string imageUrlPrefix)
    {
        var domainRecordedNames = this.RecordedNames.Select(recordedName => new Core.Models.RecordedName(recordedName.AsRecorded!)
        {
            Id = recordedName.Id,
            PersonId = recordedName.PersonId ?? null,
            Initials = recordedName.Initials ?? string.Empty, 
            FirstName = recordedName.FirstName ?? string.Empty,
            LastName = recordedName.LastName ?? string.Empty,
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
            Location = this.Location,
            MainPhotoId = this.MainPhotoId,
            NamesCount = this.NamesCount,
            District = this.District,
            Postcode = this.Postcode,
        };

        foreach (var photo in this.Photos)
        {
            memorial.Photos.Add(photo.ToDomainModel(imageUrlPrefix));
        }


        return memorial;
    }

    public int Id { get; set; }

    public string? Ukniwmref { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Geometry? Location { get; set; } 
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
