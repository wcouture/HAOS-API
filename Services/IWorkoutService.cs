using HAOS.Models;

namespace HAOS.Services;

public interface IWorkoutService
{
    public Task<List<Workout>> GetWorkouts(int circuitId);
    public Task<Workout> GetWorkout(int id);
    public Task<Workout> CreateWorkout(Workout workout, int circuitId);
    public Task<Workout> UpdateWorkout(Workout workout);
    public Task<Workout> DeleteWorkout(int id);
}