using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class ExerciseService : IExerciseService
{
    private readonly TrainingDb _context;

    public ExerciseService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<Exercise> CreateExercise(Exercise exercise)
    {
        var existingExercise = await _context.ExerciseData.FirstOrDefaultAsync(e => e.Name == exercise.Name);
        if (existingExercise != null)
        {
            throw new DbConflictException("Exercise with the same name already exists.");
        }

        var result = await _context.ExerciseData.AddAsync(exercise);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Exercise> DeleteExercise(int id)
    {
        var exercise = await _context.ExerciseData.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Exercise not found.");
        
        var workouts = await _context.WorkoutData.Include(w => w.Exercise_).Where(w => w.Exercise_.Id == id).ToListAsync();
        _context.RemoveRange(workouts);
        _context.Remove(exercise);
        await _context.SaveChangesAsync();
        return exercise;
    }

    public async Task<Exercise> GetExercise(int id)
    {
        var exercise = await _context.ExerciseData.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Exercise not found.");
        return exercise;
    }

    public async Task<IEnumerable<Exercise>> GetExercises()
    {
        var exercises = await _context.ExerciseData.ToListAsync();
        return exercises;
    }

    public async Task<Exercise> UpdateExercise(Exercise exercise, int id)
    {
        var existingExercise = await _context.ExerciseData.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Exercise not found.");

        existingExercise.Name = exercise.Name;
        existingExercise.DemoUrl = exercise.DemoUrl;

        _context.SaveChanges();
        return existingExercise;
    }
}