using CodeBlock.DevKit.Authorization.Domain;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

internal class UserRepository : MongoDbBaseAggregateRepository<User>, IUserRepository
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

    public async Task<User> GetByEmailOrMobileAsync(string emailOrMobile)
    {
        return await _users.Find(e => e.Email == emailOrMobile || e.Mobile == emailOrMobile).FirstOrDefaultAsync();
    }

    public bool EmailIsUnique(string userId, string email)
    {
        var user = _users.Find(e => e.Email == email).FirstOrDefault();
        return user == null || user.Id == userId;
    }

    public bool MobileIsUnique(string userId, string mobile)
    {
        var user = _users.Find(e => e.Mobile == mobile).FirstOrDefault();
        return user == null || user.Id == userId;
    }

    #endregion

    #region Private Methods



    #endregion
}
