using Microsoft.EntityFrameworkCore;

namespace HAOS.Models;

public class TrainingDb : DbContext {
    static readonly string connectionString = "Server=localhost;Port=3306;Uid=dev;Password=devpass;Database=test";
    public TrainingDb(DbContextOptions options) : base (options) {}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    public DbSet<TrainingProgram> ProgramData { get; set; } = null!;
    public DbSet<ProgramSegment> SegmentData { get; set; } = null!;
    public DbSet<ProgramDay> ProgramDayData { get; set; } = null!;
    public DbSet<Circuit> CircuitData { get; set; } = null!;
    public DbSet<Workout> WorkoutData { get; set; } = null!;
}