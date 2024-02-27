using SuperShop.IRepository;
using SuperShop.Model;

namespace SuperShop.IService
{
    public interface IAuthenticationService
    {
        Task<KeyValuePair<UserModel, MessageHelperModel>> LogInUser(string UserName, string PassWord);
    }
}
