using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain.Roles;

internal interface IRoleRepository : IBaseAggregateRepository<Role>
{
    bool NameIsUnique(string id, string name);
}
