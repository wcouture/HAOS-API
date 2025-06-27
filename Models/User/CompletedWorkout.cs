namespace HAOS.Models.User;

public class CompletedWorkout
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int WorkoutId { get; set; }
}