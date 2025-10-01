using System.ComponentModel.DataAnnotations;

namespace HAOS.Models.Training;

public class Exercise
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? DemoUrl { get; set; }
    public bool ShowDemo { get; set; }
    public bool PremiumDemo { get; set; }
}