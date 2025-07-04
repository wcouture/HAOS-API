using HAOS.Models.Exceptions;
using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class ProgramService : IProgramService
{
    private readonly TrainingDb _context;
    /// <summary>
    /// Constructor for ProgramService. This takes the TrainingDb context which is used
    /// for database operations.
    /// </summary>
    /// <param name="context">The TrainingDb context to use for database operations.</param>
    public ProgramService(TrainingDb context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new program to the database.
    /// </summary>
    /// <param name="program">The program to add.</param>
    /// <returns>The added program.</returns>
    /// <exception cref="DbConflictException">Thrown if a program with the same title already exists.</exception>
    public async Task<TrainingProgram> AddProgram(TrainingProgram program)
    {
        var existingProgram = await _context.ProgramData.FirstOrDefaultAsync(p => p.Title == program.Title);
        if (existingProgram != null)
        {
            throw new DbConflictException("Program with the same title already exists.");
        }

        program.Segments ??= [];

        var result = await _context.ProgramData.AddAsync(program);
        await _context.SaveChangesAsync();
        return result.Entity;
    }


    /// <summary>
    /// Deletes a program from the database based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the program to delete.</param>
    /// <returns>The deleted program.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no program with the specified ID is found.</exception>

    public async Task<TrainingProgram> DeleteProgram(int id)
    {
        var program = await _context.ProgramData.Include(p => p.Segments).FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException("Program not found.");

        var deletedSegments = program.Segments;
        var segmentIds = deletedSegments.Select(s => s.Id).ToList();

        var deletedProgramDays = await _context.ProgramDayData.Where(p => segmentIds.Contains(p.SegmentId)).ToListAsync();
        var dayIds = deletedProgramDays.Select(d => d.Id).ToList();

        var deletedCircuits = await _context.CircuitData.Where(c => dayIds.Contains(c.ProgramDayId)).ToListAsync();
        var circuitIds = deletedCircuits.Select(c => c.Id).ToList();

        var deletedWorkouts = await _context.WorkoutData.Where(w => circuitIds.Contains(w.CircuitId)).ToListAsync();
        var workoutIds = deletedWorkouts.Select(w => w.Id).ToList();

        _context.WorkoutData.RemoveRange(deletedWorkouts);
        _context.CircuitData.RemoveRange(deletedCircuits);
        _context.ProgramDayData.RemoveRange(deletedProgramDays);
        _context.SegmentData.RemoveRange(deletedSegments);

        _context.Remove(program);

        await _context.SaveChangesAsync();
        return program;
    }

    /// <summary>
    /// Retrieves all programs from the database.
    /// </summary>
    /// <returns>A list of all programs.</returns>
    public async Task<List<TrainingProgram>> GetAllPrograms()
    {
        var programs = await _context.ProgramData.ToListAsync();
        return programs;
    }

    /// <summary>
    /// Retrieves a program from the database based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the program to retrieve.</param>
    /// <returns>The retrieved program.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no program with the specified ID is found.</exception>
    public async Task<TrainingProgram> GetProgramById(int id)
    {
        var program = await _context.ProgramData.FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException("Program not found.");
        await _context.SegmentData.LoadAsync();
        await _context.ProgramDayData.LoadAsync();
        await _context.CircuitData.LoadAsync();
        await _context.WorkoutData.LoadAsync();
        await _context.ExerciseData.LoadAsync();
        return program;
    }

    /// <summary>
    /// Updates an existing program in the database.
    /// </summary>
    /// <param name="program">The program to update.</param>
    /// <param name="id">The ID of the program to update.</param>
    /// <returns>The updated program.</returns>
    /// <exception cref="KeyNotFoundException">Thrown if no program with the specified ID is found.</exception>
    public async Task<TrainingProgram> UpdateProgram(TrainingProgram program, int id)
    {
        var existingProgram = _context.ProgramData.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException("Program not found.");

        existingProgram.Title = program.Title;
        existingProgram.Subtitle = program.Subtitle;

        await _context.SaveChangesAsync();
        return existingProgram;
    }
}