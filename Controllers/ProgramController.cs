using HAOS.Models;
using HAOS.Services;

namespace HAOS.Controllers;

public class ProgramController : IProgramController
{
    private readonly IProgramService _programService;
    private readonly IProgramSegmentService _programSegmentService;
    // private readonly IProgramDayService _programDayService;
    // private readonly IProgramCircuitService _programCircuitService;
    // private readonly IWorkoutService _workoutService;

    public ProgramController(IProgramService programService, IProgramSegmentService programSegmentService)
    {
        _programService = programService;
        _programSegmentService = programSegmentService;
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
            return Results.Created($"/programs/find/{newProgram.Id}", newProgram);
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
    public async Task<IResult> AddProgramSegment(ProgramSegment programSegment, int programId)
    {
        try
        {
            var newSegment = await _programSegmentService.AddProgramSegment(programSegment, programId);
            return Results.Created($"/segments/find/{newSegment.Id}", newSegment);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> DeleteProgramSegment(int programId, int id)
    {
        try
        {
            var deletedSegment = await _programSegmentService.DeleteProgramSegment(programId, id);
            return Results.Ok(deletedSegment);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetProgramSegmentById(int id)
    {
        try
        {
            var segment = await _programSegmentService.GetProgramSegment(id);
            return Results.Ok(segment);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetProgramSegments(int programId)
    {
        try
        {
            var segments = await _programSegmentService.GetProgramSegments(programId);
            return Results.Ok(segments);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> UpdateProgramSegment(ProgramSegment programSegment, int id)
    {
        try
        {
            var updatedSegment = await _programSegmentService.UpdateProgramSegment(programSegment, id);
            return Results.Ok(updatedSegment);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
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
