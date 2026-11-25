using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Dtos.BookDtos;
using LibrarySystem.Filters;
using LibrarySystem.Services;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")] // /api/Books
    [ApiController]
    [ServiceFilter(typeof(ActionExecutionLoggingFilter))]
    [ServiceFilter(typeof(ActionTimingFilter))]
    [ServiceFilter(typeof(ExceptionHandlingFilter))]
    [Authorize]
    public class BooksController(IBookService service, ILogger<BooksController> logger) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // Paginated + optional search, the way the example's
        // CategoryRepository.GetAllAsync(pageNumber, pageSize, searchTerm) worked.
        [HttpGet("get-with-criteria")]
        public async Task<IActionResult> GetAllWithCriteria(int pageNumber, int pageSize, string? searchTerm)
        {
            var result = await service.GetAllAsync(pageNumber, pageSize, searchTerm);

            return Ok(result);
        }

        [HttpGet("get-by-id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await service.GetByIdAsync(id);

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateBookRequest request)
        {
            var result = await service.CreateAsync(request);

            return result.IsSuccess ? Created() : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateBookRequest request)
        {
            var updated = await service.UpdateAsync(request);

            return updated ? Ok() : NotFound($"Book with Id = {request.Id} is not found.");
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var deleted = await service.DeleteAsync(id);

            return deleted ? NoContent() : NotFound($"Book with Id = {id} is not found.");
        }
    }
}
