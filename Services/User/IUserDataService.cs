using HAOS.Models.User;

namespace HAOS.Services.User;

public interface IUserDataService
{
    Task<List<CompletedWorkout>> GetAllCompletedWorkouts(int userId);
    Task<CompletedWorkout> AddCompletedWorkout(CompletedWorkout completedWorkout, int userId);
    Task<CompletedWorkout> DeleteCompletedWorkout(int completedWorkoutId, int userId);

    Task<UserAccount> AddCompletedCircuit(int completedCircuitId, int userId);
    Task<UserAccount> RemoveCompletedCircuit(int completedCircuitId, int userId);
    
    Task<UserAccount> AddCompletedDay(int completedDayId, int userId);
    Task<UserAccount> RemoveCompletedDay(int completedDayId, int userId);

    Task<UserAccount> AddCompletedSegment(int completedSegmentId, int userId);
    Task<UserAccount> RemoveCompletedSegment(int completedSegmentId, int userId);

    Task<UserAccount> AddCompletedProgram(int completedProgramId, int userId);
    Task<UserAccount> RemoveCompletedProgram(int completedProgramId, int userId);
}