﻿using Ardalis.GuardClauses;
using NetTopologySuite.Geometries;
using RollOfHonour.Core.Shared;

namespace RollOfHonour.Core.Models;

public class Memorial : IAggregateRoot
{
    public Memorial(int? mainPhotoId)
    {
        MainPhotoId = mainPhotoId;
    }

    private readonly List<RecordedName> _recordedNames = new();

    public int Id { get; set; }
    public string? UKNIWMRef { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    //public int Easting { get; set; }
    //public int Northing { get; set; }
    //public Point? Location { get; set; } = new Point(52.9364, 1.1358);
    public string? District { get; set; }
    public string? Postcode { get; set; }
    public int NamesCount { get; set; } //TODO: Might be a better way to do this based on Count of recorded names
    public int? MainPhotoId { get; set; }
    public IEnumerable<RecordedName> RecordedNames => _recordedNames.AsReadOnly();

    public Photo? MainPhoto
    {
        get
        {
            if (MainPhotoId.HasValue && Photos.Exists(p => p.Id == MainPhotoId))
            {
                return Photos.Single(p => p.Id == MainPhotoId);
            }

            //Photo decision
            //If main photo null, then use the first of the other images (or a random one)
            if (Photos.Any())
            {
                //Promote an image to MainPhoto
                return Photos.First();
            }

            return null;
        }
    }

    public List<Photo> Photos { get; set; } = new List<Photo>();

    public Memorial(string name, Point location)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        //Location = location;
    }

    public Memorial(string name, string description, Point location)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        //Location = location;
        Description = description;
    }

    public Memorial(string name)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
    }

    public Memorial(string name, List<RecordedName> recordedNames)
    {
        Name = Guard.Against.NullOrEmpty(name, nameof(name));
        _recordedNames.AddRange(recordedNames);
    }

    public void RecordName(RecordedName newName)
    {
        Guard.Against.Null(newName, nameof(newName));
        _recordedNames.Add(newName);
    }
}
