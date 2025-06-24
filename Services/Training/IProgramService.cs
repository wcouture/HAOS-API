using HAOS.Models.Training;

namespace HAOS.Services.Training;

public interface IProgramService
{
    Task<List<TrainingProgram>> GetAllPrograms();
    Task<TrainingProgram> GetProgramById(int id);
    Task<TrainingProgram> AddProgram(TrainingProgram program);
    Task<TrainingProgram> UpdateProgram(TrainingProgram program, int id);
    Task<TrainingProgram> DeleteProgram(int id);
}