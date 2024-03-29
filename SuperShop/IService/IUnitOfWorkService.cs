﻿namespace SuperShop.IService
{
    public interface IUnitOfWorkService
    {
        ISuperShopService SuperShopService { get; }
        IAuthenticationService AuthenticationService { get; }
        ILogService LogService { get; }
        INotificationService NotificationService { get; }
    }
}
