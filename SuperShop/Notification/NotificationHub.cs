using Microsoft.AspNetCore.SignalR;
using SuperShop.IRepository;
using SuperShop.Repository;

namespace SuperShop.Notification;

public class NotificationHub : Hub
{
    private readonly IUnitOfWorkRepository _unitOfWorkRepository;
    public NotificationHub(IUnitOfWorkRepository unitOfWorkRepository)
    {
        _unitOfWorkRepository = unitOfWorkRepository;
    }
    public async Task SendNotificationToAll(string message)
    {
        await Clients.All.SendAsync("broadcastMessage", message);
    }
    public async Task SendNotificationToClient(string message)
    {
        try
        {
            await Clients.Client("").SendAsync("ReceivedPersonalNotification", message);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public override Task OnConnectedAsync()
    {
        Clients.Caller.SendAsync("OnConnected");
        return base.OnConnectedAsync();
    }
    public async Task SaveUserConnection(long UserId)
    {
        await _unitOfWorkRepository.SuperShopRepository.SaveUserConnectionIdAsync("", UserId);
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {

    }

}