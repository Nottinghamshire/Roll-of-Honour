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

      return dbMemorial.ToDomainModel(settingBlobName, settingBlobImageContainerName);
    }
    catch (InvalidOperationException)
    {
      return null;
    }
  }

  public async Task<IEnumerable<Memorial>> GetAll()
  {
    try
    {
      var memorials = await _dbContext.WarMemorials
        .Include(m => m.Photos)
        .Take(80).ToListAsync();

      return memorials.Select(m => m.ToDomainModel(settingBlobName, settingBlobImageContainerName));
    }
    catch (Exception e)
    {
      return null;
    }
  }
}
