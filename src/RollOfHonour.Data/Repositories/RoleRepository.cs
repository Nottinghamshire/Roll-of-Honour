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
    public Task<List<string>?> GetAllNamesAsync();
}

public class RoleRepository : IRoleRepository
{
    public RollOfHonourContext DbContext { get; set; }

    public RoleRepository(RollOfHonourContext context)
    {
        DbContext = context;
    }

    public async Task<Role?> GetAsync(int id)
    {
        try
        {
            return Models.DB.Role.ToDomainModel(await DbContext.Roles.Include(role => role.Claims).SingleOrDefaultAsync(_ => _.Id == id));
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
            var role = await DbContext.Roles.Include(role => role.Claims).SingleOrDefaultAsync(_ => _.Name == name);
            return role is null ? null : Models.DB.Role.ToDomainModel(role);
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
            var roles = await DbContext.Roles.Include(role => role.Claims).ToListAsync();
            return roles.Select(Models.DB.Role.ToDomainModel).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<string>?> GetAllNamesAsync()
    {
        try
        {
            var roles = await DbContext.Roles.Include(role => role.Claims).ToListAsync();
            return roles.Select(role => role.Name).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }
}

