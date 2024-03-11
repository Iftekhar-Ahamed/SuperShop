using SuperShop.IRepository;

namespace SuperShop.Repository
{
    public class UnitOfWorkRepository:IUnitOfWorkRepository
    {
        private readonly IConfiguration _configuration;
        public UnitOfWorkRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        ISuperShopRepository IUnitOfWorkRepository.SuperShopRepository => new SuperShopRepository(_configuration);

        IAuthenticationRepository IUnitOfWorkRepository.AuthenticationRepository => new AuthenticationRepository(_configuration);
        ILogRepository IUnitOfWorkRepository.LogRepository => new LogRepository(_configuration);
    }
}
