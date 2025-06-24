namespace HAOS.Services.Auth;

public interface IEncryptionService
{
    string PublicKey { get; }
    string PublicKeyXML { get; }
    string Encrypt(string plainText);
    string Decrypt(string cipherMessage);
}