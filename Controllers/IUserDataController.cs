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
    Task<IResult> AddSubscription(int programId, int userId);
    Task<IResult> RemoveSubscription(int programId, int userId);

    // CompletedWorkout functions
    Task<IResult> GetCompletedWorkouts(int userId);
    Task<IResult> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId);
    Task<IResult> DeleteCompletedWorkout(int completedWorkoutId, int userId);

    Task<IResult> AddCompletedCircuit(int completedCircuitId, int userId);
    Task<IResult> RemoveCompletedCircuit(int completedCircuitId, int userId);

    Task<IResult> AddCompletedSession(int completedSessionId, int userId);
    Task<IResult> RemoveCompletedSession(int completedSessionId, int userId);

    Task<IResult> AddCompletedDay(int completedDayId, int userId);
    Task<IResult> RemoveCompletedDay(int completedDayId, int userId);

    Task<IResult> AddCompletedSegment(int completedSegmentId, int userId);
    Task<IResult> RemoveCompletedSegment(int completedSegmentId, int userId);
    
    Task<IResult> AddCompletedProgram(int completedProgramId, int userId);
    Task<IResult> RemoveCompletedProgram(int completedProgramId, int userId);
}