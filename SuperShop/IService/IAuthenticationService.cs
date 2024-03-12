using SuperShop.IRepository;
using SuperShop.Model;

namespace SuperShop.IService
{
    public interface IAuthenticationService
    {
        Task<MessageHelperModel> LogInUser(string UserName, string PassWord);
        string GenerateToken(UserModel user,string type);
        MessageHelperModel GetNewAccessToken(UserModel userModel);
    }
}
