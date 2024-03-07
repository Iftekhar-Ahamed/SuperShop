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
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public AuthenticationController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([MaxLength(20)] string UserName, [MaxLength(16)] string PassWord)
        {
            var res = await _unitOfWorkService.AuthenticationService.LogInUser(UserName, PassWord);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetAccessToken")]
        public async Task<IActionResult> GetAccessToken()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var userFullNameClaim = HttpContext.User.FindFirst(ClaimTypes.Name);
            if (userIdClaim != null && userFullNameClaim != null)
            {
                var UserModel = new UserModel
                {
                    Id = long.Parse(userIdClaim.Value),
                    UserFullName = userFullNameClaim.Value
                };
                var msg = new MessageHelperModel();
                var res = await _unitOfWorkService.AuthenticationService.GetNewAccessToken(UserModel);

                return Ok(res);
            }
            return BadRequest();
        }
    }
}
