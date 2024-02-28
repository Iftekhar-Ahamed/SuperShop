using SuperShop.Model;

namespace SuperShop.IService
{
    public interface INotificationService
    {
        Task<MessageHelperModel> SendNotificationToAll(String Message);
    }
}
