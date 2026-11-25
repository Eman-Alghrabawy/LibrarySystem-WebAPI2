using LibrarySystem.Dtos.LoanDtos;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public interface ILoanService
    {
        Task<GenericResult<List<GetLoanResponse>>> GetAllAsync();
        Task<GenericResult<GetLoanResponse>> GetByIdAsync(int bookId, int borrowerId);
        Task<GenericResult<GetLoanResponse>> CreateAsync(CreateLoanRequest request);
        Task<bool> UpdateAsync(UpdateLoanRequest request);
        Task<bool> DeleteAsync(int bookId, int borrowerId);
    }
}
