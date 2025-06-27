using HAOS.Models.User;

namespace HAOS.Services.User;

public interface IUserAccountService
{
    Task<IList<UserAccount>> GetAllUsers();
    Task<UserAccount> AddUser(UserAccount user);
    Task<UserAccount> Authenticate(UserAccount user);
    Task<UserAccount> GetUserInfo(int id);
    Task<UserAccount> UpdateUserInfo(UserAccount user, int id);
    Task<UserAccount> DeleteUser(int id);
}