using HAOS.Models.Training;
using HAOS.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class ProgramSegmentService : IProgramSegmentService
{
    private readonly TrainingDb _context;

    public ProgramSegmentService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<ProgramSegment> AddProgramSegment(ProgramSegment programSegment, int programId)
    {
        programSegment.ProgramId = programId;
        var program = await _context.ProgramData.Include(p => p.Segments).FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");

        if (program.Segments?.Any(s => s.Title == programSegment.Title) ?? false)
        {
            throw new DbConflictException("Segment with the same title already exists.");
        }

        programSegment.Days ??= [];

        program.Segments?.Add(programSegment);
        await _context.SaveChangesAsync();
        return programSegment;
    }

    public async Task<ProgramSegment> DeleteProgramSegment(int programId, int id)
    {
        var program = await _context.ProgramData.Include(p => p.Segments).FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");
        var segment = await _context.SegmentData.Include(s => s.Days).FirstOrDefaultAsync(ps => ps.Id == id) ?? throw new KeyNotFoundException("Segment not found.");

        var deletedDays = segment.Days;
        var dayIds = deletedDays.Select(d => d.Id).ToList();

        var deletedCircuits = await _context.CircuitData.Where(c => dayIds.Contains(c.ProgramDayId)).ToListAsync();
        var circuitIds = deletedCircuits.Select(c => c.Id).ToList();

        var deletedWorkouts = await _context.WorkoutData.Where(w => circuitIds.Contains(w.CircuitId)).ToListAsync();
        var workoutIds = deletedWorkouts.Select(w => w.Id).ToList();

        program.Segments?.Remove(segment);

        _context.WorkoutData.RemoveRange(deletedWorkouts);
        _context.CircuitData.RemoveRange(deletedCircuits);
        _context.ProgramDayData.RemoveRange(deletedDays);

        _context.Remove(segment);
        _context.SaveChanges();
        return segment;
    }

    public async Task<ProgramSegment> GetProgramSegment(int id)
    {
        var programSegment = await _context.SegmentData.FirstOrDefaultAsync(ps => ps.Id == id) ?? throw new KeyNotFoundException("Segment not found.");
        return programSegment;
    }

    public async Task<IList<ProgramSegment>> GetProgramSegments(int programId)
    {
        var program = await _context.ProgramData.Include(p => p.Segments).FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");

        return program.Segments;
    }

    public async Task<ProgramSegment> UpdateProgramSegment(ProgramSegment programSegment, int id)
    {
        var existingSegment = await _context.SegmentData.FirstOrDefaultAsync(ps => ps.Id == id) ?? throw new KeyNotFoundException("Segment not found.");

        existingSegment.Title = programSegment.Title;
        existingSegment.Subtitle = programSegment.Subtitle;

        await _context.SaveChangesAsync();
        return existingSegment;
    }
}