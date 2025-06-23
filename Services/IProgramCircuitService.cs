using HAOS.Models;

namespace HAOS.Services;

public interface IProgramCircuitService
{
    public Task<List<Circuit>> GetCircuits(int programDayId);
    public Task<Circuit> GetCircuit(int circuitId);
    public Task<Circuit> CreateCircuit(Circuit circuit, int programDayId);
    public Task<Circuit> DeleteCircuit(int programDayId, int circuitId);
    public Task<Circuit> AddWorkout(int circuitId, int exerciseId);
    public Task<Circuit> RemoveWorkout(int circuitId, int exerciseId);
}