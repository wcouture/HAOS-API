using HAOS.Models.Training;
namespace HAOS.Services.Training;

public interface IProgramDayService
{
    Task<IList<ProgramDay>> GetProgramDays(int segmentId);
    Task<ProgramDay> GetProgramDay(int id);
    Task<ProgramDay> CreateProgramDay(ProgramDay programDay, int segmentId);
    Task<ProgramDay> UpdateProgramDay(ProgramDay programDay, int id);
    Task<ProgramDay> DeleteProgramDay(int segmentId, int id);
}