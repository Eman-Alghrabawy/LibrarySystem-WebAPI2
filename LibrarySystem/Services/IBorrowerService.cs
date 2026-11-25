using LibrarySystem.Dtos.BorrowerDtos;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public interface IBorrowerService
    {
        Task<GenericResult<List<GetBorrowerResponse>>> GetAllAsync();
        Task<GenericResult<GetBorrowerResponse>> GetByIdAsync(int id);
        Task<GenericResult<GetBorrowerResponse>> CreateAsync(CreateBorrowerRequest request);
        Task<bool> UpdateAsync(UpdateBorrowerRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
