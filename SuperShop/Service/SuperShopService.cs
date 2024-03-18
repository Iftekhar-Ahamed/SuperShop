using Microsoft.AspNetCore.Mvc;
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

        
        public async Task<MessageHelperModel> GetUserById(long UserId)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetUserByIdAsync(UserId);
            var msg = new MessageHelperModel();
            if (res != null)
            {
                res.Password = "";
                msg.Message = "Successfull";
                msg.StatusCode = 200;
                msg.data = res;
            }
            else
            {
                msg.Message = "Invalid User Id";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> DeleteUserById(long UserId, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.DeleteUserById(UserId);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                LogModel log = new LogModel
                {
                    TableId = 1,
                    TablePk = UserId,
                    ActionBy = ActionBy,
                    ActionChanges = "User " + UserId.ToString() + " Deleted ",
                    JsonPayload = JsonSerializer.Serialize(UserId),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Delete"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to delete";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetUserInformationById(long UserId)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetUserInformationByIdAsync(UserId);
            var msg = new MessageHelperModel();
            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Invalid User Id";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetAllUser(GetDataConfigModel getDataConfigModel)
        {
            PaginationModel res = new PaginationModel();
            res.data = await _unitOfWorkRepository.SuperShopRepository.GetAllUserAsync(getDataConfigModel);
            var msg = new MessageHelperModel();
            if (res.data != null)
            {
                res.total = await _unitOfWorkRepository.SuperShopRepository.GetAllUserCountAsync(getDataConfigModel);
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Invalid User Id";
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
                    TablePk = res,
                    ActionBy = ActionBy,
                    ActionChanges = "New User " + userModel.UserName + " Created ",
                    JsonPayload = JsonSerializer.Serialize(userModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Create"
                };

                await _unitOfWorkService.LogService.CreateLog(log);

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
                        TablePk = userModel.Id??0,
                        ActionBy = ActionBy,
                        ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,userModel),
                        JsonPayload = JsonSerializer.Serialize(userModel),
                        ActionDate = DateTime.Now,
                        IsActive = true,
                        ActionType = "Update"
                    };

                    await _unitOfWorkService.LogService.CreateLog(log);

                    msg.Message = "Sucessfully Update";
                    msg.StatusCode = 200;
                }
                else
                {
                    msg.Message = "Faild to Update";
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
                    TablePk = res,
                    ActionBy = ActionBy,
                    ActionChanges = "New Menu " + menuModel.MenuName + " Created ",
                    JsonPayload = JsonSerializer.Serialize(menuModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Create"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
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
            if (res != 0 && previous !=null )
            {
                var log = new LogModel
                {
                    TableId = 2,
                    TablePk = menuModel.Id,
                    ActionBy = ActionBy,
                    ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,menuModel),
                    JsonPayload = JsonSerializer.Serialize(menuModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Updated"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Sucessfully Updated";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Updated";
                msg.StatusCode = 400;
            }
            return msg;

        }
        public async Task<MessageHelperModel> CreateUpdateUserMenuPermission(MenuUserPermissionModel menuUserPermissionModel, long ActionBy)
        {
            string OperationType = "";
            var res = 0;

            if (menuUserPermissionModel.Id != 0)
            {
                OperationType = "Update";
                res = await _unitOfWorkRepository.SuperShopRepository.UpdateUserMenuPermissionAsync(menuUserPermissionModel);
                if(res!=0)res = menuUserPermissionModel.Id;
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
                    TablePk = res,
                    ActionBy = ActionBy,
                    ActionChanges = "User : " + menuUserPermissionModel.UserId.ToString() + (menuUserPermissionModel.IsActive==true?" Get":" Lost")+" menu :" + menuUserPermissionModel.MenuId + " Permission",
                    JsonPayload = JsonSerializer.Serialize(menuUserPermissionModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = OperationType
                };

                await _unitOfWorkService.LogService.CreateLog(log);
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
                    TablePk = res,
                    ActionBy = ActionBy,
                    ActionChanges = "New Item Transaction Type Added: "+itemTransactionTypeModel.TransactionName,
                    JsonPayload = JsonSerializer.Serialize(itemTransactionTypeModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Create"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
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
            if (res != 0 && previous != null)
            {
                var log = new LogModel
                {
                    TableId = 3,
                    TablePk = itemTransactionTypeModel.Id,
                    ActionBy = ActionBy,
                    ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,itemTransactionTypeModel),
                    JsonPayload = JsonSerializer.Serialize(itemTransactionTypeModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Update"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
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
        public async Task<MessageHelperModel> GeAlltItemTransactionType()
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemTransactionTypeAsync();
            var msg = new MessageHelperModel();
            if (res != null)
            {
                msg.Message = "Sucessfully Get";
                msg.StatusCode = 200;
                msg.data = res;
            }
            else
            {
                msg.Message = "Faild to Get";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetMenuPermissionByUserId(long UserId)
        {
            var msg = new MessageHelperModel();
            var res = await _unitOfWorkRepository.SuperShopRepository.GetMenuPermissionByUserIdAsync(UserId);
            if (res.Count > 0)
            {
                msg.Message = "Sucessfull";
                msg.StatusCode = 200;
                msg.data = res;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetAllMenuPermission(GetDataConfigModel getDataConfigModel)
        {
            PaginationModel res = new PaginationModel();
            res.data = await _unitOfWorkRepository.SuperShopRepository.GetAllMenuPermissionAsync(getDataConfigModel);
            var msg = new MessageHelperModel();
            if (res.data != null)
            {
                res.total = await _unitOfWorkRepository.SuperShopRepository.GetAllMenuPermissionCountAsync(getDataConfigModel);
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetMenuPermissionById(long MenuPermissionId)
        {
            var msg = new MessageHelperModel();
            var res = await _unitOfWorkRepository.SuperShopRepository.GetMenuPermissionByIdAsync(MenuPermissionId);
            if (res != null)
            {
                msg.data = res;
                msg.Message = "Sucessfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetUserType()
        {
            var msg = new MessageHelperModel();
            var res = await _unitOfWorkRepository.SuperShopRepository.GetUserTypeAsync();

            List<CommonDDL> commonDDLs = new List<CommonDDL>();

            foreach (var item in res)
            {
                commonDDLs.Add(new CommonDDL { Name = item.UserTypeName, Value = item.Id });
            }
            msg.Message = "Sucessfull";
            msg.StatusCode = 200;
            msg.data = commonDDLs;
            return msg;
        }
        public async Task<MessageHelperModel> GetAllMenus(GetDataConfigModel getDataConfigModel)
        {
            var msg = new MessageHelperModel();
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllMenusAsync(getDataConfigModel);
            if(res.Count != 0)
            {
                msg.Message = "Sucessfull";
                msg.StatusCode = 200;
                msg.data = res;
            }
            else
            {
                msg.Message = "No Menus Found";
                msg.StatusCode = 200;
            }
            
            return msg;
        }
        public async Task<MessageHelperModel> GetMenuById(long menuId)
        {
            var msg = new MessageHelperModel();
            var res = await _unitOfWorkRepository.SuperShopRepository.GetMenuByIdAsync(menuId);
            if (res != null)
            {
                msg.Message = "Sucessfull";
                msg.StatusCode = 200;
                msg.data = res;
            }
            else
            {
                msg.Message = "Not Menu Found";
                msg.StatusCode = 400;
            }

            return msg;
        }
        public async Task<MessageHelperModel> DeleteMenuById(long MenuId, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.DeleteMenuById(MenuId);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 2,
                    TablePk = MenuId,
                    ActionBy = ActionBy,
                    ActionChanges = "Menu : " + MenuId.ToString() + " Deleted ",
                    JsonPayload = JsonSerializer.Serialize(MenuId),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Delete"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to delete";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> DeleteMenuPermissionById(long PermissionId, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.DeleteMenuPermissionByIdAsync(PermissionId);
            var msg = new MessageHelperModel();
            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 2,
                    TablePk=PermissionId,
                    ActionBy = ActionBy,
                    ActionChanges = "Menu Permission : " + PermissionId.ToString() + " Deleted ",
                    JsonPayload = JsonSerializer.Serialize(PermissionId),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Delete"
                };

                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to delete";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetMenuDDL(long? UserId)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllMenusForMenuPermissionAsync(new GetDataConfigModel{ IsActive=true,SearchTerm = UserId.ToString()??""});
            var msg = new MessageHelperModel();
            List<CommonDDL> commonDDLs = new List<CommonDDL>();

            foreach (var item in res)
            {
                CommonDDL commonDDL = new CommonDDL();
                commonDDL.Name = item.MenuName+"["+item.Id.ToString()+"]";
                commonDDL.Value = item.Id;
                commonDDLs.Add(commonDDL);
            }

            if (res.Count != 0)
            {
                msg.data = commonDDLs;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetUserDDL()
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllUserAsync(new GetDataConfigModel { IsActive = true });
            var msg = new MessageHelperModel();
            List<CommonDDL> commonDDLs = new List<CommonDDL>();

            foreach (var item in res)
            {
                CommonDDL commonDDL = new CommonDDL();
                commonDDL.Name = item.UserFullName + "[" + item.Id.ToString() + "]";
                commonDDL.Value = item.Id??0;
                commonDDLs.Add(commonDDL);
            }

            if (res.Count != 0)
            {
                msg.data = commonDDLs;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> CreateItemType(ItemTypeModel  itemTypeModel,long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.CreateItemTypeAsync(itemTypeModel);
            var msg = new MessageHelperModel();

            if (res!= 0)
            {
                var log = new LogModel
                {
                    TableId = 10002,
                    TablePk = res,
                    ActionBy = ActionBy,
                    ActionChanges = "New " + itemTypeModel.ItemTypeName.ToString() + " Item Type Created",
                    JsonPayload = JsonSerializer.Serialize(itemTypeModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Create"
                };
                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfully Created";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Create";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> DeleteItemType(long Id, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.DeleteItemTypeByIdAsync(Id);
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 10002,
                    TablePk = Id,
                    ActionBy = ActionBy,
                    ActionChanges = "Item Type " + Id.ToString() + "  Deleted",
                    JsonPayload = JsonSerializer.Serialize(Id),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Delete"
                };
                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfully Deleted";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Deleted";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> UpdateItemType(ItemTypeModel itemTypeModel, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.UpdateItemTypeAsync(itemTypeModel);
            var previous = await _unitOfWorkRepository.SuperShopRepository.GetItemTypeByIdAsync(itemTypeModel.Id);
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 10002,
                    TablePk = itemTypeModel.Id,
                    ActionBy = ActionBy,
                    ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous,itemTypeModel),
                    JsonPayload = JsonSerializer.Serialize(itemTypeModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Update"
                };
                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfully Updated";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Updated";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetItemTypeById(long Id)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetItemTypeByIdAsync(Id);
            var msg = new MessageHelperModel();

            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetAllItemType(GetDataConfigModel getDataConfigModel)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemTypeAsync(getDataConfigModel);
            var msg = new MessageHelperModel();

            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetItemTypeDDL()
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemTypeAsync(new GetDataConfigModel { IsActive = true });
            var msg = new MessageHelperModel();
            List<CommonDDL> commonDDLs = new List<CommonDDL>();

            foreach (var item in res)
            {
                CommonDDL commonDDL = new CommonDDL();
                commonDDL.Name = item.ItemTypeName;
                commonDDL.Value = item.Id;
                commonDDLs.Add(commonDDL);
            }

            if (res.Count != 0)
            {
                msg.data = commonDDLs;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }





        public async Task<MessageHelperModel> CreateItem(ItemModel item, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.CreateItemAsync(item);
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 10003,
                    TablePk = res,
                    ActionBy = ActionBy,
                    ActionChanges = "New " + item.ItemName.ToString() + " Item Created",
                    JsonPayload = JsonSerializer.Serialize(item),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Create"
                };
                var r = _unitOfWorkService.LogService.CreateLog(log);

                msg.Message = "Successfully Created";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Create";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> DeleteItemById(long Id, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.DeleteItemByIdAsync(Id);
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 10003,
                    TablePk = Id,
                    ActionBy = ActionBy,
                    ActionChanges = "Item " + Id.ToString() + "  Deleted",
                    JsonPayload = JsonSerializer.Serialize(Id),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Delete"
                };
                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfully Deleted";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Deleted";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> UpdateItem(ItemModel item, long ActionBy)
        {
            var previous = await _unitOfWorkRepository.SuperShopRepository.GetItemByIdAsync(item.Id);
            var res = await _unitOfWorkRepository.SuperShopRepository.UpdateItemAsync(item);
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 10003,
                    TablePk = item.Id,
                    ActionBy = ActionBy,
                    ActionChanges = _unitOfWorkService.LogService.UpdateDifference(previous, item),
                    JsonPayload = JsonSerializer.Serialize(item),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Update"
                };
                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfully Updated";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Updated";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetItemById(long Id)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetItemByIdAsync(Id);
            var msg = new MessageHelperModel();

            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetAllItem(GetDataConfigModel getDataConfigModel)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemAsync(getDataConfigModel);
            var msg = new MessageHelperModel();

            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetItemDDL()
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemAsync(new GetDataConfigModel { IsActive = true });
            var msg = new MessageHelperModel();
            List<CommonDDL> commonDDLs = new List<CommonDDL>();

            foreach (var item in res)
            {
                CommonDDL commonDDL = new CommonDDL();
                commonDDL.Name = item.ItemName;
                commonDDL.Value = item.Id;
                commonDDLs.Add(commonDDL);
            }

            if (res.Count != 0)
            {
                msg.data = commonDDLs;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> DeleteItemTransactionTypeById(long Id, long ActionBy)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.DeleteItemTransactionTypeByIdAsync(Id);
            var msg = new MessageHelperModel();

            if (res != 0)
            {
                var log = new LogModel
                {
                    TableId = 3,
                    TablePk = Id,
                    ActionBy = ActionBy,
                    ActionChanges = "Item Transaction Type" + Id.ToString() + "  Deleted",
                    JsonPayload = JsonSerializer.Serialize(Id),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = "Delete"
                };
                await _unitOfWorkService.LogService.CreateLog(log);
                msg.Message = "Successfully Deleted";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "Faild to Deleted";
                msg.StatusCode = 400;
            }
            return msg;
        }
        
        public async Task<MessageHelperModel> GetItemTransactionTypeById(long Id)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetItemTransactionTypeByIdAsync(Id);
            var msg = new MessageHelperModel();

            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetAllItemTransactionType(GetDataConfigModel getDataConfigModel)
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemTransactionTypeAsync(getDataConfigModel);
            var msg = new MessageHelperModel();

            if (res != null)
            {
                msg.data = res;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> GetItemTransactionTypeDDL()
        {
            var res = await _unitOfWorkRepository.SuperShopRepository.GetAllItemTransactionTypeAsync(new GetDataConfigModel { IsActive = true });
            var msg = new MessageHelperModel();
            List<CommonDDL> commonDDLs = new List<CommonDDL>();

            foreach (var item in res)
            {
                CommonDDL commonDDL = new CommonDDL();
                commonDDL.Name = item.TransactionName;
                commonDDL.Value = item.Id;
                commonDDLs.Add(commonDDL);
            }

            if (res.Count != 0)
            {
                msg.data = commonDDLs;
                msg.Message = "Successfull";
                msg.StatusCode = 200;
            }
            else
            {
                msg.Message = "No data found";
                msg.StatusCode = 400;
            }
            return msg;
        }
        public async Task<MessageHelperModel> MakeTransaction(ItemTransactionModel itemTransactionModel, UserModel ActionBy)
        {
            string operation = string.Empty;
            if(itemTransactionModel.TransactionTypeId == 1)
            {
                itemTransactionModel.UnitPrice = itemTransactionModel.unitPriceSell;
                operation = "Sell";
            }
            else
            {
                itemTransactionModel.UnitPrice = itemTransactionModel.unitPricePurchase;
                operation = "Purchase";
            }
            itemTransactionModel.DateTimeAction = DateTime.Now;
            itemTransactionModel.IsActive = true;
            itemTransactionModel.Total = itemTransactionModel.UnitPrice * itemTransactionModel.UnitOfAmount;

            var res = await _unitOfWorkRepository.SuperShopRepository.MakeItemTransactionAsync(itemTransactionModel);

            if (res.StatusCode == 200)
            {
                var log = new LogModel
                {
                    TableId = 2002,
                    ActionBy = ActionBy.Id ?? 0,
                    ActionChanges = ActionBy.UserFullName + "[" + ActionBy.Id.ToString() + "] " + operation + " " + itemTransactionModel.UnitOfAmount + " " + itemTransactionModel.ItemName + "[" + itemTransactionModel.ItemId + "]",
                    TablePk = res.Id,
                    JsonPayload = JsonSerializer.Serialize(itemTransactionModel),
                    ActionDate = DateTime.Now,
                    IsActive = true,
                    ActionType = operation
                };
                await _unitOfWorkService.LogService.CreateLog(log);

                if(operation== "Purchase")
                {
                    await _unitOfWorkService.NotificationService.SendNotificationToAll(itemTransactionModel.ItemName + "[" + itemTransactionModel.ItemId + "] Stock Updated");
                }
            }


            return res;
        }
    }
}
