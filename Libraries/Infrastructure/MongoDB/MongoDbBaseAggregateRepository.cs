using CodeBlock.DevKit.Domain.Entities;
using CodeBlock.DevKit.Domain.Exceptions;
using CodeBlock.DevKit.Domain.Services;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Infrastructure.MongoDB;

public class MongoDbBaseAggregateRepository<TEntity> : MongoDbBaseRepository<TEntity>, IBaseAggregateRepository<TEntity>
    where TEntity : AggregateRoot
{
    #region Fields

    private readonly IMongoCollection<TEntity> _collection;

    #endregion

    #region Ctors

    public MongoDbBaseAggregateRepository(MongoDbContext dbContext)
        : base(dbContext)
    {
        _collection = dbContext.GetCollection<TEntity>();
    }

    #endregion

    #region Public Methods





    /// <summary>
    ///
    /// </summary>
    public async Task ConcurrencySafeUpdate(TEntity entity, string loadedVersion)
    {
        await CheckIfVersionIsChangedAndThrowExceptionAsync(entity.Id, loadedVersion);
        await _collection.ReplaceOneAsync(p => p.Id == entity.Id && p.Version == loadedVersion, entity, new ReplaceOptions() { IsUpsert = false });
    }

    #endregion

    #region Private Methods

    /// <summary>
    ///
    /// </summary>
    private async Task CheckIfVersionIsChangedAndThrowExceptionAsync(string id, string loadedVersion)
    {
        var versionExists = await _collection.Find(e => e.Id == id && e.Version == loadedVersion).AnyAsync();
        if (!versionExists)
            throw new DomainException($"Concurrency Error. entity:{nameof(TEntity)} id:{id} loadedVersion:{loadedVersion}");
    }

    #endregion
}
