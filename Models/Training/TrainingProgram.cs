using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;
public class TrainingProgram {
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public IList<ProgramSegment> Segments { get; set; } = new List<ProgramSegment>();
}