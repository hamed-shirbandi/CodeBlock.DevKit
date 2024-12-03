// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

using System.Security.Cryptography;
using System.Text;
using CodeBlock.DevKit.Application.Srvices;

namespace CodeBlock.DevKit.Infrastructure.Services;

public class EncryptionService : IEncryptionService
{
    public EncryptionService() { }

    public string CreateSalt(int size)
    {
        var rng = RandomNumberGenerator.Create();
        var buff = new byte[size];
        rng.GetBytes(buff);
        return Convert.ToBase64String(buff);
    }

    public string CreateHash(string plainText, string salt)
    {
        var saltAndText = string.Concat(plainText, salt);
        HashAlgorithm algorithm = SHA256.Create();

        var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndText));
        return BitConverter.ToString(hashByteArray).Replace("-", "");
    }

    public string EncryptText(string plainText, string privateKey)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        if (string.IsNullOrEmpty(privateKey) || privateKey.Length != 24)
            throw new Exception("Wrong private key");

        var tDES = TripleDES.Create();

        tDES.Key = new ASCIIEncoding().GetBytes(privateKey.Substring(0, 24));
        tDES.IV = new ASCIIEncoding().GetBytes(privateKey.Substring(16, 8));

        byte[] encryptedBinary = EncryptTextToMemory(plainText, tDES.Key, tDES.IV);
        return Convert.ToBase64String(encryptedBinary);
    }

    public string DecryptText(string cipherText, string encryptionPrivateKey)
    {
        if (string.IsNullOrEmpty(cipherText))
            return cipherText;

        if (string.IsNullOrEmpty(encryptionPrivateKey) || encryptionPrivateKey.Length != 24)
            throw new Exception("Wrong encryp private key");

        var tDES = TripleDES.Create();
        tDES.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 24));
        tDES.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(16, 8));

        byte[] buffer = Convert.FromBase64String(cipherText);
        return DecryptTextFromMemory(buffer, tDES.Key, tDES.IV);
    }

    /// <summary>
    ///
    /// </summary>
    private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
    {
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, TripleDES.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                cs.Write(toEncrypt, 0, toEncrypt.Length);
                cs.FlushFinalBlock();
            }

            return ms.ToArray();
        }
    }

    private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
    {
        using (var ms = new MemoryStream(data))
        {
            using (var cs = new CryptoStream(ms, TripleDES.Create().CreateDecryptor(key, iv), CryptoStreamMode.Read))
            {
                var sr = new StreamReader(cs, new UnicodeEncoding());
                return sr.ReadLine();
            }
        }
    }
}
