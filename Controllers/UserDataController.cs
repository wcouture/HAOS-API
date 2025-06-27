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
        } catch (DbConflictException ex)
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
        } catch (KeyNotFoundException ex)
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
        } catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }


    // User Account Functions

    public async Task<IResult> DeleteUser(int id)
    {
        try
        {
            var deletedUser = await _userAccountService.DeleteUser(id);
            return Results.Ok(deletedUser);
        } catch (KeyNotFoundException ex)    
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetUserInfo(int id)
    {
        try
        {
            var userInfo = await _userAccountService.GetUserInfo(id);
            return Results.Ok(userInfo);
        } catch (KeyNotFoundException ex)
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
        catch (KeyNotFoundException ex)
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
    }
    public async Task<IResult> UpdateUserInfo(UserAccount user, int id)
    {
        try
        {
            var updatedUser = await _userAccountService.UpdateUserInfo(user, id);
            return Results.Ok(updatedUser);
        } catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
}