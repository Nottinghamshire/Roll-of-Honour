using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RollOfHonour.Core;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class PhotoRepository : IPhotoRepository
{
    private readonly Storage _storage;
    private RollOfHonourContext _dbContext { get; set; }

    public PhotoRepository(RollOfHonourContext dbContext, IOptions<Storage> storageSettings)
    {
        _dbContext = dbContext;
        _storage = storageSettings.Value;
    }


    public async Task<IEnumerable<Photo>> PersonPhotos(int personId)
    {
        try
        {
            var photo = await _dbContext.Photos.Where(p => p.PersonId == personId).ToListAsync();

            return photo.Select(
                p => p.ToDomainModel(_storage.BlobName, _storage.BlobImageContainerName));
        }
        catch (Exception)
        {
            return new List<Photo>();
        }
    }

    public async Task<IEnumerable<Photo>> MemorialPhotos(int memoriald)
    {
        try
        {
            var photo = await _dbContext.Photos.Where(p => p.WarMemorialId == memoriald).ToListAsync();

            return photo.Select(
                p => p.ToDomainModel(_storage.BlobName, _storage.BlobImageContainerName));
        }
        catch (Exception)
        {
            return new List<Photo>();
        }
    }
}
