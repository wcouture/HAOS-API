namespace HAOS.Models.User;

public class CompletedWorkout
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int WorkoutId { get; set; }
    public DateTime CompletedDate { get; set; }
    public int WeightUsed { get; set; }
    public int Duration { get; set; }
}