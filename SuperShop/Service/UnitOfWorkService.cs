using SuperShop.IRepository;
using SuperShop.IService;

namespace SuperShop.Service
{
    public class UnitOfWorkService:IUnitOfWorkService 
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IConfiguration _configuration;
        public UnitOfWorkService(IUnitOfWorkRepository unitOfWorkRepository, IConfiguration configuration) { 
            _unitOfWorkRepository = unitOfWorkRepository;
            _configuration = configuration;
        }

        ISuperShopService IUnitOfWorkService.SuperShopService =>  new SuperShopService(_unitOfWorkRepository);
        IAuthenticationService IUnitOfWorkService.AuthenticationService =>  new AuthenticationService(_unitOfWorkRepository,_configuration);
    }
}
