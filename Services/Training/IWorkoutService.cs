using HAOS.Models.Training;

namespace HAOS.Services.Training;

public interface IWorkoutService
{
    public Task<List<Workout>> GetWorkouts();
    public Task<Workout> GetWorkout(int id);
    public Task<Workout> CreateWorkout(Workout workout);
    public Task<Workout> UpdateWorkout(Workout workout, int id);
    public Task<Workout> DeleteWorkout(int id);
}