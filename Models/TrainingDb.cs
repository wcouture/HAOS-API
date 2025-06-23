using Microsoft.EntityFrameworkCore;

namespace HAOS.Models;

public class TrainingDb : DbContext {
    public static readonly string connectionString = "Server=localhost; User ID=dev; Password=devpass; Database=test";
    public TrainingDb() {}
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