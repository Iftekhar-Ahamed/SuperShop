using Microsoft.AspNetCore.SignalR;
using SuperShop.IRepository;
using SuperShop.IService;
using SuperShop.Notification;

namespace SuperShop.Service
{
    public class UnitOfWorkService:IUnitOfWorkService 
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<NotificationHub> _hubContext;
        public UnitOfWorkService(IUnitOfWorkRepository unitOfWorkRepository, IConfiguration configuration, IHubContext<NotificationHub> hubContext) { 
            _unitOfWorkRepository = unitOfWorkRepository;
            _configuration = configuration;
            _hubContext = hubContext;
        }

        ISuperShopService IUnitOfWorkService.SuperShopService =>  new SuperShopService(_unitOfWorkRepository,this);
        IAuthenticationService IUnitOfWorkService.AuthenticationService =>  new AuthenticationService(_unitOfWorkRepository,_configuration);
        ILogService IUnitOfWorkService.LogService => new LogService(_unitOfWorkRepository);
        INotificationService IUnitOfWorkService.NotificationService => new NotificationService(_hubContext);
    }
}
