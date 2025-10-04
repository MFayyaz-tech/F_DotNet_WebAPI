using Common.Helper;
using DTO.Core;
using Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/ddl/")]
    [ApiController]
    [Authorize]
    public class DDLControllercs : Controller
    {
        private IDDLService _ddlService;
        public DDLControllercs(IDDLService ddlService)
        {
            _ddlService = ddlService;
        }

        [HttpPost("loadItems")]
        public IActionResult GetDDLItems(DDLItemsDTO ddlDTO)
        {
            Result result = new Result(true);
            try
            {
                result.Data = _ddlService.LoadData(ddlDTO);
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message;
                //_logging.Fatal(exc.Message);
            }
            return Json(result);
        }
    }
}
