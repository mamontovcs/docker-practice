using HelloApi.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HelloApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public IEnumerable<string> GetItems()
        {
            Log.Information("Getting all items from collection....");
            return _itemService.GetItems();
        }
    }
}
