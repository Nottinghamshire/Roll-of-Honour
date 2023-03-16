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

    public async Task<Memorial?> GetById(int id)
    {
        try
        {
            var dbMemorial = await _dbContext.WarMemorials
              .Include(m => m.RecordedNames)
              .FirstAsync(p => p.Id == id);
            if (dbMemorial == null)
            {
                return null;
            }

            return dbMemorial.ToDomainModel();
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
                //.OrderByDescending(x => x.Id)
                .Take(80);

            if (!memorials.Any())
            {
                return Enumerable.Empty<Memorial>();
            }

            return memorials
            .Select(m => m.ToDomainModel());
        }
        catch (Exception ex)
        {
            // TODO: Handle exceptions 
            throw ex;
        }
    }

    public async Task<IEnumerable<MemorialSearchResult>?> FindMemorialByName(string nameFragment)
    {
        var dbMemorials = _dbContext.WarMemorials.Where(m =>
            m.Name.Contains(nameFragment))
            .AsNoTracking();

        if (!dbMemorials.Any())
        {
            return null;
        }

        var results = await dbMemorials.Take(25).Select(m => new MemorialSearchResult()
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description == null ? string.Empty : m.Description
        }).ToListAsync();

        return results;
    }
}
