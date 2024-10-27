using CodeBlock.DevKit.Authorization.Resources;
using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Entities;
using CodeBlock.DevKit.Domain.Exceptions;
using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain;

public class User : AggregateRoot
{
    private User(IUserRepository userRepository, string email)
    {
        Email = email;
        Roles = new List<string>();

        CheckPolicies(userRepository);
    }

    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public List<string> Roles { get; private set; }

    public static User Register(IUserRepository userRepository, string email)
    {
        return new User(userRepository, email);
    }

    public void Update(IUserRepository userRepository, string email)
    {
        Email = email;

        CheckPolicies(userRepository);
    }

    public void SetPassword(IEncryptionService encryptionService, string newPassword)
    {
        PasswordSalt = encryptionService.CreateSaltKey(5);
        PasswordHash = encryptionService.CreatePasswordHash(newPassword, PasswordSalt);
    }

    public bool VerifyPassword(IEncryptionService encryptionService, string password)
    {
        var newPasswordHash = encryptionService.CreatePasswordHash(password, PasswordSalt);
        return newPasswordHash == PasswordHash;
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
            throw new DomainException(string.Format(CoreResource.Required, AuthorizationResource.Email));

        if (!userRepository.EmailIsUnique(Id, Email))
            throw new DomainException(string.Format(CoreResource.ALready_Exists, AuthorizationResource.Email));
    }
}
