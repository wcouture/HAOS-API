using Microsoft.EntityFrameworkCore;

namespace HAOS.Models.Auth;

public class EncryptionKeyDb : DbContext
{
    public EncryptionKeyDb(DbContextOptions<EncryptionKeyDb> options) : base(options) { }
    public DbSet<EncryptionKey> EncryptionKeys { get; set; }
}