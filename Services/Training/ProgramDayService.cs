using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class ProgramDayService : IProgramDayService
{
    private readonly TrainingDb _context;
    public ProgramDayService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<ProgramDay> CreateProgramDay(ProgramDay programDay, int segmentId)
    {
        var segment = await _context.SegmentData.Include(s => s.Days).FirstOrDefaultAsync(s => s.Id == segmentId) ?? throw new KeyNotFoundException("Segment not found.");

        if (segment.Days?.Any(d => d.Title == programDay.Title) ?? false)
        {
            throw new DbConflictException("Day with the same title already exists.");
        }

        segment.Days?.Add(programDay);
        await _context.SaveChangesAsync();
        return programDay;
    }

    public async Task<ProgramDay> DeleteProgramDay(int segmentId, int id)
    {
        var segment = await _context.SegmentData.Include(s => s.Days).FirstOrDefaultAsync(s => s.Id == segmentId) ?? throw new KeyNotFoundException("Segment not found.");

        var day = segment.Days?.FirstOrDefault(d => d.Id == id) ?? throw new KeyNotFoundException("Day not found.");

        segment.Days?.Remove(day);
        _context.ProgramDayData.Remove(day);

        await _context.SaveChangesAsync();
        return day;
    }

    public async Task<ProgramDay> GetProgramDay(int id)
    {
        var day = await _context.ProgramDayData.FirstOrDefaultAsync(d => d.Id == id) ?? throw new KeyNotFoundException("Day not found.");
        return day;
    }

    public async Task<List<ProgramDay>> GetProgramDays(int segmentId)
    {
        var segment = await _context.SegmentData.Include(s => s.Days).FirstOrDefaultAsync(s => s.Id == segmentId) ?? throw new KeyNotFoundException("Segment not found.");
        return segment.Days ?? [];
    }

    public async Task<ProgramDay> UpdateProgramDay(ProgramDay programDay, int id)
    {
        var day = await _context.ProgramDayData.FirstOrDefaultAsync(d => d.Id == id) ?? throw new KeyNotFoundException("Day not found.");

        day.Title = programDay.Title;
        day.WeekNum = programDay.WeekNum;

        await _context.SaveChangesAsync();
        return day;
    }
}