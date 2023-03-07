using Ardalis.Specification;

namespace RollOfHonour.Core.Shared;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
