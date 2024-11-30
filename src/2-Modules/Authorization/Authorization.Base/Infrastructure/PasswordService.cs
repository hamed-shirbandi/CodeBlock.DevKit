using CodeBlock.DevKit.Application.Srvices;
using CodeBlock.DevKit.Authorization.Domain.Users;

namespace CodeBlock.DevKit.Authorization.Infrastructure;

public class PasswordService : IPasswordService
{
    private readonly IEncryptionService _encryptionService;

    public PasswordService(IEncryptionService encryptionService)
    {
        _encryptionService = encryptionService;
    }

    public string CreatePasswordHash(string password, string salt)
    {
        return _encryptionService.CreateHash(password, salt);
    }

    public string CreatePasswordSalt(int size)
    {
        return _encryptionService.CreateSalt(size);
    }
}
