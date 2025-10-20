using HAOS.Models.Exceptions;
using HAOS.Models.User;
using HAOS.Services.User;

namespace HAOS.Controllers;

public class UserDataController : IUserDataController
{
    private readonly IUserDataService _userDataService;
    private readonly IUserAccountService _userAccountService;
    public UserDataController(IUserDataService userDataService, IUserAccountService userAccountService)
    {
        _userAccountService = userAccountService;
        _userDataService = userDataService;
    }

    // Completed Workout Functions
    public async Task<IResult> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId)
    {
        try
        {
            var newCompletedWorkout = await _userDataService.AddCompletedWorkout(completedWorkout, userId);
            return Results.Created($"/userworkouts/find/{newCompletedWorkout.Id}", newCompletedWorkout);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }
    public async Task<IResult> DeleteCompletedWorkout(int completedWorkoutId, int userId)
    {
        try
        {
            var deletedCompletedWorkout = await _userDataService.DeleteCompletedWorkout(completedWorkoutId, userId);
            return Results.Ok(deletedCompletedWorkout);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetCompletedWorkouts(int userId)
    {
        try
        {
            var completedWorkouts = await _userDataService.GetAllCompletedWorkouts(userId);
            return Results.Ok(completedWorkouts);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }


    // User Account Functions

    public async Task<IResult> GetAllUsers()
    {
        var users = await _userAccountService.GetAllUsers();
        return Results.Ok(users);
    }

    public async Task<IResult> DeleteUser(int id)
    {
        try
        {
            var deletedUser = await _userAccountService.DeleteUser(id);
            return Results.Ok(deletedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    public async Task<IResult> GetUserInfo(int id)
    {
        try
        {
            var userInfo = await _userAccountService.GetUserInfo(id);
            return Results.Ok(userInfo);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> LoginUser(UserAccount user)
    {
        try
        {
            var loggedInUser = await _userAccountService.Authenticate(user);
            return Results.Ok(loggedInUser);
        }
        catch (FailedAuthenticationException)
        {
            return Results.Unauthorized();
        }
        catch (ArgumentNullException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> RegisterUser(UserAccount user)
    {
        try
        {
            var registeredUser = await _userAccountService.AddUser(user);
            return Results.Ok(registeredUser);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    public async Task<IResult> UpdateUserInfo(UserAccount user, int id)
    {
        try
        {
            var updatedUser = await _userAccountService.UpdateUserInfo(user, id);
            return Results.Ok(updatedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    public async Task<IResult> AddSubscription(int programId, int userId)
    {
        try
        {
            var subscribedUser = await _userAccountService.AddSubscription(programId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> RemoveSubscription(int programId, int userId)
    {
        try
        {
            var subscribedUser = await _userAccountService.RemoveSubscription(programId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> AddCompletedCircuit(int completedCircuitId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.AddCompletedCircuit(completedCircuitId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> RemoveCompletedCircuit(int completedCircuitId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.RemoveCompletedCircuit(completedCircuitId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> AddCompletedSession(int completedSessionId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.AddCompletedSession(completedSessionId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> RemoveCompletedSession(int completedSessionId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.RemoveCompletedSession(completedSessionId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> AddCompletedDay(int completedDayId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.AddCompletedDay(completedDayId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> RemoveCompletedDay(int completedDayId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.RemoveCompletedDay(completedDayId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> AddCompletedSegment(int completedSegmentId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.AddCompletedSegment(completedSegmentId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> RemoveCompletedSegment(int completedSegmentId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.RemoveCompletedSegment(completedSegmentId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> AddCompletedProgram(int completedProgramId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.AddCompletedProgram(completedProgramId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> RemoveCompletedProgram(int completedProgramId, int userId)
    {
        try
        {
            var subscribedUser = await _userDataService.RemoveCompletedProgram(completedProgramId, userId);
            return Results.Ok(subscribedUser);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }
}