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
        public async Task<KeyValuePair<UserModel?, MessageHelperModel>> GetUserById(long UserId)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetUserByIdAsync(UserId);
            var msg = new MessageHelperModel();
            if (res != null)
            {
                res.Password = "";
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Invalid User Id";
                msg.StatusCode = 400;
            }
            return KeyValuePair.Create(res,msg);
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
        public async Task<MessageHelperModel> UpdateUserById(UserModel userModel, long ActionBy)
        {
            var previous=  await _unitOfWorkRepository.SuperShopRepository.GetUserByIdAsync(userModel.Id??0);
            var msg = new MessageHelperModel();

            if (previous != null)
            {
                var res = await _unitOfWorkRepository.SuperShopRepository.UpdateUserAsync(userModel);
                
                if (res != 0)
                {
                    var log = new LogModel
                    {
                        TableId = 1,
                        ActionBy = ActionBy,
                        ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,userModel),
                        JsonPayload = JsonSerializer.Serialize(userModel),
                        ActionDate = DateTime.Now,
                        IsActive = true,
                        ActionType = "Update"
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
            msg.Message = "No User Found in given id";
            msg.StatusCode= 400;
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
                msg.Message = "Faild to "+OperationType;
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> CreateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.CreateItemTransactionTypeAsync(itemTransactionTypeModel);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 3,
                    ActionBy = ActionBy,
                    ActionChanges = "New Item Transaction Type Added: "+itemTransactionTypeModel.TransactionName,
                    JsonPayload = JsonSerializer.Serialize(itemTransactionTypeModel),
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
        public async Task<MessageHelperModel> UpdateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel, long ActionBy)
        {
            var previous = await _unitOfWorkRepository.SuperShopRepository.GetItemTransactionTypeAsync(itemTransactionTypeModel.Id,null);
            var res = await _unitOfWorkRepository.SuperShopRepository.UpdateItemTransactionTypeAsync(itemTransactionTypeModel);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 3,
                    ActionBy = ActionBy,
                    ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,itemTransactionTypeModel),
                    JsonPayload = JsonSerializer.Serialize(itemTransactionTypeModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Update"
                };

                await CreateLog(log);
                msg.Message = "Sucessfully Updated";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Update";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<KeyValuePair<List<ItemTransactionTypeModel>?,MessageHelperModel>> GeAlltItemTransactionType()
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemTransactionTypeAsync();
            var msg = new MessageHelperModel();
            if (res != null)
            {
                msg.Message = "Sucessfully Get";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Get";
                msg.StatusCode = 400;
            }
            return KeyValuePair.Create(res,msg);
        }

    }
}
