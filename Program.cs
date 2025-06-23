using Microsoft.EntityFrameworkCore;
using HAOS.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<TrainingDb>();

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
    db.Database.Migrate();
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


app.MapGet("/", () =>
{
    return "Welcome to HAOS App API";
});

app.MapGet("/api/programs", async (TrainingDb programDb) => await programDb.ProgramData.Include(p => p.Segments).ToListAsync());
app.MapPost("/api/programs/add", async (TrainingDb programDb, TrainingProgram newProgram) => {
    var savedProgram = await programDb.ProgramData.AddAsync(newProgram);
    await programDb.SaveChangesAsync();
    return Results.Ok(newProgram);
});

app.MapDelete("/api/data/clear", async(TrainingDb programDb) => {
    await programDb.Database.EnsureDeletedAsync();
    await programDb.Database.EnsureCreatedAsync();
    await programDb.SaveChangesAsync();
    return Results.Ok("Database successfully deleted.");
});

app.Run();
