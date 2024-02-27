namespace SuperShop.IRepository
{
    public interface IUnitOfWorkRepository
    {
        ISuperShopRepository SuperShopRepository { get; }
    }
}
