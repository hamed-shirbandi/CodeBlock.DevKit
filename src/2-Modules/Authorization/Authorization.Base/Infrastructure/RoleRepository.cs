using CodeBlock.DevKit.Authorization.Domain.Roles;
using CodeBlock.DevKit.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class RoleRepository : MongoDbBaseAggregateRepository<Role>, IRoleRepository
{
    #region Fields

    private readonly IMongoCollection<Role> _roles;

    #endregion

    #region Ctors

    public RoleRepository(AuthorizationDbContext dbContext)
        : base(dbContext)
    {
        _roles = dbContext.GetCollection<Role>();
    }

    #endregion

    #region Public Methods

    public bool NameIsUnique(string roleId, string name)
    {
        var role = _roles.Find(e => e.Name == name).FirstOrDefault();
        return role == null || role.Id == roleId;
    }

    #endregion

    #region Private Methods



    #endregion
}
