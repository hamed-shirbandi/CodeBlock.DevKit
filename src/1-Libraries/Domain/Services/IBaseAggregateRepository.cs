using CodeBlock.DevKit.Domain.Entities;

namespace CodeBlock.DevKit.Domain.Services;

public interface IBaseAggregateRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : AggregateRoot
{
    Task ConcurrencySafeUpdate(TEntity entity, string loadedVersion);
}
