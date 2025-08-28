using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public enum TrackingType
{
    Reps = 1,
    Time = 2,
    Distance = 3,
    Rounds = 4,
    Weight = 5,
    Calories = 6,
    Completed = 7
}

public class Workout {
    [Key]
    public int Id { get; set; }
    public int Rounds { get; set; }
    public string? Description { get; set; }
    public int CircuitId { get; set; }
    public required Exercise Exercise_ { get; set; }
    public required TrackingType TrackingType_ { get; set; }
}