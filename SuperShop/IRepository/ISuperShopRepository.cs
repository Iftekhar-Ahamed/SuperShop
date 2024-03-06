using SuperShop.Model;

namespace SuperShop.IRepository
{
    public interface ISuperShopRepository
    {
        Task<long> CreateUserAsync(UserModel userModel);
        Task<long> UpdateUserAsync(UserModel userModel);
        Task<UserModel> GetUserByIdAsync(long UserId);
        Task<AllUserInformationViewModel> GetUserInformationByIdAsync(long UserId);
        Task<List<AllUserInformationViewModel>> GetAllUserAsync(GetDataConfigModel getDataConfigModel);
        Task<long> DeleteUserById(long Id);
        Task<long> CreateLogAsync(LogModel logModel);
        Task<long> SaveUserConnectionIdAsync(String ConnectionId,long UserId);
        Task<MenuModel?> GetMenuByIdAsync(long MenuId);
        Task<long> CreateMenuAsync(MenuModel menuModel);
        Task<long> UpdateMenuAsync(MenuModel menuModel);
        Task<int> CreateUserMenuPermissionAsync(MenuUserPermissionModel obj);
        Task<int> UpdateUserMenuPermissionAsync(MenuUserPermissionModel obj);
        Task<int> CreateItemTransactionTypeAsync(ItemTransactionTypeModel  itemTransactionTypeModel);
        Task<int> UpdateItemTransactionTypeAsync(ItemTransactionTypeModel itemTransactionTypeModel);
        Task<ItemTransactionTypeModel?> GetItemTransactionTypeAsync(long Id,bool? IsActive);
        Task<List<ItemTransactionTypeModel>?> GetAllItemTransactionTypeAsync();
        Task<List<MenuModel>> GetMenuPermissionByUserIdAsync(long UserId);
        Task<List<GetAllMenuPermissionModel>> GetAllMenuPermissionAsync(GetDataConfigModel getDataConfigModel);
        Task<GetAllMenuPermissionModel> GetMenuPermissionByIdAsync(long MenuPermissionId);
        Task<List<MenuModel>> GetAllMenusAsync(GetDataConfigModel getDataConfigModel);
        Task<List<UserTypeModel>> GetUserTypeAsync();
        Task<long> DeleteMenuById(long Id);
        Task<long> DeleteMenuPermissionByIdAsync(long Id);
        Task<List<MenuModel>> GetAllMenusForMenuPermissionAsync(GetDataConfigModel getDataConfigModel);
        Task<long> CreateItemTypeAsync(ItemTypeModel itemTypeModel);
        Task<long> DeleteItemTypeByIdAsync(long Id);
        Task<long> UpdateItemTypeAsync(ItemTypeModel itemTypeModel);
        Task<List<ItemTypeModel>> GetAllItemTypeAsync(GetDataConfigModel getDataConfigModel);
        Task<ItemTypeModel> GetItemTypeByIdAsync(long Id);
        Task<long> CreateItemAsync(ItemModel  item);
        Task<long> DeleteItemByIdAsync(long Id);
        Task<long> UpdateItemAsync(ItemModel itemModel);
        Task<List<ItemModel>> GetAllItemAsync(GetDataConfigModel getDataConfigModel);
        Task<ItemModel> GetItemByIdAsync(long Id);
    }
}
