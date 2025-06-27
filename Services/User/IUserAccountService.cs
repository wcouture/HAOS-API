using HAOS.Models.User;

namespace HAOS.Services.User;

public interface IUserAccountService
{
    Task<UserAccount> GetUserData(string username);
}