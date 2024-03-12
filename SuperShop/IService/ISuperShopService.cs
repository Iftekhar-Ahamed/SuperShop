using SuperShop.Model;
using System;

namespace SuperShop.IService
{
    public interface ISuperShopService
    {
        Task<MessageHelperModel> CreateUser(UserModel userModel, long ActionBy);
        Task<MessageHelperModel> GetUserById(long UserId);
        Task<MessageHelperModel> DeleteUserById(long UserId, long ActionBy);
        Task<MessageHelperModel> GetUserInformationById(long UserId);
        Task<MessageHelperModel> GetAllUser(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> UpdateUserById(UserModel userModel, long ActionBy);
        Task<MessageHelperModel> CreateMenu(MenuModel menuModel, long ActionBy);
        Task<MessageHelperModel> UpdateMenu(MenuModel menuModel, long ActionBy);
        Task<MessageHelperModel> CreateUpdateUserMenuPermission(MenuUserPermissionModel menuUserPermissionModel, long ActionBy);
        Task<MessageHelperModel> CreateItemTransactionType(ItemTransactionTypeModel obj, long ActionBy);
        Task<MessageHelperModel> UpdateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel, long ActionBy);
        Task<MessageHelperModel> GeAlltItemTransactionType();
        Task<MessageHelperModel> GetMenuPermissionByUserId(long UserId);
        Task<MessageHelperModel> GetAllMenuPermission(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> GetMenuPermissionById(long MenuPermissionId);
        Task<MessageHelperModel> GetAllMenus(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> GetMenuById(long menuId);
        Task<MessageHelperModel> GetUserType();
        Task<MessageHelperModel> DeleteMenuById(long MenuId, long ActionBy);
        Task<MessageHelperModel> GetMenuDDL(long? UserId);
        Task<MessageHelperModel> GetUserDDL();
        Task<MessageHelperModel> DeleteMenuPermissionById(long PermissionId, long ActionBy);
        Task<MessageHelperModel> CreateItemType(ItemTypeModel itemTypeModel, long ActionBy);
        Task<MessageHelperModel> UpdateItemType(ItemTypeModel itemTypeModel, long ActionBy);
        Task<MessageHelperModel> DeleteItemType(long Id, long ActionBy);
        Task<MessageHelperModel> GetItemTypeById(long Id);
        Task<MessageHelperModel> GetAllItemType(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> GetItemTypeDDL();
        Task<MessageHelperModel> CreateItem(ItemModel item, long ActionBy);
        Task<MessageHelperModel> DeleteItemById(long Id, long ActionBy);
        Task<MessageHelperModel> UpdateItem(ItemModel item, long ActionBy);
        Task<MessageHelperModel> GetItemById(long Id);
        Task<MessageHelperModel> GetAllItem(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> GetItemDDL();
        Task<MessageHelperModel> DeleteItemTransactionTypeById(long Id, long ActionBy);
        Task<MessageHelperModel> GetItemTransactionTypeById(long Id);
        Task<MessageHelperModel> GetAllItemTransactionType(GetDataConfigModel getDataConfigModel);
        Task<MessageHelperModel> GetItemTransactionTypeDDL();
        Task<MessageHelperModel> MakeTransaction(ItemTransactionModel itemTransactionModel, UserModel ActionBy);
    }
}
