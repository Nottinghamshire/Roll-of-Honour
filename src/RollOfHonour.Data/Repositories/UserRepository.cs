using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RollOfHonour.Core.Authorization;
using RollOfHonour.Data.Context;
using RollOfHonour.Data.Models.DB;
using User = RollOfHonour.Core.Models.User;

namespace RollOfHonour.Data.Repositories;

public interface IUserRepository
{
    public Task<User?> GetAsync(Guid reference);
    public Task CreateAsync(User user);
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
}

