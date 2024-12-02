namespace CodeBlock.DevKit.Authorization.Domain.Users;

public interface IPasswordService
{
    string CreatePasswordSalt(int size);
    string CreatePasswordHash(string password, string salt);
}
