using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Dtos.LoanDtos;
using LibrarySystem.Filters;
using LibrarySystem.Services;

namespace LibrarySystem.Controllers
{
    [Route("api/[controller]")] // /api/Loans
    [ApiController]
    [ServiceFilter(typeof(ActionExecutionLoggingFilter))]
    [ServiceFilter(typeof(ActionTimingFilter))]
    [ServiceFilter(typeof(ExceptionHandlingFilter))]
    [Authorize]
    public class LoansController(ILoanService service, ILogger<LoansController> logger) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await service.GetAllAsync();

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // A Loan is identified by its composite key: BookId + BorrowerId
        [HttpGet("get-by-id/{bookId:int}/{borrowerId:int}")]
        public async Task<IActionResult> GetById(int bookId, int borrowerId)
        {
            var result = await service.GetByIdAsync(bookId, borrowerId);

            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateLoanRequest request)
        {
            var result = await service.CreateAsync(request);

            return result.IsSuccess ? Created() : BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateLoanRequest request)
        {
            var updated = await service.UpdateAsync(request);

            return updated ? Ok() : NotFound($"Loan with BookId = {request.BookId} and BorrowerId = {request.BorrowerId} is not found.");
        }

        [HttpDelete("delete/{bookId:int}/{borrowerId:int}")]
        public async Task<IActionResult> Delete(int bookId, int borrowerId)
        {
            var deleted = await service.DeleteAsync(bookId, borrowerId);

            return deleted ? NoContent() : NotFound($"Loan with BookId = {bookId} and BorrowerId = {borrowerId} is not found.");
        }
    }
}
