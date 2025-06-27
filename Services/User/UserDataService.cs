using HAOS.Models.User;
using HAOS.Services.User;
using Microsoft.EntityFrameworkCore;

public class UserDataService : IUserDataService
{
    private readonly UserDataDb _userDataDb;
    public UserDataService(UserDataDb userDataDb)
    {
        _userDataDb = userDataDb;
    }

    public async Task<CompletedWorkout> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId)
    {
        var user = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        completedWorkout.UserId = user.Id;

        user.CompletedWorkouts ??= [];
        user.CompletedWorkouts.Add(completedWorkout);

        await _userDataDb.SaveChangesAsync();
        return completedWorkout;
    }

    public async Task<CompletedWorkout> DeleteCompletedWorkout(int completedWorkoutId, int userId)
    {
        var user = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        var completedWorkout = await _userDataDb.CompletedWorkouts.FirstOrDefaultAsync(cw => cw.Id == completedWorkoutId && cw.UserId == userId) ?? throw new KeyNotFoundException("Completed workout not found.");

        user.CompletedWorkouts!.Remove(completedWorkout);
        await _userDataDb.SaveChangesAsync();
        return completedWorkout;
    }

    public async Task<List<CompletedWorkout>> GetAllCompletedWorkouts(int userId)
    {
        var user = await _userDataDb.UserAccounts.Include(u => u.CompletedWorkouts).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        user.CompletedWorkouts ??= [];
        var completedWorkouts = user.CompletedWorkouts.ToList();
        return completedWorkouts;
    }
}