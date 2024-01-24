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
    public Task<Dictionary<string, int>?> GetAllAsUsernameIdCollectionAsync();

}

public class UserRepository : IUserRepository
{
    public RollOfHonourContext DbContext { get; set; }

    public UserRepository(RollOfHonourContext context)
    {
        DbContext = context;
    }

    public async Task<User?> GetAsync(Guid reference)
    {
        try
        {
            var user = await DbContext.Users.SingleOrDefaultAsync(_ => _.Reference == reference);
            return user is null ? null : Models.DB.User.ToDomainModel(user);
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
            var defaultRole = DbContext.Roles.FirstOrDefault(_ => _.Name.Equals(ApplicationRoles.User));
            var newUser = new Models.DB.User
            {
                FirstName = user.FirstName,
                Surname = user.Surname,
                AzureCorrelationId = user.AzureCorrelationId,
                EmailAddress = user.EmailAddress,
                IsActive = true,
                Role = defaultRole
            };

            await DbContext.Users.AddAsync(newUser);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public async Task<IReadOnlyCollection<User>?> GetAllAsync()
    {
        try
        {
            var users = await DbContext.Users.ToListAsync();
            return users.Select(Models.DB.User.ToDomainModel).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }
    public async Task<Dictionary<string, int>?> GetAllAsUsernameIdCollectionAsync()
    {
        try
        {
            var users = await DbContext.Users.ToListAsync();
            var userIdCollection = new Dictionary<string, int>();
            foreach (var user in users)
            {
                userIdCollection.Add($"{user.FirstName} {user.Surname}", user.Id);
            }

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
            var userEntity = await DbContext.Users.SingleOrDefaultAsync(_ => _.Id == userId);
            if (userEntity is null) return false;

            var newUserRole = await DbContext.Roles.SingleOrDefaultAsync(_ => _.Id == role.Id);
            if(newUserRole is null) return false;

            userEntity.Role = newUserRole;
            await DbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}

