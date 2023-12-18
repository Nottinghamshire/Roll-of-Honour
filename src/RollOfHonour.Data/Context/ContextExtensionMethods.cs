using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RollOfHonour.Data.Context;
public static class ContextExtensionMethods
{
    public static EntityEntry<T> AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
    {
        return !dbSet.Any(predicate) ? dbSet.Add(entity) : null!;
    }
}
