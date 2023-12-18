using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Core.Models;
using RollOfHonour.Data.Context;
using RollOfHonour.Data.Models.DB;
using Role = RollOfHonour.Core.Models.Role;
using User = RollOfHonour.Core.Models.User;

namespace RollOfHonour.Data.Repositories;

public interface IUserRepository
{
    public Task<User?> GetAsync(Guid reference);
    public Task CreateAsync(User user);
    public Task<IReadOnlyCollection<User>?> GetAllAsync();
    public Task<bool> UpdateRoleAsync(int userId, Role role);
    public Task<Dictionary<string, int>> GetAllAsUsernameIdCollectionAsync();

}

public class UserRepository : IUserRepository
{
    public RollOfHonourContext _dbContext { get; set; }

    public UserRepository(RollOfHonourContext context)
    {
        _dbContext = context;
    }

    public async Task<User?> GetAsync(Guid reference)
    {
        try
        {
            return Models.DB.User.ToDomainModel(await _dbContext.Users.SingleOrDefaultAsync(_ => _.Reference == reference));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task CreateAsync(User user)
    {
        try
        {
            // don't need to validate here because we can enforce form input values via b2c user flow
            // although ideally we do it here anyway just in case
            var defaultRole = _dbContext.Roles.FirstOrDefault(_ => _.Name.Equals(ApplicationRoles.User));
            var newUser = new Models.DB.User
            {
                FirstName = user.FirstName,
                Surname = user.Surname,
                AzureCorrelationId = user.AzureCorrelationId,
                EmailAddress = user.EmailAddress,
                IsActive = true,
                Role = defaultRole
            };

            await _dbContext.Users.AddAsync(newUser);
        }
        catch (Exception)
        {
        }
    }

    public async Task<IReadOnlyCollection<User>?> GetAllAsync()
    {
        try
        {
            var users = await _dbContext.Users.ToListAsync();
            return users.Select(Models.DB.User.ToDomainModel).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<Dictionary<string, int>> GetAllAsUsernameIdCollectionAsync()
    {
        try
        {
            var users = await _dbContext.Users.ToListAsync();
            var userIdCollection = new Dictionary<string, int>();
            foreach (var user in users)
                userIdCollection.Add($"{user.FirstName} {user.Surname}", user.Id);

            return userIdCollection;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> UpdateRoleAsync(int userId, Role role)
    {
        try
        {
            var userEntity = await _dbContext.Users.SingleOrDefaultAsync(_ => _.Id == userId);
            if (userEntity is null) return false;

            var newUserRole = await _dbContext.Roles.SingleOrDefaultAsync(_ => _.Id == role.Id);
            if(newUserRole is null) return false;

            userEntity.Role = newUserRole;
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

