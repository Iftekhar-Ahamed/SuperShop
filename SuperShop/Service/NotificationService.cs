using Microsoft.AspNetCore.SignalR;
using SuperShop.IService;
using SuperShop.Model;
using SuperShop.Notification;

namespace SuperShop.Service
{
    public class NotificationService:INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub> hubContext) { 
            _hubContext = hubContext;
        }

        public async Task<MessageHelperModel> SendNotificationToAll(string Message)
        {
            try
            {
                await _hubContext.Clients.All.SendAsync("broadcastMessage", Message);
                return new MessageHelperModel
                {
                    Message = "Sucessfully Send",
                    StatusCode = 200
                };

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Faild to Send Notification");
            }
        }
    }
}
