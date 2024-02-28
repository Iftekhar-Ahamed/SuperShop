using SuperShop.Model;

namespace SuperShop.IRepository
{
    public interface ISuperShopRepository
    {
        Task<long> CreateUserAsync(UserModel userModel);
        Task<long> UpdateUserAsync(UserModel userModel);
        Task<UserModel> GetUserByIdAsync(long UserId);
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
    }
}
