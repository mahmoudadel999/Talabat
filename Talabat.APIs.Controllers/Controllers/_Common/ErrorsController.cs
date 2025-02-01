using Microsoft.AspNetCore.Mvc;
using System.Net;
using Talabat.APIs.Controllers.Errors;

namespace Talabat.APIs.Controllers.Controllers.Common
{
    [ApiController]
    [Route("Errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Error(int code)
        {
            if (code == (int)HttpStatusCode.NotFound)
            {
                var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The requested endpoint: {Request.Path} is not found");
                return NotFound(code);
            }

            return StatusCode(code, new ApiResponse(code));
        }
    }
}
