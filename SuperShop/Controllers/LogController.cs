using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.IService;
using SuperShop.Model;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        IUnitOfWorkService _unitOfWorkService;
        public LogController(IUnitOfWorkService  unitOfWorkService) {
            _unitOfWorkService = unitOfWorkService;
        }
        [HttpGet]
        [Route("GetAllLog")]
        public async Task<IActionResult> GetAllLog([FromQuery]GetLogModel getLogModel)
        {
            var res = await _unitOfWorkService.LogService.GetAllLog(getLogModel);
            return Ok(res);
        }
        [HttpGet]
        [Route("GetItemLog")]
        public async Task<IActionResult> GetItemLog([FromQuery] GetLogModel getLogModel)
        {
            var res = await _unitOfWorkService.LogService.GetItemLog(getLogModel);
            return Ok(res);
        }
    }
}
