﻿using NetTopologySuite.Geometries;

namespace RollOfHonour.Data.Models.DB;

public partial class WarMemorial
{
    public Core.Models.Memorial ToDomainModel(string imageUrlPrefix)
    {
        var domainRecordedNames = this.RecordedNames.Select(recordedName => new Core.Models.RecordedName
        {
            AsRecorded = recordedName.AsRecorded!,
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

        var memorial = new Core.Models.Memorial
        {
            Name = this.Name,
            Id = this.Id,
            UKNIWMRef = this.Ukniwmref,
            Description = this.Description,
            //Location = //TODO: Convert to LatLong from East/North 
            MainPhotoId = this.MainPhotoId,
            NamesCount = this.NamesCount,
            District = this.District,
            Postcode = this.Postcode,
            RecordedNames = domainRecordedNames
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

    // below are causing errors?
    //public int Easting { get; set; }

    //public int Northing { get; set; }
    
    public Geometry? Location { get; set; } //= new Point(52.9364, 1.1358);
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
