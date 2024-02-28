using SuperShop.IRepository;
using SuperShop.Model;

namespace SuperShop.IService
{
    public interface IAuthenticationService
    {
        Task<KeyValuePair<UserModel, MessageHelperModel>> LogInUser(string UserName, string PassWord);
        string GenerateToken(UserModel user,string type);
        Task<MessageHelperModel> GetNewAccessToken(UserModel userModel);
    }
}
