using System.ComponentModel.DataAnnotations;

namespace HAOS.Models;

public class ProgramSegment {
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public List<ProgramDay> Days { get; set; }
}