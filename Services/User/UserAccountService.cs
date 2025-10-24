using HAOS.Models.Exceptions;
using HAOS.Models.Training;
using HAOS.Models.User;
using HAOS.Services.User;
using Microsoft.EntityFrameworkCore;
using HAOS.Services.Auth;

public class UserAccountService : IUserAccountService
{
    private readonly TrainingDb _trainingDb;
    private readonly IPasswordHasher _passwordHasher;

    public UserAccountService(TrainingDb trainingDb, IPasswordHasher passwordHasher)
    {
        _trainingDb = trainingDb;
        _passwordHasher = passwordHasher;
    }

    public async Task<IList<UserAccount>> GetAllUsers()
    {
        var users = await _trainingDb.AccountData.Include(u => u.SubscribedPrograms).Include(u => u.CompletedWorkouts).ToListAsync();
        foreach (var user in users)
        {
            user.Password = null;
        }
        return users;
    }

    public async Task<UserAccount> AddUser(UserAccount user)
    {
        var existingUser = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            throw new DbConflictException("User already exists.");
        }

        if (user.Password == null)
        {
            throw new ArgumentNullException("Password is required.");
        }
        user.Password = _passwordHasher.HashPassword(user.Password);


        user.CompletedWorkouts = [];
        user.CompletedCircuits = [];
        user.CompletedDays = [];
        user.CompletedSegments = [];
        user.CompletedPrograms = [];
        
        _trainingDb.AccountData.Add(user);
        await _trainingDb.SaveChangesAsync();
        return user;
    }

    public async Task<UserAccount> Authenticate(UserAccount user)
    {
        var existingUser = await _trainingDb.AccountData.Include(u => u.SubscribedPrograms).FirstOrDefaultAsync(u => u.Email == user.Email) ?? throw new KeyNotFoundException("User not found.");

        if (user.Password == null)
        {
            throw new ArgumentNullException("Password is required.");
        }

        if (!_passwordHasher.VerifyPassword(user.Password, existingUser.Password!))
        {
            throw new FailedAuthenticationException("Invalid password.");
        }

        return existingUser;
    }

    public async Task<UserAccount> DeleteUser(int id)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User not found.");

        var completedWorkouts = await _trainingDb.CompletedWorkoutData.Where(cw => cw.UserId == id).ToListAsync();
        _trainingDb.CompletedWorkoutData.RemoveRange(completedWorkouts);
        await _trainingDb.SaveChangesAsync();

        _trainingDb.AccountData.Remove(user);
        await _trainingDb.SaveChangesAsync();
        return user;
    }

    public async Task<UserAccount> GetUserInfo(int id)
    {
        var user = await _trainingDb.AccountData.Include(u => u.SubscribedPrograms).Include(u => u.CompletedWorkouts).FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User not found.");
        user.Password = null;
        return user;
    }

    public async Task<UserAccount> UpdateUserInfo(UserAccount user, int id)
    {
        var existingUser = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User not found.");

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;

        await _trainingDb.SaveChangesAsync();
        return existingUser;
    }

    public async Task<UserAccount> AddSubscription(int programId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        var program = await _trainingDb.ProgramData.FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");

        if (user.SubscribedPrograms?.Any(p => p.Id == programId) ?? false)
        {
            throw new DbConflictException("User already subscribed to this program.");
        }

        user.SubscribedPrograms ??= [];
        user.SubscribedPrograms.Add(program);
        await _trainingDb.SaveChangesAsync();
        return user;
    }
    
    public async Task<UserAccount> RemoveSubscription(int programId, int userId)
    {
        var user = await _trainingDb.AccountData.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        var program = await _trainingDb.ProgramData.FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");
        
        var segments = await _trainingDb.SegmentData.Where(s => s.ProgramId == programId).ToListAsync();
        var segment_ids = segments.Select(s => s.Id).ToList();

        var days = await _trainingDb.ProgramDayData.Where(d => segment_ids.Contains(d.SegmentId)).ToListAsync();
        var day_ids = days.Select(d => d.Id).ToList();

        var sessions = await _trainingDb.SessionData.Where(s => day_ids.Contains(s.ProgramDayId)).ToListAsync();
        var session_ids = sessions.Select(s => s.Id).ToList();

        var circuits = await _trainingDb.CircuitData.Where(c => session_ids.Contains(c.SessionId)).ToListAsync();
        var circuit_ids = circuits.Select(c => c.Id).ToList();

        var workouts = await _trainingDb.WorkoutData.Where(w => circuit_ids.Contains(w.CircuitId)).ToListAsync();
        var workout_ids = workouts.Select(w => w.Id).ToList();

        if (!user.SubscribedPrograms?.Any(p => p.Id == programId) ?? false)
        {
            throw new DbConflictException("User is not subscribed to this program.");
        }

        user.SubscribedPrograms?.Remove(program);

        user.CompletedPrograms?.RemoveAll(p => p == programId);
        user.CompletedSegments?.RemoveAll(s => segment_ids.Contains(s));
        user.CompletedDays?.RemoveAll(d => day_ids.Contains(d));
        user.CompletedSessions?.RemoveAll(s => session_ids.Contains(s));
        user.CompletedWorkouts?.RemoveAll(w => workout_ids.Contains(w.Id));
        user.CompletedCircuits?.RemoveAll(c => circuit_ids.Contains(c));

        await _trainingDb.SaveChangesAsync();
        return user;
    }
}