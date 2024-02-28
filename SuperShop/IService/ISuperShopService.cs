using SuperShop.Model;
using System;

namespace SuperShop.IService
{
    public interface ISuperShopService
    {
        Task<MessageHelperModel> CreateUser(UserModel userModel, long ActionBy);
        Task<KeyValuePair<UserModel?, MessageHelperModel>> GetUserById(long UserId);
        Task<MessageHelperModel> UpdateUserById(UserModel userModel, long ActionBy);
        Task<MessageHelperModel> CreateLog(LogModel logModel);
        Task<MessageHelperModel> CreateMenu(MenuModel menuModel, long ActionBy);
        Task<MessageHelperModel> UpdateMenu(MenuModel menuModel, long ActionBy);
        Task<MessageHelperModel> UserMenuPermission(MenuUserPermissionModel menuUserPermissionModel, long ActionBy);
        Task<MessageHelperModel> CreateItemTransactionType(ItemTransactionTypeModel obj, long ActionBy);
        Task<MessageHelperModel> UpdateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel, long ActionBy);
        Task<KeyValuePair<List<ItemTransactionTypeModel>?, MessageHelperModel>> GeAlltItemTransactionType();
    }
}
