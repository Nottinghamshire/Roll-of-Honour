using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RollOfHonour.Core;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public class MemorialRepository : IMemorialRepository
{
    private readonly Storage _storage;
    public RollOfHonourContext _dbContext { get; set; }

    public MemorialRepository(RollOfHonourContext dbContext, IOptions<Storage> storageSettings)
    {
        _dbContext = dbContext;
        _storage = storageSettings.Value;
    }

    public async Task<Memorial?> GetById(int id)
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

            return dbMemorial.ToDomainModel(_storage.ImageUrlPrefix);
        }
        catch (InvalidOperationException)
        {
            // TODO: Understand if this is even possible 
            return null;
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
            return new PaginatedList<Memorial>();
        }

        dbMemorials = dbMemorials.Skip(
                (pageIndex - 1) * pageSize)
            .Take(pageSize);

        var results = await dbMemorials
            .Select(m => m.ToDomainModel(_storage.ImageUrlPrefix))
            .ToListAsync();

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

        return new PaginatedList<Memorial>(
            dbMemorials.Select(m => m.ToDomainModel(_storage.ImageUrlPrefix))
                .ToList(), _dbContext.WarMemorials.Count(), pageIndex, pageSize);
    }
}
