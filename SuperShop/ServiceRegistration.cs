using SuperShop.IRepository;
using SuperShop.IService;
using SuperShop.Repository;
using SuperShop.Service;

namespace SuperShop
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
        }
    }
}
