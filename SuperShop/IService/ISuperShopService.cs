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
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetMenuDDL(long? UserId);
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetUserDDL();
        Task<MessageHelperModel> DeleteMenuPermissionById(long PermissionId, long ActionBy);
        Task<MessageHelperModel> CreateItemType(ItemTypeModel itemTypeModel, long ActionBy);
        Task<MessageHelperModel> UpdateItemType(ItemTypeModel itemTypeModel, long ActionBy);
        Task<MessageHelperModel> DeleteItemType(long Id, long ActionBy);
        Task<KeyValuePair<ItemTypeModel, MessageHelperModel>> GetItemTypeById(long Id);
        Task<KeyValuePair<List<ItemTypeModel>, MessageHelperModel>> GetAllItemType(GetDataConfigModel getDataConfigModel);
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetItemTypeDDL();
        Task<MessageHelperModel> CreateItem(ItemModel item, long ActionBy);
        Task<MessageHelperModel> DeleteItemById(long Id, long ActionBy);
        Task<MessageHelperModel> UpdateItem(ItemModel item, long ActionBy);
        Task<KeyValuePair<ItemModel, MessageHelperModel>> GetItemById(long Id);
        Task<KeyValuePair<List<ItemModel>, MessageHelperModel>> GetAllItem(GetDataConfigModel getDataConfigModel);
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetItemDDL();
        Task<MessageHelperModel> DeleteItemTransactionTypeById(long Id, long ActionBy);
        Task<KeyValuePair<ItemTransactionTypeModel, MessageHelperModel>> GetItemTransactionTypeById(long Id);
        Task<KeyValuePair<List<ItemTransactionTypeModel>, MessageHelperModel>> GetAllItemTransactionType(GetDataConfigModel getDataConfigModel);
        Task<KeyValuePair<List<CommonDDL>, MessageHelperModel>> GetItemTransactionTypeDDL();
        Task<MessageHelperModel> MakeTransaction(ItemTransactionModel itemTransactionModel, long ActionBy);
    }
}
