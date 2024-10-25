using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class UserRepository : MongoDbBaseAggregateRepository<User>, IUserRepository
{
    #region Fields

    private readonly IMongoCollection<User> _users;

    #endregion

    #region Ctors

    public UserRepository(AuthorizationDbContext dbContext)
        : base(dbContext)
    {
        _users = dbContext.GetCollection<User>();
    }

    #endregion

    #region Public Methods

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _users.Find(e => e.Email == email).FirstOrDefaultAsync();
    }

    public bool EmailIsUnique(string userId, string email)
    {
        var user = _users.Find(e => e.Email == email).FirstOrDefault();
        return user == null || user.Id == userId;
    }

    #endregion

    #region Private Methods



    #endregion
}
