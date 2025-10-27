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
    Dumbbell = 1,
    RunningShoe = 2,
    YogaMat = 3,
    Swimming = 4,
    JumpRope = 5,
    Kettlebell = 6,
    Barbell = 7,
    Bicycle = 8
}