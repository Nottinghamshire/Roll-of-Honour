using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class MemorialRepository : IMemorialRepository
{
  public RollOfHonourContext _dbContext { get; set; }

  public MemorialRepository(RollOfHonourContext dbContext)
  {
    _dbContext = dbContext;
  }
  
  public async Task<Memorial> GetById(int id)
  {
    try
    {
      var dbMemorial = await _dbContext.WarMemorials.FirstAsync(p => p.Id == id);
          
      // TODO: Work out mapping
      var result = new Memorial(dbMemorial.Name)
      {
        Id = dbMemorial.Id,
        Description = dbMemorial.Description,
        District = dbMemorial.District,
        Postcode = dbMemorial.Postcode,
        // TODO: Convert recorded names to domain model
        // RecordedNames = dbMemorial.RecordedNames.AsEnumerable(),
        MainPhotoId = dbMemorial.MainPhotoId,
        UKNIWMRef = dbMemorial.Ukniwmref
      };
          
      return result;
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
        //.OrderByDescending(x => x.Id)
        .Take(80).ToListAsync();

      return memorials.Select(m => m.ToDomainModel());
    }
    catch (Exception e)
    {
      return null;
    }
  }
}
