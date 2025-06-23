using HAOS.Models;

namespace HAOS.Services;

public interface IProgramSegmentService
{
    Task<List<ProgramSegment>> GetProgramSegments(int programId);
    Task<ProgramSegment> GetProgramSegment(int id);
    Task<List<ProgramSegment>> AddProgramSegment(ProgramSegment programSegment, int programId);
    Task<ProgramSegment> UpdateProgramSegment(ProgramSegment programSegment, int id);
    Task<List<ProgramSegment>> DeleteProgramSegment(int id);
}