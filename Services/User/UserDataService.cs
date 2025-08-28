using HAOS.Models.Exceptions;
using HAOS.Models.Training;
using HAOS.Models.User;
using HAOS.Services.User;
using Microsoft.EntityFrameworkCore;

public class UserDataService : IUserDataService
{
    private readonly TrainingDb _trainingDb;
    public UserDataService(TrainingDb trainingDb)
    {
        _trainingDb = trainingDb;
    }

    public async Task<UserAccount> AddCompletedCircuit(int completedCircuitId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        if (user.CompletedCircuits?.Any(c => c == completedCircuitId) ?? false)
        {
            throw new DbConflictException("User already completed this circuit.");
        }

        user.CompletedCircuits ??= [];
        user.CompletedCircuits.Add(completedCircuitId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> RemoveCompletedCircuit(int completedCircuitId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        user.CompletedCircuits?.Remove(completedCircuitId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> AddCompletedSession(int completedSessionId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        if (user.CompletedSessions?.Any(s => s == completedSessionId) ?? false)
        {
            throw new DbConflictException("User already completed this session.");
        }

        user.CompletedSessions ??= [];
        user.CompletedSessions.Add(completedSessionId);
        _trainingDb.SaveChanges();
        return user;
    }
    public async Task<UserAccount> RemoveCompletedSession(int completedSessionId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        user.CompletedSessions?.Remove(completedSessionId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> AddCompletedDay(int completedDayId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        if (user.CompletedDays?.Any(d => d == completedDayId) ?? false)
        {
            throw new DbConflictException("User already completed this day.");
        }

        user.CompletedDays ??= [];
        user.CompletedDays.Add(completedDayId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> RemoveCompletedDay(int completedDayId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        user.CompletedDays?.Remove(completedDayId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> AddCompletedProgram(int completedProgramId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        if (user.CompletedPrograms?.Any(p => p == completedProgramId) ?? false)
        {
            throw new DbConflictException("User already completed this program.");
        }

        user.CompletedPrograms ??= [];
        user.CompletedPrograms.Add(completedProgramId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> RemoveCompletedProgram(int completedProgramId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        user.CompletedPrograms?.Remove(completedProgramId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> AddCompletedSegment(int completedSegmentId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        if (user.CompletedSegments?.Any(s => s == completedSegmentId) ?? false)
        {
            throw new DbConflictException("User already completed this segment.");
        }

        user.CompletedSegments ??= [];
        user.CompletedSegments.Add(completedSegmentId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<UserAccount> RemoveCompletedSegment(int completedSegmentId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        user.CompletedSegments?.Remove(completedSegmentId);
        _trainingDb.SaveChanges();
        return user;
    }

    public async Task<CompletedWorkout> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        completedWorkout.UserId = user.Id;

        if (user.CompletedWorkouts?.Any(cw => cw.WorkoutId == completedWorkout.WorkoutId) ?? false)
        {
            throw new DbConflictException("User already completed this workout.");
        }

        user.CompletedWorkouts ??= [];
        user.CompletedWorkouts.Add(completedWorkout);

        await _trainingDb.SaveChangesAsync();
        return completedWorkout;
    }

    public async Task<CompletedWorkout> DeleteCompletedWorkout(int completedWorkoutId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        var completedWorkout = await _trainingDb.CompletedWorkoutData.FirstOrDefaultAsync(cw => cw.Id == completedWorkoutId && cw.UserId == userId) ?? throw new KeyNotFoundException("Completed workout not found.");

        user.CompletedWorkouts!.Remove(completedWorkout);
        _trainingDb.CompletedWorkoutData.Remove(completedWorkout);
        await _trainingDb.SaveChangesAsync();
        return completedWorkout;
    }

    public async Task<List<CompletedWorkout>> GetAllCompletedWorkouts(int userId)
    {
        var user = await _trainingDb.AccountData.Include(u => u.CompletedWorkouts).FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");

        user.CompletedWorkouts ??= [];
        var completedWorkouts = user.CompletedWorkouts.ToList();
        return completedWorkouts;
    }
}