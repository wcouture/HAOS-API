using HAOS.Models.Auth;
using HAOS.Models.Training;
using HAOS.Services.Training;
using HAOS.Services.Auth;
using HAOS.Controllers;


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

builder.Services.AddScoped<IProgramController, ProgramController>();
builder.Services.AddScoped<IEncryptionService, RsaEncryptionService>();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo {
        Title = "HAOS Training App API Backend",
        Description = "API for retrieving program and user data for the HAOS training app.",
        Version = "v2"
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
    // Skip authentication for public key requests
    if (context.Request.Path == "/rsa/key")
    {
        return next(context);
    }
    var _encryptionService = context.RequestServices.GetRequiredService<IEncryptionService>();

    // Read in auth token from header, assume encrypted
    var encryptedAuthToken = context.Request.Headers["Authorization"];
    var authToken = string.Empty;
    if (!string.IsNullOrEmpty(encryptedAuthToken))
    {
        // Decrypt auth token
        authToken = _encryptionService.Decrypt(encryptedAuthToken!);
    }
    else
    {
        // Missing auth token
        context.Response.StatusCode = 401;
        context.Response.WriteAsync("Missing Authorization header");

        return Task.CompletedTask;
    }

    // Check if authToken is valid here
    if (authToken == "HAOSAPIauthorizationToken")
    {
        // Valid auth token
        return next(context);
    }
    else
    {
        // Invalid auth token
        context.Response.StatusCode = 401;
        context.Response.WriteAsync("Invalid Authorization header");

        return Task.CompletedTask;
    }
});


app.MapGet("/", () =>
{
    return "Welcome to HAOS App API";
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

// Circuits

// Workouts

app.MapDelete("/data/clear", async(TrainingDb programDb) => {
    await programDb.Database.EnsureDeletedAsync();
    await programDb.Database.EnsureCreatedAsync();
    await programDb.SaveChangesAsync();
    return Results.Ok("Database successfully deleted.");
});

app.Run();
