using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class WorkoutService : IWorkoutService
{
    private readonly TrainingDb _context;
    public WorkoutService(TrainingDb context)
    {
        _context = context;
    }
    public async Task<Workout> CreateWorkout(Workout workout, int circuitId)
    {
        var existingWorkout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.ExerciseRef == workout.ExerciseRef);
        if (existingWorkout != null)
        {
            throw new DbConflictException("Workout with the same exercise already exists.");
        }

        var result = await _context.WorkoutData.AddAsync(workout);
        await _context.SaveChangesAsync();
        return result.Entity;

    }

    public async Task<Workout> DeleteWorkout(int circuitId, int id)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        var workout = circuit.Workouts?.FirstOrDefault(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");

        circuit.Workouts?.Remove(workout);
        _context.WorkoutData.Remove(workout);

        await _context.SaveChangesAsync();
        return workout;

    }

    public async Task<Workout> GetWorkout(int id)
    {
        var workout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");
        return workout;
    }

    public async Task<List<Workout>> GetWorkouts(int circuitId)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        return circuit.Workouts ?? [];
    }

    public async Task<Workout> UpdateWorkout(Workout workout, int id)
    {
        var existingWorkout = await _context.WorkoutData.Include(w => w.ExerciseRef).FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");

        if (workout.ExerciseRef != null)
            existingWorkout.ExerciseRef = workout.ExerciseRef;
        
        existingWorkout.RecommendedReps = workout.RecommendedReps;
        existingWorkout.RecommendedSets = workout.RecommendedSets;
        existingWorkout.RecommendedWeight = workout.RecommendedWeight;

        await _context.SaveChangesAsync();
        return existingWorkout;
    }
}