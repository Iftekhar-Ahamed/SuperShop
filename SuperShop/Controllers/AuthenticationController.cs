using Microsoft.AspNetCore.Mvc;
using SuperShop.IService;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public AuthenticationController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([MaxLength(20)] string UserName, [MaxLength(16)] string PassWord)
        {
            var res = await _unitOfWorkService.AuthenticationService.LogInUser(UserName, PassWord);
            return Ok(res);

        }
    }
}
