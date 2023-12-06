using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Core.Models;
using RollOfHonour.Data.Context;

namespace RollOfHonour.Data.Repositories;

public interface IRoleRepository
{
    public Task<Role?> GetAsync(int id);
    public Task<Role?> GetByNameAsync(string name);
    public Task<IReadOnlyCollection<Role>?> GetAllAsync();
}

public class RoleRepository : IRoleRepository
{
    public RollOfHonourContext _dbContext { get; set; }

    public RoleRepository(RollOfHonourContext context)
    {
        _dbContext = context;
    }

    public async Task<Role?> GetAsync(int id)
    {
        try
        {
            return Models.DB.Role.ToDomainModel(await _dbContext.Roles.SingleOrDefaultAsync(_ => _.Id == id));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        try
        {
            return Models.DB.Role.ToDomainModel(await _dbContext.Roles.SingleOrDefaultAsync(_ => _.Name == name));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IReadOnlyCollection<Role>?> GetAllAsync()
    {
        try
        {
            var roles = await _dbContext.Roles.ToListAsync();
            return roles.Select(Models.DB.Role.ToDomainModel).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }
}

