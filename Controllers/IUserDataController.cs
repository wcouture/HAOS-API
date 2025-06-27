using HAOS.Models.User;

namespace HAOS.Controllers;

public interface IUserDataController
{
    // Account functions
    Task<IResult> RegisterUser(UserAccount user);
    Task<IResult> LoginUser(UserAccount user);
    Task<IResult> GetUserInfo(int id);
    Task<IResult> GetAllUsers();
    Task<IResult> UpdateUserInfo(UserAccount user, int id);
    Task<IResult> DeleteUser(int id);

    // CompletedWorkout functions
    Task<IResult> GetCompletedWorkouts(int userId);
    Task<IResult> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId);
    Task<IResult> DeleteCompletedWorkout(int completedWorkoutId, int userId);
}