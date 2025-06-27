using HAOS.Models.User;

namespace HAOS.Services.User;

public interface IUserDataService
{
    Task<UserAccount> GetUserData(string username);
}