using Biomedica.NGS.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Yungching.Rehouse.Web.Abstract
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ServiceFilter(typeof(ApiLoggingFilterAttribute))]
    public abstract class BaseApiController:ControllerBase
    {
        protected IActionResult HandleResponse<T>(T data, string message = null)
        {
            if (data == null)
                return NotFound(new ApiResponse<T>
                {
                    Success = false,
                    Message = message ?? "Not Found",
                    StatusCode = HttpStatusCode.NotFound
                });

            return Ok(new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message ?? "Success",
                StatusCode = HttpStatusCode.OK
            });
        }

        protected IActionResult HandleError(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return StatusCode((int)statusCode, new ApiResponse<object>
            {
                Success = false,
                Message = message,
                StatusCode = statusCode
            });
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
