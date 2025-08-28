using HAOS.Models.Training;

namespace HAOS.Services.Training;

public interface IProgramCircuitService
{
    public Task<IList<Circuit>> GetCircuits(int sessionId);
    public Task<Circuit> GetCircuit(int circuitId);
    public Task<Circuit> CreateCircuit(Circuit circuit, int sessionId);
    public Task<Circuit> DeleteCircuit(int sessionId, int circuitId);
    public Task<Circuit> UpdateCircuit(Circuit circuit, int circuitId);
}