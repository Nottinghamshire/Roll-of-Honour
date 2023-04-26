using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class PhotoRepository : IPhotoRepository
{
    //https://ncc01sarollhonstdlrsdev.blob.core.windows.net/images/10002/original.jpg
    private string settingBlobName = "ncc01sarollhonstdlrsdev";
    private string settingBlobImageContainerName = "images";

    private RollOfHonourContext _dbContext { get; set; }

    public PhotoRepository(RollOfHonourContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Photo>> PersonPhotos(int personId)
    {
        try
        {
            var photo = await _dbContext.Photos.Where(p => p.PersonId == personId).ToListAsync();

            return photo.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName));
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

            return photo.Select(p => p.ToDomainModel(settingBlobName, settingBlobImageContainerName));
        }
        catch (Exception)
        {
            return new List<Photo>();
        }
    }
}
