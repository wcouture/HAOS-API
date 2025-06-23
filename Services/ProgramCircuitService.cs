using HAOS.Models;
using Microsoft.EntityFrameworkCore;

namespace HAOS.Services;

public class ProgramCircuitService : IProgramCircuitService
{
    private readonly TrainingDb _context;
    public ProgramCircuitService(TrainingDb context)
    {
        _context = context;
    }

    public async Task<Circuit> CreateCircuit(Circuit circuit, int programDayId)
    {
        var programDay = await _context.ProgramDayData.Include(p => p.Circuits).FirstOrDefaultAsync(p => p.Id == programDayId) ?? throw new KeyNotFoundException("Program day not found.");

        programDay.Circuits?.Add(circuit);
        await _context.SaveChangesAsync();
        return circuit;
    }

    public async Task<Circuit> DeleteCircuit(int programDayId, int circuitId)
    {
        var programDay = await _context.ProgramDayData.Include(p => p.Circuits).FirstOrDefaultAsync(p => p.Id == programDayId) ?? throw new KeyNotFoundException("Program day not found.");

        var circuit = programDay.Circuits?.FirstOrDefault(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");

        programDay.Circuits?.Remove(circuit);
        _context.CircuitData.Remove(circuit);
        await _context.SaveChangesAsync();
        return circuit;
    }

    public async Task<Circuit> GetCircuit(int circuitId)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        return circuit;
    }

    public async Task<List<Circuit>> GetCircuits(int programDayId)
    {
        var program = await _context.ProgramDayData.Include(p => p.Circuits).FirstOrDefaultAsync(p => p.Id == programDayId) ?? throw new KeyNotFoundException("Program day not found.");
        return program.Circuits ?? [];
    }

    public async Task<Circuit> AddWorkout(int circuitId, int workoutId)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        var workout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == workoutId) ?? throw new KeyNotFoundException("Workout not found.");

        circuit.Workouts?.Add(workout);
        await _context.SaveChangesAsync();
        return circuit;
    }

    public async Task<Circuit> RemoveWorkout(int circuitId, int workoutId)
    {
        var circuit = await _context.CircuitData.Include(c => c.Workouts).FirstOrDefaultAsync(c => c.Id == circuitId) ?? throw new KeyNotFoundException("Circuit not found.");
        var workout = await _context.WorkoutData.FirstOrDefaultAsync(w => w.Id == workoutId) ?? throw new KeyNotFoundException("Workout not found.");

        circuit.Workouts?.Remove(workout);
        await _context.SaveChangesAsync();
        return circuit;
    }
}