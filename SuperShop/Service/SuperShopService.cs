using SuperShop.IRepository;
using SuperShop.IService;
using SuperShop.Model;
using System.Text.Json;

namespace SuperShop.Service
{
    public class SuperShopService:ISuperShopService
    {
        private IUnitOfWorkRepository _unitOfWorkRepository;
        public SuperShopService(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task<MessageHelperModel> CreateLog(LogModel logModel)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.CreateLogAsync(logModel);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                msg.Message = "Sucessfully Log Created";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild Log Created";
                msg.StatusCode = 400;
            }
            return msg;
        }

        public async Task<MessageHelperModel> CreateUser(UserModel userModel,long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.CreateUserAsync(userModel);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 1,
                    ActionBy = ActionBy,
                    ActionChanges = "New User " + userModel.UserName + " Created ",
                    JsonPayload = JsonSerializer.Serialize(userModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Create"
                };

                await CreateLog(log);

                msg.Message = "Sucessfully Created";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Created";
                msg.StatusCode = 400;
            }
            return msg;
        }
    }
}
