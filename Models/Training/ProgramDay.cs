using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public class ProgramDay {
    [Key]
    public int Id { get; set; }
    public string? Title { get; set; }
    public int WeekNum { get; set; }
    public int SegmentId { get; set; }
    public IList<Circuit> Circuits { get; set; } = new List<Circuit>();
}