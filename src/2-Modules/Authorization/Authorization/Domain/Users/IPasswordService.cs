namespace CodeBlock.DevKit.Authorization.Domain.Users;

internal interface IPasswordService
{
    string CreatePasswordSalt(int size);
    string CreatePasswordHash(string password, string salt);
}
