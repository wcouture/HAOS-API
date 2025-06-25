using HAOS.Models.Auth;
using HAOS.Models.Training;
using HAOS.Services.Training;
using HAOS.Services.Auth;
using HAOS.Controllers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<EncryptionKeyDb>();
builder.Services.AddDbContext<TrainingDb>();


builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IProgramSegmentService, ProgramSegmentService>();
builder.Services.AddScoped<IProgramDayService, ProgramDayService>();
builder.Services.AddScoped<IProgramCircuitService, ProgramCircuitService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

builder.Services.AddScoped<IProgramController, ProgramController>();
builder.Services.AddScoped<IEncryptionService, RsaEncryptionService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "HAOS Training App API Backend",
        Description = "API for retrieving program and user data for the HAOS training app.",
        Version = "v2"
    });
    c.TagActionsBy(d =>
    {
        var rootSegment = d.RelativePath?.Split("/")[0];
        if (string.IsNullOrEmpty(rootSegment))
        {
            rootSegment = "Home";
        }
        rootSegment = rootSegment[0].ToString().ToUpper() + rootSegment.Substring(1);
        return new[] { rootSegment };
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TrainingDb>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "HAOS Training API");
    });
}

app.UseHttpsRedirection();

// Custom authentication
app.Use((context, next) =>
{
    return next(context);

    // // Skip authentication for public key requests
    // if (context.Request.Path == "/rsa/key")
    // {
    //     return next(context);
    // }
    // var _encryptionService = context.RequestServices.GetRequiredService<IEncryptionService>();

    // // Read in auth token from header, assume encrypted
    // var encryptedAuthToken = context.Request.Headers["Authorization"];
    // var authToken = string.Empty;
    // if (!string.IsNullOrEmpty(encryptedAuthToken))
    // {
    //     // Decrypt auth token
    //     authToken = _encryptionService.Decrypt(encryptedAuthToken!);
    // }
    // else
    // {
    //     // Missing auth token
    //     context.Response.StatusCode = 401;
    //     context.Response.WriteAsync("Missing Authorization header");

    //     return Task.CompletedTask;
    // }

    // // Check if authToken is valid here
    // if (authToken == "HAOSAPIauthorizationToken")
    // {
    //     // Valid auth token
    //     return next(context);
    // }
    // else
    // {
    //     // Invalid auth token
    //     context.Response.StatusCode = 401;
    //     context.Response.WriteAsync("Invalid Authorization header");

    //     return Task.CompletedTask;
    // }
});


app.MapGet("/", async (TrainingDb _context) =>
{

    var programs = await _context.ProgramData.ToListAsync();
    var ProgramSegments = await _context.SegmentData.ToListAsync();
    var programDays = await _context.ProgramDayData.ToListAsync();
    var programCircuits = await _context.CircuitData.ToListAsync();
    var workouts = await _context.WorkoutData.ToListAsync();
    var exercises = await _context.ExerciseData.ToListAsync();


    return Results.Ok(new {
        programs,
        ProgramSegments,
        programDays,
        programCircuits,
        workouts,
        exercises
    });
});

app.MapGet("/rsa/key", (IEncryptionService encryptionService) =>
{
    var publicKey = encryptionService.PublicKey;
    var XMLPublicKey = encryptionService.PublicKeyXML;
    return Results.Ok(new { publicKey, XMLPublicKey });
});

