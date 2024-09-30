using CodeBlock.DevKit.Core.Resources;
using CodeBlock.DevKit.Domain.Entities;
using CodeBlock.DevKit.Domain.Exceptions;
using CodeBlock.DevKit.Domain.Services;

namespace CodeBlock.DevKit.Authorization.Domain;

public class User : AggregateRoot
{
    private User(IUserRepository userRepository, IEncryptionService encryptionService, string mobile, string email, string password)
    {
        Mobile = mobile;
        Email = email;

        SetPassword(encryptionService, password);

        CheckPolicies(userRepository);
    }

    private User(IUserRepository userRepository, string mobile, string email)
    {
        Mobile = mobile;
        Email = email;
        PasswordHash = Guid.NewGuid().ToString();
        PasswordSalt = Guid.NewGuid().ToString();

        CheckPolicies(userRepository);
    }

    public string Mobile { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }

    public static User Register(IUserRepository userRepository, IEncryptionService encryptionService, string mobile, string email, string password)
    {
        return new User(userRepository, encryptionService, mobile, email, password);
    }

    public static User RegisterWithoutPassword(IUserRepository userRepository, string mobile, string email)
    {
        return new User(userRepository, mobile, email);
    }

    public void Update(IUserRepository userRepository, string mobile, string email)
    {
        Mobile = mobile;
        Email = email;

        CheckPolicies(userRepository);
    }

    public void ChangePassword(IUserRepository userRepository, IEncryptionService encryptionService, string newPassword)
    {
        SetPassword(encryptionService, newPassword);

        CheckPolicies(userRepository);
    }

    private void SetPassword(IEncryptionService encryptionService, string newPassword)
    {
        PasswordSalt = encryptionService.CreateSaltKey(5);
        PasswordHash = encryptionService.CreatePasswordHash(newPassword, PasswordSalt);
    }

    public bool VerifyPassword(IEncryptionService encryptionService, string password)
    {
        var newPasswordHash = encryptionService.CreatePasswordHash(password, PasswordSalt);
        return newPasswordHash == PasswordHash;
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
