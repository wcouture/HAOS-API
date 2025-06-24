using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public class Circuit {
    [Key]
    public int Id { get; set; }
    public List<Workout>? Workouts { get; set; }
}