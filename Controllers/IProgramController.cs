using HAOS.Models.Training;

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
    Task<IResult> DeleteProgramSegment(int programId, int id);

    // Program Day Data
    Task<IResult> GetProgramDays(int segmentId);
    Task<IResult> GetProgramDayById(int id);
    Task<IResult> AddProgramDay(ProgramDay programDay, int segmentId);
    Task<IResult> UpdateProgramDay(ProgramDay programDay, int id);
    Task<IResult> DeleteProgramDay(int segmentId, int id);

    // Circuit Data
    Task<IResult> GetCircuits(int dayId);
    Task<IResult> GetCircuitById(int id);
    Task<IResult> AddCircuit(Circuit circuit, int dayId);
    Task<IResult> DeleteCircuit(int dayId, int id);
    Task<IResult> RemoveWorkout(int circuitId, int workoutId);
    Task<IResult> AddWorkout(int circuitId, int workoutId);

    // Workout Data
    Task<IResult> GetExercises();
    Task<IResult> GetExerciseById(int id);
    Task<IResult> AddExercise(Workout exercise);
    Task<IResult> UpdateExercise(Workout exercise, int id);
    Task<IResult> DeleteExercise(int id);

}