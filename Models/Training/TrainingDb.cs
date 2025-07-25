using HAOS.Models.User;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Models.Training;

public class TrainingDb : DbContext
{
    public TrainingDb() { }
    public TrainingDb(DbContextOptions<TrainingDb> options) : base(options) { }

    public DbSet<TrainingProgram> ProgramData { get; set; } = null!;
    public DbSet<ProgramSegment> SegmentData { get; set; } = null!;
    public DbSet<ProgramDay> ProgramDayData { get; set; } = null!;
    public DbSet<Circuit> CircuitData { get; set; } = null!;
    public DbSet<Workout> WorkoutData { get; set; } = null!;
    public DbSet<Exercise> ExerciseData { get; set; } = null!;
    public DbSet<UserAccount> AccountData { get; set; } = null!;
    public DbSet<CompletedWorkout> CompletedWorkoutData { get; set; } = null!;
}