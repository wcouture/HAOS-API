using Microsoft.EntityFrameworkCore;

namespace HAOS.Models.User;

public class UserDataDb : DbContext
{
    public UserDataDb() { }
    public UserDataDb(DbContextOptions<UserDataDb> options) : base(options) { }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<CompletedWorkout> CompletedWorkouts { get; set; }
}