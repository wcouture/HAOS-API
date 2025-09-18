using HAOS.Models.Training;
using HAOS.Models.User;
using HAOS.Services.User;

namespace HAOS.Controllers;

public class RouteController : IRouteController
{
    public void MapRouteEndpoints(WebApplication app)
    {
        // Map Program and Training Endpoints
        MapProgramEndpoints(app);
        MapSegmentEndpoints(app);
        MapDayEndpoints(app);
        MapSessionEndpoints(app);
        MapCircuitEndpoints(app);
        MapWorkoutEndpoints(app);
        MapExerciseEndpoints(app);

        // Map User Account Endpoints
        MapUserAccountEndpoints(app);
        MapUserWorkoutEndpoints(app);
    }

    private void MapUserAccountEndpoints(WebApplication app)
    {
        app.MapPost("/user/login", async (IUserDataController userDataController, UserAccount user) => await userDataController.LoginUser(user));
        app.MapPost("/user/register", async (IUserDataController userDataController, UserAccount user) => await userDataController.RegisterUser(user));
        app.MapPut("/user/update/{id}", async (IUserDataController userDataController, UserAccount updatedUser, int id) => await userDataController.UpdateUserInfo(updatedUser, id));
        app.MapGet("/user/find/{id}", async (IUserDataController userDataController, int id) => await userDataController.GetUserInfo(id));
        app.MapGet("/user/all", async (IUserDataController userDataController) => await userDataController.GetAllUsers());
        app.MapDelete("/user/delete/{id}", async (IUserDataController userDataController, int id) => await userDataController.DeleteUser(id));
        app.MapPost("/user/subscribe/{userId}/{programId}", async (IUserDataController userDataController, int userId, int programId) => await userDataController.AddSubscription(programId, userId));
        app.MapDelete("/user/unsubscribe/{userId}/{programId}", async (IUserDataController userDataController, int userId, int programId) => await userDataController.RemoveSubscription(programId, userId));
    }

    private void MapUserWorkoutEndpoints(WebApplication app)
    {
        app.MapGet("/userworkouts/all/{userId}", async (IUserDataController userDataController, int userId) => await userDataController.GetCompletedWorkouts(userId));
        app.MapPost("/userworkouts/add", async (IUserDataController userDataController, CompletedWorkout newWorkout, int userId) => await userDataController.AddCompletedWorkout(newWorkout, userId));
        app.MapDelete("/userworkouts/delete/{userId}/{id}", async (IUserDataController userDataController, int userId, int id) => await userDataController.DeleteCompletedWorkout(id, userId));
    }

