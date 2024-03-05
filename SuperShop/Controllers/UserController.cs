using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperShop.IService;
using SuperShop.Model;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SuperShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public UserController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserModel userModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.CreateUser(userModel,ActionBy);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(long Id)
        {
            var res = await _unitOfWorkService.SuperShopService.GetUserById(Id);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetUserInformationById")]
        public async Task<IActionResult> GetUserInformationById(long Id)
        {
            var res = await _unitOfWorkService.SuperShopService.GetUserInformationById(Id);
            return Ok(res);

        }
        [HttpPost]
        [Route("UpdateUserById")]
        public async Task<IActionResult> UpdateUserById(UserModel userModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.UpdateUserById(userModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("DeleteUserById")]
        public async Task<IActionResult> DeleteUserById([FromBody] long UserId)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.DeleteUserById(UserId, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("CreateMenu")]
        public async Task<IActionResult> CreateMenu(MenuModel menuModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.CreateMenu(menuModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("UpdateMenu")]
        public async Task<IActionResult> UpdateMenu(MenuModel menuModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.UpdateMenu(menuModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("CreateUpdateUserMenuPermission")]
        public async Task<IActionResult> CreateUpdateUserMenuPermission(MenuUserPermissionModel menuUserPermissionModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.CreateUpdateUserMenuPermission(menuUserPermissionModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("CreateItemTransactionType")]
        public async Task<IActionResult> CreateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.CreateItemTransactionType(itemTransactionTypeModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("UpdateItemTransactionType")]
        public async Task<IActionResult> UpdateItemTransactionType(ItemTransactionTypeModel itemTransactionTypeModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.UpdateItemTransactionType(itemTransactionTypeModel, ActionBy);
            return Ok(res);

        }
        [HttpGet]
        [Route("GeAlltItemTransactionType")]
        public async Task<IActionResult> GeAlltItemTransactionType()
        {

            var res = await _unitOfWorkService.SuperShopService.GeAlltItemTransactionType();
            return Ok(res);

        }
        [HttpGet]
        [Route("GeAllUser")]
        public async Task<IActionResult> GeAllUser([FromQuery]GetDataConfigModel getDataConfigModel)
        {

            var res = await _unitOfWorkService.SuperShopService.GetAllUser(getDataConfigModel);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetMenuPermissionByUserId")]
        public async Task<IActionResult> GetMenuPermissionByUserId(long UserId)
        {

            var res = await _unitOfWorkService.SuperShopService.GetMenuPermissionByUserId(UserId);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetMenuPermissionById")]
        public async Task<IActionResult> GetMenuPermissionById(long MenuPermissionId)
        {

            var res = await _unitOfWorkService.SuperShopService.GetMenuPermissionById(MenuPermissionId);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetAllMenuPermission")]
        public async Task<IActionResult> GetAllMenuPermission([FromQuery]GetDataConfigModel getDataConfigModel)
        {
            var res = await _unitOfWorkService.SuperShopService.GetAllMenuPermission(getDataConfigModel);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetAllMenus")]
        public async Task<IActionResult> GetAllMenus([FromQuery]GetDataConfigModel getDataConfigModel)
        {

            var res = await _unitOfWorkService.SuperShopService.GetAllMenus(getDataConfigModel);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetMenuById")]
        public async Task<IActionResult> GetMenuById(long MenuId)
        {

            var res = await _unitOfWorkService.SuperShopService.GetMenuById(MenuId);
            return Ok(res);

        }
        [HttpPost]
        [Route("DeleteMenuById")]
        public async Task<IActionResult> DeleteMenuById([FromBody] long MenuId)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.DeleteMenuById(MenuId, ActionBy);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetUserType")]
        public async Task<IActionResult> GetUserType()
        {

            var res = await _unitOfWorkService.SuperShopService.GetUserType();
            return Ok(res);

        }
        [HttpGet]
        [Route("GetMenuDDL")]
        public async Task<IActionResult> GetMenuDDL()
        {

            var res = await _unitOfWorkService.SuperShopService.GetMenuDDL();
            return Ok(res);

        }
        [HttpGet]
        [Route("GetUserDDL")]
        public async Task<IActionResult> GetUserDDL()
        {

            var res = await _unitOfWorkService.SuperShopService.GetUserDDL();
            return Ok(res);

        }

    }
}
