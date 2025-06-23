using HAOS.Models;
using HAOS.Services;
using Microsoft.EntityFrameworkCore;

public class ProgramSegmentService : IProgramSegmentService
{
    private readonly TrainingDb _context;

    public ProgramSegmentService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<ProgramSegment> AddProgramSegment(ProgramSegment programSegment, int programId)
    {
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

        var programSegment = program.Segments?.FirstOrDefault(ps => ps.Id == id) ?? throw new KeyNotFoundException("Segment not found.");

        program.Segments?.Remove(programSegment);
        _context.SegmentData.Remove(programSegment);
        _context.SaveChanges();
        return programSegment;
    }

    public async Task<ProgramSegment> GetProgramSegment(int id)
    {
        var programSegment = await _context.SegmentData.FirstOrDefaultAsync(ps => ps.Id == id) ?? throw new KeyNotFoundException("Segment not found.");
        return programSegment;
    }

    public async Task<List<ProgramSegment>> GetProgramSegments(int programId)
    {
        var program = await _context.ProgramData.Include(p => p.Segments).FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");
        
        return program.Segments ?? [];
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