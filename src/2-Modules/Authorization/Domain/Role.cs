using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Entities;
using CodeBlock.DevKit.Domain.Exceptions;

namespace CodeBlock.DevKit.Authorization.Domain;

public class Role : AggregateRoot
{
    private Role(IRoleRepository roleRepository, string name)
    {
        Name = name;

        CheckPolicies(roleRepository);
    }

    public string Name { get; private set; }

    public static Role Create(IRoleRepository roleRepository, string name)
    {
        return new Role(roleRepository, name);
    }

    public void Update(IRoleRepository roleRepository, string name)
    {
        Name = name;

        CheckPolicies(roleRepository);
    }

    protected override void CheckInvariants() { }

    private void CheckPolicies(IRoleRepository roleRepository)
    {
        if (string.IsNullOrEmpty(Name))
            throw new DomainException(string.Format(CommonResource.Required, AuthorizationResource.Name));

        if (!roleRepository.NameIsUnique(Id, Name))
            throw new DomainException(string.Format(CommonResource.ALready_Exists, AuthorizationResource.Name));
    }
}
