using System.ComponentModel.DataAnnotations;

namespace HAOS.Models;
public class Workout {
    [Key]
    public int Id { get; set; }
    public string? Label { get; set; }
    public int RecommendedSets { get; set; }
    public int RecommendedReps { get; set; }
    public int RecommendedWeight { get; set; }
}