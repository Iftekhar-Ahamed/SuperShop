using SuperShop.Model;

namespace SuperShop.IService
{
    public interface ILogService
    {
        String UpdateDifference(dynamic Previous,dynamic New);
        Task<MessageHelperModel> CreateLog(LogModel logModel);
        Task<MessageHelperModel> GetAllLog(GetLogModel logModel);
        Task<MessageHelperModel> GetItemLog(GetLogModel logModel);
    }
}
