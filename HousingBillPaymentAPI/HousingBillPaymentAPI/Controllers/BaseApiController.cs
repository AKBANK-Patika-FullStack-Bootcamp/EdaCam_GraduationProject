using System.Collections.Generic;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
namespace HousingBillPaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BaseApiController : Controller
    {
        [NonAction]
        protected IActionResult Success(BaseResponse response)
        {
            return Ok(response);
        }
        [NonAction]
        protected IActionResult SuccessData(BaseResponse response)
        {
            return Ok(response.Data);
        }
        [NonAction]
        protected IActionResult BadRequestMessage(BaseResponse response)
        {
            return StatusCode(400,response.Message);
        }
        [NonAction]
        protected IActionResult BadRequest(BaseResponse response)
        {
            return StatusCode(400,response);
        }
    }
}
