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
  
  public async Task<Memorial> FindById(int id)
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
        UKNIWMRef = int.TryParse(dbMemorial.Ukniwmref, out int ukniwmRef) ? ukniwmRef : null
      };
          
      return result;
    }
    catch (InvalidOperationException)
    {
      return null;
    }
  }
}
