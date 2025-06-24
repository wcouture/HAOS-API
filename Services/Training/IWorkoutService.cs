using HAOS.Models.Training;

namespace HAOS.Services.Training;

public interface IWorkoutService
{
    public Task<List<Workout>> GetWorkouts(int circuitId);
    public Task<Workout> GetWorkout(int id);
    public Task<Workout> CreateWorkout(Workout workout, int circuitId);
    public Task<Workout> UpdateWorkout(Workout workout, int id);
    public Task<Workout> DeleteWorkout(int circuitId, int id);
}