using HAOS.Models.Training;

namespace HAOS.Services.Training;

public interface IExerciseService
{
    public Task<Exercise> CreateExercise(Exercise exercise);
    public Task<Exercise> DeleteExercise(int id);
    public Task<Exercise> GetExercise(int id);
    public Task<IEnumerable<Exercise>> GetExercises();
    public Task<Exercise> UpdateExercise(Exercise exercise, int id);
}