    private void MapProgramEndpoints(WebApplication app)
    {
        app.MapGet("/programs/all", async (IProgramController programController) => await programController.GetPrograms());
        app.MapPost("/programs/add", async (IProgramController programController, TrainingProgram newProgram) => await programController.AddProgram(newProgram));
        app.MapDelete("/programs/delete/{id}", async (IProgramController programController, int id) => await programController.DeleteProgram(id));
        app.MapPut("/programs/update/{id}", async (IProgramController programController, TrainingProgram updatedProgram, int id) => await programController.UpdateProgram(updatedProgram, id));
        app.MapGet("/programs/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramById(id));

        app.MapPost("/programs/complete/{user_id}/{program_id}", async (IUserDataController userDataController, int user_id, int program_id) => await userDataController.AddCompletedProgram(program_id, user_id));
        app.MapDelete("/programs/uncomplete/{user_id}/{program_id}", async (IUserDataController userDataController, int user_id, int program_id) => await userDataController.RemoveCompletedProgram(program_id, user_id));
    }

    private void MapSegmentEndpoints(WebApplication app)
    {
        app.MapGet("/segments/all/{programId}", async (IProgramController programController, int programId) => await programController.GetProgramSegments(programId));
        app.MapPost("/segments/add/{programId}", async (IProgramController programController, ProgramSegment newProgramSegment, int programId) => await programController.AddProgramSegment(newProgramSegment, programId));
        app.MapDelete("/segments/delete/{programId}/{id}", async (IProgramController programController, int programId, int id) => await programController.DeleteProgramSegment(programId, id));
        app.MapPut("/segments/update/{id}", async (IProgramController programController, ProgramSegment updatedProgramSegment, int id) => await programController.UpdateProgramSegment(updatedProgramSegment, id));
        app.MapGet("/segments/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramSegmentById(id));

        app.MapPost("/segments/complete/{user_id}/{segment_id}", async (IUserDataController userDataController, int user_id, int segment_id) => await userDataController.AddCompletedSegment(segment_id, user_id));
        app.MapDelete("/segments/uncomplete/{user_id}/{segment_id}", async (IUserDataController userDataController, int user_id, int segment_id) => await userDataController.RemoveCompletedSegment(segment_id, user_id));
    }

    private void MapDayEndpoints(WebApplication app)
    {
        app.MapGet("/days/all/{segmentId}", async (IProgramController programController, int segmentId) => await programController.GetProgramDays(segmentId));
        app.MapPost("/days/add/{segmentId}", async (IProgramController programController, ProgramDay newProgramDay, int segmentId) => await programController.AddProgramDay(newProgramDay, segmentId));
        app.MapDelete("/days/delete/{segmentId}/{id}", async (IProgramController programController, int segmentId, int id) => await programController.DeleteProgramDay(segmentId, id));
        app.MapPut("/days/update/{id}", async (IProgramController programController, ProgramDay updatedProgramDay, int id) => await programController.UpdateProgramDay(updatedProgramDay, id));
        app.MapGet("/days/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramDayById(id));

        app.MapPost("/days/complete/{user_id}/{day_id}", async (IUserDataController userDataController, int user_id, int day_id) => await userDataController.AddCompletedDay(day_id, user_id));
        app.MapDelete("/days/uncomplete/{user_id}/{day_id}", async (IUserDataController userDataController, int user_id, int day_id) => await userDataController.RemoveCompletedDay(day_id, user_id));
    }

    private void MapSessionEndpoints(WebApplication app)
    {
        app.MapGet("/sessions/all/{dayId}", async (IProgramController programController, int dayId) => await programController.GetProgramSessions(dayId));
        app.MapPost("/sessions/add/{dayId}", async (IProgramController programController, Session newSession, int dayId) => await programController.AddProgramSession(newSession, dayId));
        app.MapDelete("/sessions/delete/{id}", async (IProgramController programController, int id) => await programController.DeleteProgramSession(id));
        app.MapPut("/sessions/update/{id}", async (IProgramController programController, Session updatedSession, int id) => await programController.UpdateProgramSession(updatedSession, id));
        app.MapGet("/sessions/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramSessionById(id));

        app.MapPost("/sessions/complete/{user_id}/{session_id}", async (IUserDataController userDataController, int user_id, int session_id) => await userDataController.AddCompletedSession(session_id, user_id));
        app.MapDelete("/sessions/uncomplete/{user_id}/{session_id}", async (IUserDataController userDataController, int user_id, int session_id) => await userDataController.RemoveCompletedSession(session_id, user_id));
    }

    private void MapCircuitEndpoints(WebApplication app)
    {
        app.MapGet("/circuits/all/{sessionId}", async (IProgramController programController, int sessionId) => await programController.GetCircuits(sessionId));
        app.MapPost("/circuits/add/{sessionId}", async (IProgramController programController, Circuit newCircuit, int sessionId) => await programController.AddCircuit(newCircuit, sessionId));
        app.MapDelete("/circuits/delete/{sessionId}/{id}", async (IProgramController programController, int sessionId, int id) => await programController.DeleteCircuit(sessionId, id));
        app.MapGet("/circuits/find/{id}", async (IProgramController programController, int id) => await programController.GetCircuitById(id));
        app.MapPut("/circuits/update/{id}", async (IProgramController programController, Circuit updatedCircuit, int id) => await programController.UpdateCircuit(updatedCircuit, id));

        app.MapPost("/circuits/complete/{user_id}/{circuit_id}", async (IUserDataController userDataController, int user_id, int circuit_id) => await userDataController.AddCompletedCircuit(circuit_id, user_id));
        app.MapDelete("/circuits/uncomplete/{user_id}/{circuit_id}", async (IUserDataController userDataController, int user_id, int circuit_id) => await userDataController.RemoveCompletedCircuit(circuit_id, user_id));
    }

    private void MapWorkoutEndpoints(WebApplication app)
    {
        app.MapGet("/workouts/all/{circuitId}", async (IProgramController programController, int circuitId) => await programController.GetWorkouts(circuitId));
        app.MapPost("/workouts/add/{circuitId}", async (IProgramController programController, Workout newWorkout, int circuitId) => await programController.AddWorkout(newWorkout, circuitId));
        app.MapDelete("/workouts/delete/{circuitId}/{id}", async (IProgramController programController, int circuitId, int id) => await programController.DeleteWorkout(circuitId, id));
        app.MapPut("/workouts/update/{id}", async (IProgramController programController, Workout updatedWorkout, int id) => await programController.UpdateWorkout(updatedWorkout, id));
        app.MapGet("/workouts/find/{id}", async (IProgramController programController, int id) => await programController.GetWorkoutById(id));
    }

    private void MapExerciseEndpoints(WebApplication app)
    {
        app.MapGet("/exercises/all", async (IProgramController programController) => await programController.GetExercises());
        app.MapPost("/exercises/add", async (IProgramController programController, Exercise newExercise) => await programController.AddExercise(newExercise));
        app.MapDelete("/exercises/delete/{id}", async (IProgramController programController, int id) => await programController.DeleteExercise(id));
        app.MapPut("/exercises/update/{id}", async (IProgramController programController, Exercise updatedExercise, int id) => await programController.UpdateExercise(updatedExercise, id));
        app.MapGet("/exercises/find/{id}", async (IProgramController programController, int id) => await programController.GetExerciseById(id));
    }

}