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
        [HttpGet]
        [Route("GeAllUser")]
        public async Task<IActionResult> GeAllUser([FromQuery]GetDataConfigModel getDataConfigModel)
        {

            var res = await _unitOfWorkService.SuperShopService.GetAllUser(getDataConfigModel);
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
        [Route("GetUserDDL")]
        public async Task<IActionResult> GetUserDDL()
        {

            var res = await _unitOfWorkService.SuperShopService.GetUserDDL();
            return Ok(res);

        }
        
    }
}
