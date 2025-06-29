using HAOS.Models.Exceptions;
using HAOS.Models.Training;
using HAOS.Models.User;
using HAOS.Services.User;
using Microsoft.EntityFrameworkCore;

public class UserAccountService : IUserAccountService
{
    private readonly UserDataDb _userDataDb;
    private readonly TrainingDb _trainingDb;

    public UserAccountService(UserDataDb userDataDb, TrainingDb trainingDb)
    {
        _userDataDb = userDataDb;
        _trainingDb = trainingDb;
    }

    public async Task<IList<UserAccount>> GetAllUsers()
    {
        var users = await _userDataDb.UserAccounts.ToListAsync();
        foreach (var user in users)
        {
            user.Password = null;
        }
        return users;
    }

    public async Task<UserAccount> AddUser(UserAccount user)
    {
        var existingUser = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Email == user.Email);
        if (existingUser != null)
        {
            throw new DbConflictException("User already exists.");
        }
        user.CompletedWorkouts ??= [];
        _userDataDb.UserAccounts.Add(user);
        await _userDataDb.SaveChangesAsync();
        return user;
    }

    public async Task<UserAccount> Authenticate(UserAccount user)
    {
        var existingUser = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Email == user.Email) ?? throw new KeyNotFoundException("User not found.");

        if (existingUser.Password != user.Password)
        {
            throw new FailedAuthenticationException("Invalid password.");
        }

        return existingUser;
    }

    public async Task<UserAccount> DeleteUser(int id)
    {
        var user = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User not found.");

        var completedWorkouts = await _userDataDb.CompletedWorkouts.Where(cw => cw.UserId == id).ToListAsync();
        _userDataDb.CompletedWorkouts.RemoveRange(completedWorkouts);

        _userDataDb.UserAccounts.Remove(user);
        await _userDataDb.SaveChangesAsync();
        return user;
    }

    public async Task<UserAccount> GetUserInfo(int id)
    {
        var user = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User not found.");
        user.Password = null;
        return user;
    }

    public async Task<UserAccount> UpdateUserInfo(UserAccount user, int id)
    {
        var existingUser = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Id == id) ?? throw new KeyNotFoundException("User not found.");

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;

        await _userDataDb.SaveChangesAsync();
        return existingUser;
    }

    public async Task<UserAccount> AddSubscription(int programId, int userId)
    {
        var user = await _userDataDb.UserAccounts.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new KeyNotFoundException("User not found.");
        var program = await _trainingDb.ProgramData.FirstOrDefaultAsync(p => p.Id == programId) ?? throw new KeyNotFoundException("Program not found.");

        user.SubscribedPrograms ??= [];
        user.SubscribedPrograms.Add(program.Id);
        await _userDataDb.SaveChangesAsync();
        return user;
    }
}