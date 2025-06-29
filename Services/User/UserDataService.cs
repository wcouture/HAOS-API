using HAOS.Models.Training;
using HAOS.Models.User;
using HAOS.Services.User;
using Microsoft.EntityFrameworkCore;

public class UserDataService : IUserDataService
{
    private readonly TrainingDb _trainingDb;
    public UserDataService(TrainingDb trainingDb)
    {
        _trainingDb = trainingDb;
    }

    public async Task<CompletedWorkout> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        completedWorkout.UserId = user.Id;

        user.CompletedWorkouts ??= [];
        user.CompletedWorkouts.Add(completedWorkout);

        await _trainingDb.SaveChangesAsync();
        return completedWorkout;
    }

    public async Task<CompletedWorkout> DeleteCompletedWorkout(int completedWorkoutId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        var completedWorkout = await _trainingDb.CompletedWorkoutData.FirstOrDefaultAsync(cw => cw.Id == completedWorkoutId && cw.UserId == userId) ?? throw new KeyNotFoundException("Completed workout not found.");

        user.CompletedWorkouts!.Remove(completedWorkout);
        await _trainingDb.SaveChangesAsync();
        return completedWorkout;
    }

    public async Task<List<CompletedWorkout>> GetAllCompletedWorkouts(int userId)
    {
        var user = await _trainingDb.AccountData.Include(u => u.CompletedWorkouts).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        user.CompletedWorkouts ??= [];
        var completedWorkouts = user.CompletedWorkouts.ToList();
        return completedWorkouts;
    }
}