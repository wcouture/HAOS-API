namespace HAOS.Models.User;

public enum UserType
{
    Admin = 1,
    User = 2
}

public class UserAccount
{
    public int Id { get; set; }
    public UserType UserType { get; set; }

    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Password { get; set; }

    public List<int>? SubscribedPrograms { get; set; }
    public List<CompletedWorkout>? CompletedWorkouts { get; set; }
}