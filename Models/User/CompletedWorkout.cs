using HAOS.Models.Training;

namespace HAOS.Models.User;

public class CompletedWorkout
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int WorkoutId { get; set; }
    public DateTime CompletedDate { get; set; }
    public TrackingType TrackingType_ { get; set; }
    public IList<int> Metrics { get; set; } = new List<int>();
}