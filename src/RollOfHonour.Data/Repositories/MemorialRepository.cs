using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;
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
            var dbMemorial = await _dbContext.WarMemorials
              .Include(m => m.RecordedNames)
              .FirstAsync(p => p.Id == id);

            return dbMemorial.ToDomainModel();
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

    public async Task<IEnumerable<MemorialSearchResult>?> FindMemorialByName(string nameFragment)
    {
        var dbMemorials = _dbContext.WarMemorials.Where(m =>
            m.Name.Contains(nameFragment))
            .AsNoTracking();

        if (dbMemorials.Count() == 0)
        {
            return null;
        }

        var results = await dbMemorials.Take(25).Select(m => new MemorialSearchResult() { Id = m.Id, Name = m.Name, Description = m.Description }).ToListAsync();

        return results;
    }
}
