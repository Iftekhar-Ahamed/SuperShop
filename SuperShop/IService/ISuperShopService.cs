using SuperShop.Model;
using System;

namespace SuperShop.IService
{
    public interface ISuperShopService
    {
        Task<MessageHelperModel> CreateUser(UserModel userModel, long ActionBy);
        Task<KeyValuePair<UserModel?, MessageHelperModel>> GetUserById(long UserId);
        Task<MessageHelperModel> DeleteUserById(long UserId, long ActionBy);
        Task<KeyValuePair<AllUserInformationViewModel?, MessageHelperModel>> GetUserInformationById(long UserId);
        Task<KeyValuePair<PaginationModel?, MessageHelperModel>> GetAllUser(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> UpdateUserById(UserModel userModel, long ActionBy);
        Task<MessageHelperModel> CreateLog(LogModel logModel);
        Task<MessageHelperModel> CreateMenu(MenuModel menuModel, long ActionBy);
        Task<MessageHelperModel> UpdateMenu(MenuModel menuModel, long ActionBy);
        Task<MessageHelperModel> CreateUpdateUserMenuPermission(MenuUserPermissionModel menuUserPermissionModel, long ActionBy);
        Task<MessageHelperModel> CreateItemTransactionType(ItemTransactionTypeModel obj, long ActionBy);
        Task<MessageHelperModel> UpdateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel, long ActionBy);
        Task<KeyValuePair<List<ItemTransactionTypeModel>?, MessageHelperModel>> GeAlltItemTransactionType();
        Task<KeyValuePair<List<MenuModel>, MessageHelperModel>> GetMenuPermissionByUserId(long UserId);
        Task<KeyValuePair<List<GetAllMenuPermissionModel>, MessageHelperModel>> GetAllMenuPermission(GetDataConfigModel getDataConfigModel);
        Task<KeyValuePair<GetAllMenuPermissionModel, MessageHelperModel>> GetMenuPermissionById(long MenuPermissionId);
        Task<KeyValuePair<List<MenuModel>, MessageHelperModel>> GetAllMenus(GetDataConfigModel getDataConfigModel);
        Task<KeyValuePair<MenuModel?, MessageHelperModel>> GetMenuById(long menuId);
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetUserType();
        Task<MessageHelperModel> DeleteMenuById(long MenuId, long ActionBy);
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetMenuDDL();
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetUserDDL();
    }
}
