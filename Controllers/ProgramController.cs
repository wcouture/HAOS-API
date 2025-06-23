using HAOS.Models;
using HAOS.Services;

namespace HAOS.Controllers;

public class ProgramController : IProgramController
{
    private readonly IProgramService _programService;
    // private readonly IProgramSegmentService _programSegmentService;
    // private readonly IProgramDayService _programDayService;
    // private readonly IProgramCircuitService _programCircuitService;
    // private readonly IWorkoutService _workoutService;

    public ProgramController(IProgramService programService)
    {
        _programService = programService;
        // _programSegmentService = programSegmentService;
        // _programDayService = programDayService;
        // _programCircuitService = programCircuitService;
        // _workoutService = workoutService;
    }

    // Program CRUD
     public async Task<IResult> GetProgramById(int id)
    {
        try
        {
            var program = await _programService.GetProgramById(id);
            return Results.Ok(program);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetPrograms()
    {
        var programs = await _programService.GetAllPrograms();
        return Results.Ok(programs);
    }
    public async Task<IResult> AddProgram(TrainingProgram program)
    {
        try
        {
            var newProgram = await _programService.AddProgram(program);
            return Results.Created($"/api/programs/{newProgram.Id}", newProgram);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }
    public async Task<IResult> DeleteProgram(int id)
    {
        try
        {
            var program = await _programService.DeleteProgram(id);
            return Results.Ok(program);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> UpdateProgram(TrainingProgram program, int id)
    {
        try
        {
            var updatedProgram = await _programService.UpdateProgram(program, id);
            return Results.Ok(updatedProgram);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    // Program Segment CRUD
    public Task<IResult> AddProgramSegment(ProgramSegment programSegment, int programId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> DeleteProgramSegment(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetProgramSegmentById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetProgramSegments(int programId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> UpdateProgramSegment(ProgramSegment programSegment, int id)
    {
        throw new NotImplementedException();
    }

    // Program Day CRUD
    public Task<IResult> AddProgramDay(ProgramDay programDay, int segmentId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> DeleteProgramDay(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetProgramDayById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetProgramDays(int segmentId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> UpdateProgramDay(ProgramDay programDay, int id)
    {
        throw new NotImplementedException();
    }

    // Program Circuit CRUD
    public Task<IResult> AddCircuit(Circuit circuit, int dayId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> DeleteCircuit(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetCircuitById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetCircuits(int dayId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> UpdateCircuit(Circuit circuit, int id)
    {
        throw new NotImplementedException();
    }


    // Exercise CRUD
    public Task<IResult> AddExercise(Workout exercise, int circuitId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> DeleteExercise(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetExerciseById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> GetExercises(int circuitId)
    {
        throw new NotImplementedException();
    }
    public Task<IResult> UpdateExercise(Workout exercise, int id)
    {
        throw new NotImplementedException();
    }
    
}
