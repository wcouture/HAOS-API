using HAOS.Models.Training;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services.Training;

public class ProgramCircuitService : IProgramCircuitService
{
    private readonly TrainingDb _context;
    public ProgramCircuitService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<Circuit> CreateCircuit(Circuit circuit, int sessionId)
    {
        circuit.SessionId = sessionId;
        var session = await _context.SessionData.Include(s => s.Circuits).FirstOrDefaultAsync(s => s.Id == sessionId) ?? throw new KeyNotFoundException("Program session not found.");

        session.Circuits?.Add(circuit);
        await _context.SaveChangesAsync();
        return circuit;
    }

    public async Task<Circuit> DeleteCircuit(int sessionId, int circuitId)
    {
        var session = await _context.SessionData.Include(s => s.Circuits).FirstOrDefaultAsync(s => s.Id == sessionId) ?? throw new KeyNotFoundException("Program session not found.");
        var circuit = _context.CircuitData.Include(c => c.Workouts).FirstOrDefault(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");

        var deletedWorkouts = circuit.Workouts;
        var workoutIds = deletedWorkouts.Select(w => w.Id).ToList();

        session.Circuits?.Remove(circuit);
        _context.WorkoutData.RemoveRange(deletedWorkouts);
        _context.CircuitData.Remove(circuit);
        await _context.SaveChangesAsync();
        return circuit;
    }

    public async Task<Circuit> GetCircuit(int circuitId)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        await _context.ExerciseData.LoadAsync();
        return circuit;
    }

    public async Task<IList<Circuit>> GetCircuits(int sessionId)
    {
        var session = await _context.SessionData.Include(s => s.Circuits).FirstOrDefaultAsync(s => s.Id == sessionId) ?? throw new KeyNotFoundException("Program session not found.");

        foreach (var circuit in session.Circuits)
        {
            var circ = await GetCircuit(circuit.Id);
            circuit.Workouts = circ.Workouts;
        }

        return session.Circuits;
    }

    public async Task<Circuit> UpdateCircuit(Circuit circuit, int circuitId)
    {
        var existingCircuit = await _context.CircuitData.FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        existingCircuit.Description = circuit.Description;
        await _context.SaveChangesAsync();
        return existingCircuit;
    }
}