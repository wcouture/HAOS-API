using System.Security.Cryptography;
using System.Text;
using HAOS.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Auth;

public class RsaEncryptionService : IEncryptionService
{
    public string PublicKey
    {
        get
        {
            using RSA rsa = RSA.Create();
            rsa.FromXmlString(_publicKey);
            return Convert.ToBase64String(rsa.ExportRSAPublicKey());
        }

    }

    public string PublicKeyXML => _publicKey;
    private string _publicKey;
    private string _privateKey;

    private readonly EncryptionKeyDb _encryptionDb;

    public RsaEncryptionService(EncryptionKeyDb encryptionDb)
    {
        _encryptionDb = encryptionDb;
        var keys = _encryptionDb.EncryptionKeys.FirstOrDefault();
        if (keys == null || !keys.IsActive)
        {
            (_publicKey, _privateKey) = GenerateKeys().Result;
            _encryptionDb.EncryptionKeys.Add(new EncryptionKey { PublicKey = _publicKey, PrivateKey = _privateKey, IsActive = true });
            _encryptionDb.SaveChanges();

            Console.WriteLine("New keys generated.");
            Console.WriteLine("Public key: " + _publicKey);
            Console.WriteLine("Private key: " + _privateKey);
        }
        else
        {
            _publicKey = keys.PublicKey;
            _privateKey = keys.PrivateKey;
        }
    }

    private async Task<(string, string)> GenerateKeys()
    {
        using (RSA rsa = RSA.Create())
        {
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            var keys = await _encryptionDb.EncryptionKeys.FirstOrDefaultAsync();
            if (keys != null)
            {
                keys.IsActive = false;
            }

            var newKeys = new EncryptionKey { PublicKey = publicKey, PrivateKey = privateKey, IsActive = true };
            await _encryptionDb.EncryptionKeys.AddAsync(newKeys);
            await _encryptionDb.SaveChangesAsync();

            return (publicKey, privateKey);
        }
    }
    public string Decrypt(string cipherMessage)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.FromXmlString(_privateKey);
            byte[] cipherBytes = Convert.FromBase64String(cipherMessage);
            byte[] decryptedBytes = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.Pkcs1);
            string decryptedMessage = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedMessage;
        }
    }

    public string Encrypt(string plainText)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.FromXmlString(_publicKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = rsa.Encrypt(messageBytes, RSAEncryptionPadding.Pkcs1);
            string encryptedMessage = Convert.ToBase64String(encryptedBytes);
            return encryptedMessage;
        }
    }
}