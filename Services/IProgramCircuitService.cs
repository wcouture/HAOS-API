using HAOS.Models;

namespace HAOS.Services;

public interface IProgramCircuitService
{
    public Task<List<Circuit>> GetCircuits(int programDayId);
    public Task<Circuit> GetCircuit(int circuitId);
    public Task<Circuit> CreateCircuit(Circuit circuit, int programDayId);
    public Task<Circuit> UpdateCircuit(Circuit circuit);
    public Task<Circuit> DeleteCircuit(int circuitId);
}