using System.ComponentModel.DataAnnotations;

namespace HAOS.Models;

public class ProgramDay {
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public int WeekNum { get; set; }
    public List<Circuit> Circuits { get; set; }
}