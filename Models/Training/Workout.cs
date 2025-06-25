using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;
public class Workout {
    [Key]
    public int Id { get; set; }
    public string? Description { get; set; }
    public int CircuitId { get; set; }
    public required Exercise Exercise_ { get; set; }
    
}