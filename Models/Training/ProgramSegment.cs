using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public class ProgramSegment {
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public int ProgramId { get; set; }
    public IList<ProgramDay> Days { get; set; } = new List<ProgramDay>();
}