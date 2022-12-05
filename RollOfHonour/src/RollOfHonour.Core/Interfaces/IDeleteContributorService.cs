using Ardalis.Result;

namespace RollOfHonour.Core.Interfaces;

public interface IDeleteContributorService
{
    public Task<Result> DeleteContributor(int contributorId);
}
