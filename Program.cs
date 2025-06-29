using HAOS.Models.Auth;
using HAOS.Models.Training;
using HAOS.Models.User;
using HAOS.Services.Training;
using HAOS.Services.Auth;
using HAOS.Services.User;
using HAOS.Controllers;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EncryptionKeyDb>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddDbContext<TrainingDb>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
// builder.Services.AddDbContext<UserDataDb>(
//     options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
// );

// Training program services
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IProgramSegmentService, ProgramSegmentService>();
builder.Services.AddScoped<IProgramDayService, ProgramDayService>();
builder.Services.AddScoped<IProgramCircuitService, ProgramCircuitService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();

// User account and data services
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IUserDataService, UserDataService>();

// Encryption services
builder.Services.AddScoped<IEncryptionService, RsaEncryptionService>();

// Controllers
builder.Services.AddScoped<IProgramController, ProgramController>();
builder.Services.AddScoped<IUserDataController, UserDataController>();

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

RouteController routeController = new();
routeController.MapRouteEndpoints(app);

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

app.MapDelete("/data/clear", async(TrainingDb programDb) => {
    await programDb.Database.EnsureDeletedAsync();
    await programDb.Database.EnsureCreatedAsync();
    await programDb.SaveChangesAsync();
    return Results.Ok("Database successfully deleted.");
});

app.Run();
