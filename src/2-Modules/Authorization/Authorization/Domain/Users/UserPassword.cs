using CodeBlock.DevKit.Domain.ValueObjects;

namespace CodeBlock.DevKit.Authorization.Domain.Users;

internal record UserPassword : BaseValueObject
{
    private const int PASSWORD_SALT_SIZE = 5;

    private UserPassword(string salt, string hash)
    {
        Salt = salt;
        Hash = hash;

        CheckPolicies();
    }

    public string Salt { get; private set; }
    public string Hash { get; private set; }

    public static UserPassword Create(IPasswordService passwordService, string password)
    {
        var salt = passwordService.CreatePasswordSalt(PASSWORD_SALT_SIZE);
        var hash = passwordService.CreatePasswordHash(password, salt);
        return new UserPassword(salt, hash);
    }

    public bool IsValid(IPasswordService passwordService, string password)
    {
        var hash = passwordService.CreatePasswordHash(password, Salt);

        return Hash == hash;
    }

    protected override void CheckPolicies()
    {
        if (string.IsNullOrEmpty(Salt))
            throw UserExceptions.PasswordIsRequired();

        if (string.IsNullOrEmpty(Hash))
            throw UserExceptions.PasswordIsRequired();
    }
}
