using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public class Exercise
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? DemoUrl { get; set; }
    public bool ShowDemo { get; set; }
    public bool PremiumDemo { get; set; }
    public ExerciseIcon Icon { get; set; }
}

public enum ExerciseIcon
{
    Dumbbell,
    RunningShoe,
    YogaMat,
    Swimming,
    JumpRope,
    Kettlebell,
    Barbell,
    Bicycle
}