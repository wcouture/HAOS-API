using HAOS.Models.Training;

namespace HAOS.Services.Training;

public interface IProgramCircuitService
{
    public Task<IList<Circuit>> GetCircuits(int programDayId);
    public Task<Circuit> GetCircuit(int circuitId);
    public Task<Circuit> CreateCircuit(Circuit circuit, int programDayId);
    public Task<Circuit> DeleteCircuit(int programDayId, int circuitId);
    public Task<Circuit> UpdateCircuit(Circuit circuit, int circuitId);
}