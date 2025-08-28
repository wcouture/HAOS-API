namespace HAOS.Models.Training;

public class Session {
    public int Id { get; set; }
    public string? Title { get; set; }
    public int ProgramDayId { get; set; }
    public IList<Circuit> Circuits { get; set; } = new List<Circuit>();
}