using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public class Circuit {
    [Key]
    public int Id { get; set; }
    public int Rounds { get; set; }
    public string? Description { get; set; }
    public int ProgramDayId { get; set; }
    public IList<Workout> Workouts { get; set; } = new List<Workout>();
}