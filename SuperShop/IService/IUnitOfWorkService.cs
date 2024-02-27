﻿namespace SuperShop.IService
{
    public interface IUnitOfWorkService
    {
        ISuperShopService SuperShopService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
