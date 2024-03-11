using SuperShop.Model;

namespace SuperShop.IRepository
{
    public interface ILogRepository
    {
        Task<long> CreateLogAsync(LogModel logModel);
        Task<LogModel> GetLogByIdAsync(GetLogModel logModel);
        Task<List<LogModel>> GetAllLogAsync(GetLogModel logModel);
        Task<List<ItemTransactionLogModel>> GetItemLog(GetLogModel logModel);
    }
}
