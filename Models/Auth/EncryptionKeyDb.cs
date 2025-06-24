using Microsoft.EntityFrameworkCore;

namespace HAOS.Models.Auth;

public class EncryptionKeyDb : DbContext
{
    public static readonly string connectionString = "Server=localhost; User ID=dev; Password=devpass; Database=test";
    public EncryptionKeyDb(DbContextOptions<EncryptionKeyDb> options) : base(options) { }
    public DbSet<EncryptionKey> EncryptionKeys { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
}