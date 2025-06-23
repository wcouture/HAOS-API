using HAOS.Models;
namespace HAOS.Services;

public interface IProgramDayService
{
    Task<List<ProgramDay>> GetProgramDays(int segmentId);
    Task<ProgramDay> GetProgramDay(int id);
    
    Task<ProgramDay> CreateProgramDay(ProgramDay programDay, int segmentId);
    Task<ProgramDay> UpdateProgramDay(ProgramDay programDay);
    Task<ProgramDay> DeleteProgramDay(ProgramDay programDay);
}