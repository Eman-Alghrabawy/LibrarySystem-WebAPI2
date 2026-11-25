using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Dtos.BorrowerDtos;
using LibrarySystem.Filters;
using LibrarySystem.Services;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")] // /api/Borrowers
    [ApiController]
    [ServiceFilter(typeof(ActionExecutionLoggingFilter))]
    [ServiceFilter(typeof(ActionTimingFilter))]
    [ServiceFilter(typeof(ExceptionHandlingFilter))]
    [Authorize]
    public class BorrowersController(IBorrowerService service, ILogger<BorrowersController> logger) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetByIdAsync(id);

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateBorrowerRequest request)
        {
            var result = await service.CreateAsync(request);

            return result.IsSuccess ? Created() : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateBorrowerRequest request)
        {
            var updated = await service.UpdateAsync(request);

            return updated ? Ok() : NotFound($"Borrower with Id = {request.Id} is not found.");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var deleted = await service.DeleteAsync(id);

            return deleted ? NoContent() : NotFound($"Borrower with Id = {id} is not found.");
        }
    }
}
