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
            //var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            long ActionBy = 1;

            var res = await _unitOfWorkService.SuperShopService.CreateUser(userModel,ActionBy);
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

    }
}
