using SuperShop.IRepository;
using SuperShop.IService;

namespace SuperShop.Service
{
    public class UnitOfWorkService:IUnitOfWorkService 
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        public UnitOfWorkService(IUnitOfWorkRepository unitOfWorkRepository) { 
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        ISuperShopService IUnitOfWorkService.SuperShopService =>  new SuperShopService(_unitOfWorkRepository);
    }
}
