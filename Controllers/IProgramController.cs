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

    // Session Data
    Task<IResult> AddProgramSession(Session session, int dayId);
    Task<IResult> GetProgramSessions(int dayId);
    Task<IResult> GetProgramSessionById(int id);
    Task<IResult> UpdateProgramSession(Session session, int id);
    Task<IResult> DeleteProgramSession(int id);

    // Circuit Data
    Task<IResult> GetCircuits(int dayId);
    Task<IResult> GetCircuitById(int id);
    Task<IResult> AddCircuit(Circuit circuit, int dayId);
    Task<IResult> DeleteCircuit(int dayId, int id);
    Task<IResult> UpdateCircuit(Circuit circuit, int circuitId);

    // Workout Data
    Task<IResult> GetWorkouts(int circuitId);
    Task<IResult> GetWorkoutById(int id);
    Task<IResult> AddWorkout(Workout workout, int circuitId);
    Task<IResult> UpdateWorkout(Workout workout, int id);
    Task<IResult> DeleteWorkout(int circuitId, int id);

    //Exercise Data
    Task<IResult> GetExercises();
    Task<IResult> GetExerciseById(int id);
    Task<IResult> AddExercise(Exercise exercise);
    Task<IResult> UpdateExercise(Exercise exercise, int id);
    Task<IResult> DeleteExercise(int id);

}