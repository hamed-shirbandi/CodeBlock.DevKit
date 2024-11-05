namespace CodeBlock.DevKit.Authorization.Domain;

public interface IPasswordService
{
    string CreatePasswordSalt(int size);
    string CreatePasswordHash(string password, string salt);
}
