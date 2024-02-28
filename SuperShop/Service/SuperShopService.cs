using SuperShop.IRepository;
using SuperShop.IService;
using SuperShop.Model;
using System;
using System.Text.Json;

namespace SuperShop.Service
{
    public class SuperShopService:ISuperShopService
    {
        private IUnitOfWorkRepository _unitOfWorkRepository;
        private IUnitOfWorkService _unitOfWorkService;
        public SuperShopService(IUnitOfWorkRepository unitOfWorkRepository,IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _unitOfWorkService = unitOfWorkService;
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
        public async Task<MessageHelperModel> CreateMenu(MenuModel menuModel,long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.CreateMenuAsync(menuModel);
            var msg = new MessageHelperModel();
            if(res != 0)
            {
                var log = new LogModel
                {
                    TableId = 2,
                    ActionBy = ActionBy,
                    ActionChanges = "New Menu " + menuModel.MenuName + " Created ",
                    JsonPayload = JsonSerializer.Serialize(menuModel),
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
        public async Task<MessageHelperModel> UpdateMenu(MenuModel menuModel, long ActionBy)
        {
            var previous = await _unitOfWorkRepository.SuperShopRepository.GetMenuByIdAsync(menuModel.Id);
            var res = await _unitOfWorkRepository.SuperShopRepository.UpdateMenuAsync(menuModel);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 2,
                    ActionBy = ActionBy,
                    ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,menuModel),
                    JsonPayload = JsonSerializer.Serialize(menuModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Updated"
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
        public async Task<MessageHelperModel> UserMenuPermission(MenuUserPermissionModel menuUserPermissionModel, long ActionBy)
        {
            string OperationType = "";
            var res = 0;

            if (menuUserPermissionModel.Id != 0)
            {
                OperationType = "Update";
                res = await _unitOfWorkRepository.SuperShopRepository.UpdateUserMenuPermissionAsync(menuUserPermissionModel);
            }
            else
            {
                OperationType = "Crate";
                res = await _unitOfWorkRepository.SuperShopRepository.CreateUserMenuPermissionAsync(menuUserPermissionModel);
            }
            
            
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 2,
                    ActionBy = ActionBy,
                    ActionChanges = "User : " + menuUserPermissionModel.UserId.ToString() + (menuUserPermissionModel.IsActive==true?" Get":" Lost")+" menu :" + menuUserPermissionModel.MenuId + " Permission",
                    JsonPayload = JsonSerializer.Serialize(menuUserPermissionModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = OperationType
                };

                await CreateLog(log);
                msg.Message = "Sucessfully "+OperationType;
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
