namespace CodeBlock.DevKit.Application.Srvices;

public interface IEncryptionService
{
    string CreateHash(string plainText, string salt);
    string CreateSalt(int size);
    string DecryptText(string cipherText, string encryptionPrivateKey);
    string EncryptText(string plainText, string privateKey);
}
