using HAOS.Models;

namespace HAOS.Controllers;

public interface IProgramController
{
    // Top Layer Program Data
    Task<IResult> GetPrograms();
    Task<IResult> GetProgramById(int id);
    Task<IResult> AddProgram(TrainingProgram program);
    Task<IResult> UpdateProgram(TrainingProgram program, int id);
    Task<IResult> DeleteProgram(int id);

    // Program Segment Data
    Task<IResult> GetProgramSegments(int programId);
    Task<IResult> GetProgramSegmentById(int id);
    Task<IResult> AddProgramSegment(ProgramSegment programSegment, int programId);
    Task<IResult> UpdateProgramSegment(ProgramSegment programSegment, int id);
    Task<IResult> DeleteProgramSegment(int id);

    // Program Day Data
    Task<IResult> GetProgramDays(int segmentId);
    Task<IResult> GetProgramDayById(int id);
    Task<IResult> AddProgramDay(ProgramDay programDay, int segmentId);
    Task<IResult> UpdateProgramDay(ProgramDay programDay, int id);
    Task<IResult> DeleteProgramDay(int id);

    // Circuit Data
    Task<IResult> GetCircuits(int dayId);
    Task<IResult> GetCircuitById(int id);
    Task<IResult> AddCircuit(Circuit circuit, int dayId);
    Task<IResult> UpdateCircuit(Circuit circuit, int id);
    Task<IResult> DeleteCircuit(int id);

    // Workout Data
    Task<IResult> GetExercises(int circuitId);
    Task<IResult> GetExerciseById(int id);
    Task<IResult> AddExercise(Workout exercise, int circuitId);
    Task<IResult> UpdateExercise(Workout exercise, int id);
    Task<IResult> DeleteExercise(int id);

}