// Training Programs
app.MapGet("/programs/all", async (IProgramController programController) => await programController.GetPrograms());
app.MapPost("/programs/add", async (IProgramController programController, TrainingProgram newProgram) => await programController.AddProgram(newProgram));
app.MapDelete("/programs/delete/{id}", async (IProgramController programController, int id) => await programController.DeleteProgram(id));
app.MapPut("/programs/update/{id}", async (IProgramController programController, TrainingProgram updatedProgram, int id) => await programController.UpdateProgram(updatedProgram, id));
app.MapGet("/programs/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramById(id));

// Program Segments
app.MapGet("/segments/all/{programId}", async (IProgramController programController, int programId) => await programController.GetProgramSegments(programId));
app.MapPost("/segments/add/{programId}", async (IProgramController programController, ProgramSegment newProgramSegment, int programId) => await programController.AddProgramSegment(newProgramSegment, programId));
app.MapDelete("/segments/delete/{programId}/{id}", async (IProgramController programController, int programId, int id) => await programController.DeleteProgramSegment(programId, id));
app.MapPut("/segments/update/{id}", async (IProgramController programController, ProgramSegment updatedProgramSegment, int id) => await programController.UpdateProgramSegment(updatedProgramSegment, id));
app.MapGet("/segments/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramSegmentById(id));

// Program Days
app.MapGet("/days/all/{segmentId}", async (IProgramController programController, int segmentId) => await programController.GetProgramDays(segmentId));
app.MapPost("/days/add/{segmentId}", async (IProgramController programController, ProgramDay newProgramDay, int segmentId) => await programController.AddProgramDay(newProgramDay, segmentId));
app.MapDelete("/days/delete/{segmentId}/{id}", async (IProgramController programController, int segmentId, int id) => await programController.DeleteProgramDay(segmentId, id));
app.MapPut("/days/update/{id}", async (IProgramController programController, ProgramDay updatedProgramDay, int id) => await programController.UpdateProgramDay(updatedProgramDay, id));
app.MapGet("/days/find/{id}", async (IProgramController programController, int id) => await programController.GetProgramDayById(id));

// Circuits
app.MapGet("/circuits/all/{programDayId}", async (IProgramController programController, int programDayId) => await programController.GetCircuits(programDayId));
app.MapPost("/circuits/add/{programDayId}", async (IProgramController programController, Circuit newCircuit, int programDayId) => await programController.AddCircuit(newCircuit, programDayId));
app.MapDelete("/circuits/delete/{programDayId}/{id}", async (IProgramController programController, int programDayId, int id) => await programController.DeleteCircuit(programDayId, id));
app.MapGet("/circuits/find/{id}", async (IProgramController programController, int id) => await programController.GetCircuitById(id));
app.MapPut("/circuits/update/{id}", async (IProgramController programController, Circuit updatedCircuit, int id) => await programController.UpdateCircuit(updatedCircuit, id));

// Workouts
app.MapGet("/workouts/all/{circuitId}", async (IProgramController programController, int circuitId) => await programController.GetWorkouts(circuitId));
app.MapPost("/workouts/add/{circuitId}", async (IProgramController programController, Workout newWorkout, int circuitId) => await programController.AddWorkout(newWorkout, circuitId));
app.MapDelete("/workouts/delete/{circuitId}/{id}", async (IProgramController programController, int circuitId, int id) => await programController.DeleteWorkout(circuitId, id));
app.MapPut("/workouts/update/{id}", async (IProgramController programController, Workout updatedWorkout, int id) => await programController.UpdateWorkout(updatedWorkout, id));
app.MapGet("/workouts/find/{id}", async (IProgramController programController, int id) => await programController.GetWorkoutById(id));

// Exercises
app.MapGet("/exercises/all", async (IProgramController programController) => await programController.GetExercises());
app.MapPost("/exercises/add", async (IProgramController programController, Exercise newExercise) => await programController.AddExercise(newExercise));
app.MapDelete("/exercises/delete/{id}", async (IProgramController programController, int id) => await programController.DeleteExercise(id));
app.MapPut("/exercises/update/{id}", async (IProgramController programController, Exercise updatedExercise, int id) => await programController.UpdateExercise(updatedExercise, id));
app.MapGet("/exercises/find/{id}", async (IProgramController programController, int id) => await programController.GetExerciseById(id));

app.MapDelete("/data/clear", async(TrainingDb programDb) => {
    await programDb.Database.EnsureDeletedAsync();
    await programDb.Database.EnsureCreatedAsync();
    await programDb.SaveChangesAsync();
    return Results.Ok("Database successfully deleted.");
});

app.Run();
