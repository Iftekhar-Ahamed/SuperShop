using SuperShop.Model;
using System;

namespace SuperShop.IService
{
    public interface ISuperShopService
    {
        Task<MessageHelperModel> CreateUser(UserModel userModel, long ActionBy);
        Task<MessageHelperModel> CreateLog(LogModel logModel);
    }
}
