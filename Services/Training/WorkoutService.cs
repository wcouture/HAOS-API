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
    public async Task<Workout> CreateWorkout(Workout workout)
    {
        var existingWorkout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Label == workout.Label);
        if (existingWorkout != null)
        {
            throw new DbConflictException("Workout with the same label already exists.");
        }

        var result = await _context.WorkoutData.AddAsync(workout);
        await _context.SaveChangesAsync();
        return result.Entity;

    }

    public async Task<Workout> DeleteWorkout(int id)
    {
        var workout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");
        var circuits = await _context.CircuitData.Include(c => c.Workouts).Where(c => c.Workouts!.Any(w => w.Id == id)).ToListAsync();

        foreach (var circuit in circuits)
        {
            circuit.Workouts?.Remove(workout);
        }

        _context.WorkoutData.Remove(workout);
        await _context.SaveChangesAsync();
        return workout;

    }

    public async Task<Workout> GetWorkout(int id)
    {
        var workout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");
        return workout;
    }

    public async Task<List<Workout>> GetWorkouts()
    {
        var workouts = await _context.WorkoutData.ToListAsync();
        return workouts;
    }

    public async Task<Workout> UpdateWorkout(Workout workout, int id)
    {
        var existingWorkout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");

        existingWorkout.Label = workout.Label;
        existingWorkout.RecommendedReps = workout.RecommendedReps;
        existingWorkout.RecommendedSets = workout.RecommendedSets;
        existingWorkout.RecommendedWeight = workout.RecommendedWeight;

        await _context.SaveChangesAsync();
        return existingWorkout;
    }
}