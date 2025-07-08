using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public enum ExerciseType
{
    Strength = 1,
    Endurance = 2
}

public class Exercise
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? DemoUrl { get; set; }
    public ExerciseType Type { get; set; }
}