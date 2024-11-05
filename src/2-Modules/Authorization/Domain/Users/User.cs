using CodeBlock.DevKit.Domain.Entities;

namespace CodeBlock.DevKit.Authorization.Domain.Users;

public class User : AggregateRoot
{
    private User(IUserRepository userRepository, IPasswordService passwordService, string email, string password)
    {
        Email = email;
        Roles = new List<string>();
        Password = UserPassword.Create(passwordService, password);
        CheckPolicies(userRepository);
    }

    public string Email { get; private set; }
    public UserPassword Password { get; private set; }
    public List<string> Roles { get; private set; }

    public static User Register(IUserRepository userRepository, IPasswordService passwordService, string email, string password)
    {
        return new User(userRepository, passwordService, email, password);
    }

    public void Update(IUserRepository userRepository, string email)
    {
        Email = email;

        CheckPolicies(userRepository);
    }

    public void SetPassword(IPasswordService passwordService, string password)
    {
        Password = UserPassword.Create(passwordService, password);
    }

    public bool IsValidPassword(IPasswordService passwordService, string password)
    {
        return Password.IsValid(passwordService, password);
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
            throw UserExceptions.EmailIsRequired();

        if (!userRepository.EmailIsUnique(Id, Email))
            throw UserExceptions.EmailMustBeUnique();
    }
}
