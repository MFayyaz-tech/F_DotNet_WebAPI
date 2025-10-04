using System;
using Microsoft.Extensions.Configuration; 
using BU.DTO.DTOs.ListItem;
using BU.Services.IServices.Agency;
using Common.Helper;
using Logging;
using Microsoft.AspNetCore.Mvc;
using WebRAPI.Base;

namespace WebAPI.Controllers.ItemList
{
    [Route("api/itemList")]
    [ApiController]
    public class ItemListController : BaseController
    {
        private readonly ILogging _logging;
        private readonly IItemListService _ItemListServices;
        private readonly IConfiguration _configuration;

        public ItemListController(IItemListService itemListService, ILogging logging, IConfiguration configuration)
            : base(logging, configuration)
        {
            _ItemListServices = itemListService;
            _logging = logging;
            _configuration = configuration;
        }

        [HttpGet("getItemList")]
        public IActionResult GetItemLists()
        {
            Result result = new Result(true);
            try
            {
                AllListItemDTO data = _ItemListServices.GetListItems(new string[] { });
                result.Data = data;
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                _logging.Fatal(exc.Message);
            }
            return Ok(result);
        }
    }
}
