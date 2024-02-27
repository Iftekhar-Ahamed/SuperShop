using SuperShop.Model;

namespace SuperShop.IRepository
{
    public interface IAuthenticationRepository
    {
        Task<UserModel?> UserLogInAsync(string UserName, string PassWord);
    }
}
