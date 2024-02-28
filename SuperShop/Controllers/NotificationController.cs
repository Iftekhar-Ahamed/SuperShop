using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperShop.IService;
using SuperShop.Notification;

namespace SuperShop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class NotificationController:ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public NotificationController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpPost("SendNotificationToAll")]
        public async Task<IActionResult> SendNotificationToAll([FromBody] String Message)
        {
            try
            {
                var res = await _unitOfWorkService.NotificationService.SendNotificationToAll(Message);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
