using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop.IService;
using SuperShop.Model;
using System.Security.Claims;

namespace SuperShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWorkService;
        public ItemController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
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
        [Route("GetItemTypeDDL")]
        public async Task<IActionResult> GetItemTypeDDL()
        {

            var res = await _unitOfWorkService.SuperShopService.GetItemTypeDDL();
            return Ok(res);

        }
        [HttpGet]
        [Route("GetAllItemType")]
        public async Task<IActionResult> GetAllItemType([FromQuery] GetDataConfigModel getDataConfigModel)
        {

            var res = await _unitOfWorkService.SuperShopService.GetAllItemType(getDataConfigModel);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetItemTypeById")]
        public async Task<IActionResult> GetItemTypeById(long Id)
        {

            var res = await _unitOfWorkService.SuperShopService.GetItemTypeById(Id);
            return Ok(res);

        }
        [HttpPost]
        [Route("DeleteItemType")]
        public async Task<IActionResult> DeleteItemType([FromBody] long Id)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.DeleteItemType(Id, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("UpdateItemType")]
        public async Task<IActionResult> UpdateItemType(ItemTypeModel itemTypeModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.UpdateItemType(itemTypeModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("CreateItemType")]
        public async Task<IActionResult> CreateItemType(ItemTypeModel itemTypeModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.CreateItemType(itemTypeModel, ActionBy);
            return Ok(res);

        }

        [HttpGet]
        [Route("GetItemDDL")]
        public async Task<IActionResult> GetItemDDL()
        {

            var res = await _unitOfWorkService.SuperShopService.GetItemDDL();
            return Ok(res);

        }
        [HttpGet]
        [Route("GetAllItem")]
        public async Task<IActionResult> GetAllItem([FromQuery] GetDataConfigModel getDataConfigModel)
        {

            var res = await _unitOfWorkService.SuperShopService.GetAllItem(getDataConfigModel);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetItemById")]
        public async Task<IActionResult> GetItemById(long Id)
        {

            var res = await _unitOfWorkService.SuperShopService.GetItemById(Id);
            return Ok(res);

        }
        [HttpPost]
        [Route("DeleteItem")]
        public async Task<IActionResult> DeleteItem([FromBody] long Id)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.DeleteItemById(Id, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("UpdateItem")]
        public async Task<IActionResult> UpdateItem(ItemModel itemModel)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.UpdateItem(itemModel, ActionBy);
            return Ok(res);

        }
        [HttpPost]
        [Route("CreateItem")]
        public async Task<IActionResult> CreateItem(ItemModel item)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.CreateItem(item, ActionBy);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetItemTransactionTypeDDL")]
        public async Task<IActionResult> GetItemTransactionTypeDDL()
        {

            var res = await _unitOfWorkService.SuperShopService.GetItemTransactionTypeDDL();
            return Ok(res);

        }
        [HttpGet]
        [Route("GetAllItemTransactionType")]
        public async Task<IActionResult> GetAllItemTransactionType([FromQuery] GetDataConfigModel getDataConfigModel)
        {

            var res = await _unitOfWorkService.SuperShopService.GetAllItemTransactionType(getDataConfigModel);
            return Ok(res);

        }
        [HttpGet]
        [Route("GetItemTransactionTypeById")]
        public async Task<IActionResult> GetItemTransactionTypeById(long Id)
        {

            var res = await _unitOfWorkService.SuperShopService.GetItemTransactionTypeById(Id);
            return Ok(res);

        }
        [HttpPost]
        [Route("DeleteItemTransactionType")]
        public async Task<IActionResult> DeleteItemTransactionType([FromBody] long Id)
        {
            var ActionBy = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = await _unitOfWorkService.SuperShopService.DeleteItemTransactionTypeById(Id, ActionBy);
            return Ok(res);

        }   
    }
}
