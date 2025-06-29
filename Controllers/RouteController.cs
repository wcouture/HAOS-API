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
    }

    private void MapSegmentEndpoints(WebApplication app)
    {
        app.MapGet("/segments/all/{programId}", async (IProgramController programController, int programId) => await programController.GetProgramSegments(programId));
        app.MapPost("/segments/add/{programId}", async (IProgramController programController, ProgramSegment newProgramSegment, int programId) => await programController.AddProgramSegment(newProgramSegment, programId));
        app.MapDelete("/segments/delete/{programId}/{id}", async (IProgramController programController, int programId, int id) => await programController.DeleteProgramSegment(programId, id));
        app.MapPut("/segments/update/{id}", async (IProgramController programController, ProgramSegment updatedProgramSegment, int id) => await programController.UpdateProgramSegment(updatedProgramSegment, id));
        app.MapGet("/segments/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramSegmentById(id));

    }

    private void MapDayEndpoints(WebApplication app)
    {
        app.MapGet("/days/all/{segmentId}", async (IProgramController programController, int segmentId) => await programController.GetProgramDays(segmentId));
        app.MapPost("/days/add/{segmentId}", async (IProgramController programController, ProgramDay newProgramDay, int segmentId) => await programController.AddProgramDay(newProgramDay, segmentId));
        app.MapDelete("/days/delete/{segmentId}/{id}", async (IProgramController programController, int segmentId, int id) => await programController.DeleteProgramDay(segmentId, id));
        app.MapPut("/days/update/{id}", async (IProgramController programController, ProgramDay updatedProgramDay, int id) => await programController.UpdateProgramDay(updatedProgramDay, id));
        app.MapGet("/days/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramDayById(id));

    }

    private void MapCircuitEndpoints(WebApplication app)
    {
        app.MapGet("/circuits/all/{programDayId}", async (IProgramController programController, int programDayId) => await programController.GetCircuits(programDayId));
        app.MapPost("/circuits/add/{programDayId}", async (IProgramController programController, Circuit newCircuit, int programDayId) => await programController.AddCircuit(newCircuit, programDayId));
        app.MapDelete("/circuits/delete/{programDayId}/{id}", async (IProgramController programController, int programDayId, int id) => await programController.DeleteCircuit(programDayId, id));
        app.MapGet("/circuits/find/{id}", async (IProgramController programController, int id) => await programController.GetCircuitById(id));
        app.MapPut("/circuits/update/{id}", async (IProgramController programController, Circuit updatedCircuit, int id) => await programController.UpdateCircuit(updatedCircuit, id));
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