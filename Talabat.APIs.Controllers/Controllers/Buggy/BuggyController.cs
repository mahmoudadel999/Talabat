using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Controllers.Base;
using Talabat.APIs.Controllers.Errors;

namespace Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            //throw new NotFoundException();
            return NotFound(new ApiResponse(404));  // 404
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500
        }

        [HttpGet("badrequest")]
        public IActionResult GetBad()
        {
            return BadRequest(new ApiResponse(400)); // 400
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetValidationError(int id)
        {
            return Ok(); // 400
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized(new ApiResponse(401)); // 401
        }

        [HttpGet("forbidden")]
        public IActionResult GetForbidden()
        {
            return Forbid(); // 403
        }

        [HttpGet("authorized")]
        public IActionResult GetAuthorized()
        {
            return Ok(); // 403
        }
    }
}
