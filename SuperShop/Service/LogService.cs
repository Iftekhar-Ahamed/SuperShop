using SuperShop.IRepository;
using SuperShop.IService;
using SuperShop.Model;
using System;
using System.Reflection;

namespace SuperShop.Service
{
    public class LogService : ILogService
    {
        private IUnitOfWorkRepository _unitOfWorkRepository;
        private IUnitOfWorkService _unitOfWorkService;
        public LogService(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<MessageHelperModel> GetAllLog(GetLogModel logModel)
        {
            var res = await _unitOfWorkRepository.LogRepository.GetAllLogAsync(logModel);
            var msg = new MessageHelperModel();

            if (res.Count != 0)
            {
                msg.data = res;
                msg.Message = "Successfully Log Created";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild Log Created";
                msg.StatusCode = 400;
            }

            return msg;
        }
        public async Task<MessageHelperModel> GetItemLog(GetLogModel logModel)
        {
            var res = await _unitOfWorkRepository.LogRepository.GetItemLog(logModel);
            var msg = new MessageHelperModel();

            List<string> Logs = new List<string>();

            foreach (var item in res)
            {
                String temp = item.ActionChanges + " UA : "+item.UnitOfAmount.ToString() + " UP : "+item.UnitPrice.ToString()+ " Total : "+item.Total.ToString();
                Logs.Add(temp);
            }

            if (res.Count != 0)
            {
                msg.data = Logs;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild";
                msg.StatusCode = 400;
            }

            return msg;
        }
        public async Task<MessageHelperModel> CreateLog(LogModel logModel)
        {
            var res = await _unitOfWorkRepository.LogRepository.CreateLogAsync(logModel);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                msg.Message = "Successfully Log Created";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild Log Created";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public string UpdateDifference(dynamic Previous, dynamic New)
        {
            string res = string.Empty;

            Type type = Previous.GetType();

            foreach (PropertyInfo property in type.GetProperties())
            {
                object originalValue = property.GetValue(Previous, null);
                object newValue = property.GetValue(New, null);

                if (!object.Equals(originalValue, newValue))
                {
                    string originalText = (originalValue != null) ?
                        originalValue.ToString() : "[NULL]";

                    string newText = (newValue != null) ?
                        newValue.ToString() : "[NULL]";

                    res += $"{property.Name}: {originalText} Changed Into : {newText}\n";
                }
            }

            return res;
        }
    }
}
