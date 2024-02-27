using SuperShop.Model;

namespace SuperShop.IRepository
{
    public interface ISuperShopRepository
    {
        Task<long> CreateUserAsync(UserModel userModel);
        Task<long> CreateLogAsync(LogModel logModel);
    }
}
