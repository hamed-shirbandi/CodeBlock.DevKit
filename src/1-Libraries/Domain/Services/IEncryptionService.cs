﻿namespace CodeBlock.DevKit.Domain.Services;

public interface IEncryptionService
{
    string CreatePasswordHash(string password, string saltkey);
    string CreateSaltKey(int size);
    string DecryptText(string cipherText, string encryptionPrivateKey);
    string EncryptText(string plainText, string privateKey);
}
