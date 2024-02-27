using SuperShop.IRepository;
using SuperShop.IService;

namespace SuperShop.Service
{
    public class SuperShopService:ISuperShopService
    {
        private IUnitOfWorkRepository _unitOfWorkRepository;
        public SuperShopService(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
    }
}
