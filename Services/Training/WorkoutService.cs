using HAOS.Models.Training;
using HAOS.Models.Exceptions;
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
        workout.CircuitId = circuitId;
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");

        var existingWorkout = await _context.WorkoutData.Include(w => w.Exercise_).FirstOrDefaultAsync(w => w.Exercise_.Id == workout.Exercise_.Id && w.CircuitId == circuitId);
        if (existingWorkout != null)
        {
            throw new DbConflictException("Workout with the same exercise already exists in circuit.");
        }

        circuit.Workouts?.Add(workout);

        await _context.SaveChangesAsync();
        return workout;

    }

    public async Task<Workout> DeleteWorkout(int circuitId, int id)
    {
        var workout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");

        if (workout.CircuitId != circuitId)
            throw new KeyNotFoundException("Circuit ID doesn't match workout.");

        _context.Remove(workout);
        await _context.SaveChangesAsync();
        return workout;

    }

    public async Task<Workout> GetWorkout(int id)
    {
        var workout = await _context.WorkoutData.Include(w => w.Exercise_).FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");
        return workout;
    }

    public async Task<IList<Workout>> GetWorkouts(int circuitId)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
    
        return circuit.Workouts;
    }

    public async Task<Workout> UpdateWorkout(Workout workout, int id)
    {
        var existingWorkout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Workout not found.");

        existingWorkout.Exercise_ = workout.Exercise_;    
        existingWorkout.Description = workout.Description;
        existingWorkout.Rounds = workout.Rounds;
        existingWorkout.TrackingType_ = workout.TrackingType_;

        await _context.SaveChangesAsync();
        return existingWorkout;
    }
}