using HAOS.Models.Training;
using HAOS.Services.Training;

namespace HAOS.Controllers;

public class ProgramController : IProgramController
{
    private readonly IProgramService _programService;
    private readonly IProgramSegmentService _programSegmentService;
    private readonly IProgramDayService _programDayService;
    private readonly IProgramCircuitService _programCircuitService;
    private readonly IWorkoutService _workoutService;
    private readonly IExerciseService _exerciseService;

    public ProgramController(IProgramService programService, IProgramSegmentService programSegmentService, IProgramDayService programDayService, IProgramCircuitService programCircuitService, IWorkoutService workoutService, IExerciseService exerciseService)
    {
        _programService = programService;
        _programSegmentService = programSegmentService;
        _programDayService = programDayService;
        _programCircuitService = programCircuitService;
        _workoutService = workoutService;
        _exerciseService = exerciseService;
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
    public async Task<IResult> AddProgramDay(ProgramDay programDay, int segmentId)
    {
        try
        {
            var newDay = await _programDayService.CreateProgramDay(programDay, segmentId);
            return Results.Created($"/days/find/{newDay.Id}", newDay);
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
    public async Task<IResult> DeleteProgramDay(int segmentId, int id)
    {
        try
        {
            var deletedDay = await _programDayService.DeleteProgramDay(segmentId, id);
            return Results.Ok(deletedDay);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetProgramDayById(int id)
    {
        try
        {
            var day = await _programDayService.GetProgramDay(id);
            return Results.Ok(day);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetProgramDays(int segmentId)
    {
        try
        {
            var days = await _programDayService.GetProgramDays(segmentId);
            return Results.Ok(days);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> UpdateProgramDay(ProgramDay programDay, int id)
    {
        try
        {
            var updatedDay = await _programDayService.UpdateProgramDay(programDay, id);
            return Results.Ok(updatedDay);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    // Program Circuit CRUD
    public async Task<IResult> AddCircuit(Circuit circuit, int dayId)
    {
        try
        {
            var newCircuit = await _programCircuitService.CreateCircuit(circuit, dayId);
            return Results.Created($"/circuits/find/{newCircuit.Id}", newCircuit);
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
    public async Task<IResult> DeleteCircuit(int dayId, int id)
    {
        try
        {
            var deletedCircuit = await _programCircuitService.DeleteCircuit(dayId, id);
            return Results.Ok(deletedCircuit);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetCircuitById(int id)
    {
        try
        {
            var circuit = await _programCircuitService.GetCircuit(id);
            return Results.Ok(circuit);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetCircuits(int dayId)
    {
        try
        {
            var circuits = await _programCircuitService.GetCircuits(dayId);
            return Results.Ok(circuits);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> UpdateCircuit(Circuit circuit, int id)
    {
        try
        {
            var updatedCircuit = await _programCircuitService.UpdateCircuit(circuit, id);
            return Results.Ok(updatedCircuit);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    // Workout CRUD
    public async Task<IResult> AddWorkout(Workout workout, int circuitId)
    {
        try
        {
            var newExercise = await _workoutService.CreateWorkout(workout, circuitId);
            return Results.Created($"/workouts/find/{newExercise.Id}", newExercise);
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
    public async Task<IResult> DeleteWorkout(int circuitId, int id)
    {
        try
        {
            var deletedExercise = await _workoutService.DeleteWorkout(circuitId, id);
            return Results.Ok(deletedExercise);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetWorkoutById(int id)
    {
        try
        {
            var exercise = await _workoutService.GetWorkout(id);
            return Results.Ok(exercise);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> GetWorkouts(int circuitId)
    {
        try
        {
            var workouts = await _workoutService.GetWorkouts(circuitId);
            return Results.Ok(workouts);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
    public async Task<IResult> UpdateWorkout(Workout workout, int id)
    {
        try
        {
            var updatedExercise = await _workoutService.UpdateWorkout(workout, id);
            return Results.Ok(updatedExercise);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    // Exercise CRUD
    public async Task<IResult> AddExercise(Exercise exercise)
    {
        try
        {
            var newExercise = await _exerciseService.CreateExercise(exercise);
            return Results.Created($"/exercises/find/{newExercise.Id}", newExercise);
        }
        catch (DbConflictException ex)
        {
            return Results.Conflict(ex.Message);
        }
    }

    public async Task<IResult> GetExercises()
    {
        var exercises = await _exerciseService.GetExercises();
        return Results.Ok(exercises);
    }

    public async Task<IResult> GetExerciseById(int id)
    {
        try
        {
            var exercise = await _exerciseService.GetExercise(id);
            return Results.Ok(exercise);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    public async Task<IResult> UpdateExercise(Exercise exercise, int id)
    {
        try
        {
            var updatedExercise = await _exerciseService.UpdateExercise(exercise, id);
            return Results.Ok(updatedExercise);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }

    public async Task<IResult> DeleteExercise(int id)
    {
        try
        {
            var deletedExercise = await _exerciseService.DeleteExercise(id);
            return Results.Ok(deletedExercise);
        }
        catch (KeyNotFoundException ex)
        {
            return Results.NotFound(ex.Message);
        }
    }
}
