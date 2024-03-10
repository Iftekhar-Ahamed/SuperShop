using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.IService;
using SuperShop.Model;
using System.Security.Claims;

namespace SuperShop.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public MenuController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
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
        public async Task<IActionResult> GetAllMenuPermission([FromQuery] GetDataConfigModel getDataConfigModel)
        {
            var res = await _unitOfWorkService.SuperShopService.GetAllMenuPermission(getDataConfigModel);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetAllMenus")]
        public async Task<IActionResult> GetAllMenus([FromQuery] GetDataConfigModel getDataConfigModel)
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
        [HttpPost]
        [Route("DeleteMenuPermissionById")]
        public async Task<IActionResult> DeleteMenuPermissionById([FromBody] long PermissionId)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var res = await _unitOfWorkService.SuperShopService.DeleteMenuPermissionById(PermissionId, ActionBy);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetMenuDDL")]
        public async Task<IActionResult> GetMenuDDL(long? UserId)
        {

            var res = await _unitOfWorkService.SuperShopService.GetMenuDDL(UserId);
            return Ok(res);

        }
    }
}
