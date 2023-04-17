using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class MemorialRepository : IMemorialRepository
{
  private string settingBlobName = "ncc01sarollhonstdlrsdev";
  private string settingBlobImageContainerName = "images";

  public RollOfHonourContext _dbContext { get; set; }

  public MemorialRepository(RollOfHonourContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Memorial> GetById(int id)
  {
    try
    {
      var dbMemorial = await _dbContext.WarMemorials
        .Include(m => m.RecordedNames)
        .Include(m => m.Photos)
        .FirstAsync(p => p.Id == id);

        if (dbMemorial == null)
        {
            return null;
        }

      return dbMemorial.ToDomainModel(settingBlobName, settingBlobImageContainerName);
    }
    catch (InvalidOperationException)
    {
      // TODO: Understand if this is even possible 
      return null;
    }
  }
  
  // TODO: Should this return be nullable, if there were no memorials rather than an empty list?
    public async Task<IEnumerable<Memorial>> GetAll()
    {
        try
        {
            var memorials = _dbContext.WarMemorials
                .Include(m => m.Photos)
                .Take(80);

            if (!memorials.Any())
            {
                return Enumerable.Empty<Memorial>();
            }

        return memorials.Select(m => m.ToDomainModel(settingBlobName, settingBlobImageContainerName));

        }
        catch (Exception ex)
        {
            // TODO: Handle exceptions 
            throw ex;
        }
    }

    public async Task<PaginatedList<Memorial>> SearchMemorials(string searchString, int pageIndex, int pageSize)
    {
        var dbMemorials = _dbContext.WarMemorials
            .Include(m => m.Photos)
            .Where(m =>
            m.Name.Contains(searchString));

        var resultCount = dbMemorials.Count();

        if (resultCount == 0)
        {
            //TODO: handle case where there are none
            throw new NotImplementedException();
        }

        dbMemorials = dbMemorials.Skip(
            (pageIndex - 1) * pageSize)
            .Take(pageSize);

        var results = await dbMemorials.Select(m => m.ToDomainModel(settingBlobName, settingBlobImageContainerName)).ToListAsync();

        return new PaginatedList<Memorial>(results, resultCount, pageIndex, pageSize);
    }

    public int Count() 
    {
        var count = _dbContext.WarMemorials.Count();
        return count;
    }

    public async Task<PaginatedList<Memorial>> GetPageOfMemorials(int pageIndex, int pageSize)
    {
        var dbMemorials = await _dbContext.WarMemorials
            .Include(m => m.Photos)
            .Skip(
            (pageIndex - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        if (!dbMemorials.Any())
        {
            return new PaginatedList<Memorial>();
        }

        return new PaginatedList<Memorial>(dbMemorials.Select(m => m.ToDomainModel(settingBlobName, settingBlobImageContainerName)).ToList(), _dbContext.WarMemorials.Count(), pageIndex, pageSize);
    }

}
