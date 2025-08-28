using HAOS.Models.Exceptions;
using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public interface IProgramSessionService
{
    Task<Session> GetSessionByIdAsync(int id);
    Task<IList<Session>> GetSessionsByProgramDayIdAsync(int programDayId);
    Task<Session> CreateSessionAsync(Session session);
    Task<Session> UpdateSessionAsync(Session session);
    Task<bool> DeleteSessionAsync(int id);
}