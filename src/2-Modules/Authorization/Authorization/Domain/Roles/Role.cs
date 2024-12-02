using CodeBlock.DevKit.Domain.Entities;

namespace CodeBlock.DevKit.Authorization.Domain.Roles;

internal class Role : AggregateRoot
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
            throw RoleExceptions.NameIsRequired();

        if (!roleRepository.NameIsUnique(Id, Name))
            throw RoleExceptions.NameMustBeUnique();
    }
}
