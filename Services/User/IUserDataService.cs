using HAOS.Models.User;

namespace HAOS.Services.User;

public interface IUserDataService
{
    Task<List<CompletedWorkout>> GetAllCompletedWorkouts(int userId);
    Task<CompletedWorkout> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId);
    Task<CompletedWorkout> DeleteCompletedWorkout(int completedWorkoutId, int userId);
}