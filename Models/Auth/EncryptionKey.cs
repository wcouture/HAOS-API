using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Auth;

public class EncryptionKey
{
    [Key]
    public int Id { get; set; }
    public required string PrivateKey { get; set; }
    public required string PublicKey { get; set; }
    public required bool IsActive { get; set; }
}