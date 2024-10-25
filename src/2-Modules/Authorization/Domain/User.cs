using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Entities;
using CodeBlock.DevKit.Domain.Exceptions;
using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain;

public class User : AggregateRoot
{
    private User(IUserRepository userRepository, string mobile, string email)
    {
        Mobile = mobile;
        Email = email;
        Roles = new List<string>();

        CheckPolicies(userRepository);
    }

    public string Mobile { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public List<string> Roles { get; private set; }

    public static User Register(IUserRepository userRepository, string mobile, string email)
    {
        return new User(userRepository, mobile, email);
    }

    public void Update(IUserRepository userRepository, string mobile, string email)
    {
        Mobile = mobile;
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
        if (string.IsNullOrEmpty(PasswordHash))
            throw new DomainException(string.Format(CommonResource.Required, AuthorizationResource.PasswordHash));

        if (string.IsNullOrEmpty(PasswordSalt))
            throw new DomainException(string.Format(CommonResource.Required, AuthorizationResource.PasswordSalt));

        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Mobile))
            throw new DomainException(AuthorizationResource.Mobile_Or_Email_Is_Required);

        if (!string.IsNullOrEmpty(Mobile))
        {
            if (!userRepository.MobileIsUnique(Id, Mobile))
                throw new DomainException(string.Format(CommonResource.ALready_Exists, AuthorizationResource.Mobile));
        }

        if (!string.IsNullOrEmpty(Email))
        {
            if (!userRepository.EmailIsUnique(Id, Email))
                throw new DomainException(string.Format(CommonResource.ALready_Exists, AuthorizationResource.Email));
        }
    }
}
