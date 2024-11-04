using CodeBlock.DevKit.Domain.Entities;

namespace CodeBlock.DevKit.Authorization.Domain;

public class User : AggregateRoot
{
    private User(IUserRepository userRepository, string email, string passwordSalt, string passwordHash)
    {
        Email = email;
        Roles = new List<string>();
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;

        CheckPolicies(userRepository);
    }

    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public List<string> Roles { get; private set; }

    public static User Register(IUserRepository userRepository, string email, string passwordSalt, string passwordHash)
    {
        return new User(userRepository, email, passwordSalt, passwordHash);
    }

    public void Update(IUserRepository userRepository, string email)
    {
        Email = email;

        CheckPolicies(userRepository);
    }

    public void SetPassword(string passwordSalt, string passwordHash)
    {
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }

    public bool IsValidPassword(string passwordHash)
    {
        return PasswordHash == passwordHash;
    }

    public void AddRole(string role)
    {
        if (Roles.Contains(role))
            return;

        Roles.Add(role);
    }

    protected override void CheckInvariants() { }

    private void CheckPolicies(IUserRepository userRepository)
    {
        if (string.IsNullOrEmpty(Email))
            throw AuthorizationExceptions.UserEmailIsRequired();

        if (string.IsNullOrEmpty(PasswordSalt))
            throw AuthorizationExceptions.PasswordIsRequired();

        if (string.IsNullOrEmpty(PasswordHash))
            throw AuthorizationExceptions.PasswordIsRequired();

        if (!userRepository.EmailIsUnique(Id, Email))
            throw AuthorizationExceptions.UserEmailMustBeUnique();
    }
}
