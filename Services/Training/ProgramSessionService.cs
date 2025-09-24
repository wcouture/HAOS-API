using HAOS.Models.Exceptions;
using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class ProgramSessionService : IProgramSessionService
{
    private readonly TrainingDb _context;

    public ProgramSessionService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<Session> GetSessionByIdAsync(int id)
    {
        var session = await _context.SessionData.Include(s => s.Circuits).FirstOrDefaultAsync(s => s.Id == id);
        await _context.WorkoutData.LoadAsync();
        await _context.ExerciseData.LoadAsync();

        if (session == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }

        return session;
    }

    public async Task<IList<Session>> GetSessionsByProgramDayIdAsync(int programDayId)
    {
        return await _context.SessionData.Where(s => s.ProgramDayId == programDayId).Include(s => s.Circuits).ToListAsync();
    }

    public async Task<Session> CreateSessionAsync(Session session)
    {
        _context.SessionData.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task<Session> UpdateSessionAsync(Session session)
    {
        var existingSession = await _context.SessionData.FindAsync(session.Id);
        if (existingSession == null)
        {
            throw new KeyNotFoundException("Session not found.");
        }

        existingSession.Title = session.Title;

        await _context.SaveChangesAsync();
        return existingSession;
    }

    public async Task<bool> DeleteSessionAsync(int id)
    {
        var session = await _context.SessionData.FindAsync(id);
        if (session == null)
        {
            return false;
        }

        // Load related circuits and workouts
        await _context.Entry(session).Collection(s => s.Circuits).LoadAsync();
        foreach (var circuit in session.Circuits)
        {
            await _context.Entry(circuit).Collection(c => c.Workouts).LoadAsync();
            _context.WorkoutData.RemoveRange(circuit.Workouts);
        }
        _context.CircuitData.RemoveRange(session.Circuits);

        _context.SessionData.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }
}