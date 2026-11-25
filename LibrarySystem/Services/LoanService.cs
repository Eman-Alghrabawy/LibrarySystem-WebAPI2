using AutoMapper;
using LibrarySystem.Dtos.LoanDtos;
using LibrarySystem.Entities;
using LibrarySystem.Repositories;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public class LoanService(ILoanRepository repository, IMapper mapper) : ILoanService
    {
        public async Task<GenericResult<List<GetLoanResponse>>> GetAllAsync()
        {
            var entities = await repository.GetAllAsync();
            var response = mapper.Map<List<GetLoanResponse>>(entities);
            return GenericResult<List<GetLoanResponse>>.Success(response);
        }

        public async Task<GenericResult<GetLoanResponse>> GetByIdAsync(int bookId, int borrowerId)
        {
            var entity = await repository.GetByIdAsync(bookId, borrowerId);

            if (entity is null)
                return GenericResult<GetLoanResponse>.Failure($"Loan with BookId = {bookId} and BorrowerId = {borrowerId} is not found.");

            var response = mapper.Map<GetLoanResponse>(entity);
            return GenericResult<GetLoanResponse>.Success(response);
        }

        public async Task<GenericResult<GetLoanResponse>> CreateAsync(CreateLoanRequest request)
        {
            var existing = await repository.GetByIdAsync(request.BookId, request.BorrowerId);

            if (existing is not null)
                return GenericResult<GetLoanResponse>.Failure($"Borrower with Id = {request.BorrowerId} already has a loan for Book with Id = {request.BookId}.");

            var entity = mapper.Map<Loan>(request);
            await repository.CreateAsync(entity);

            var created = await repository.GetByIdAsync(entity.BookId, entity.BorrowerId);
            var response = mapper.Map<GetLoanResponse>(created);
            return GenericResult<GetLoanResponse>.Success(response);
        }

        public async Task<bool> UpdateAsync(UpdateLoanRequest request)
        {
            var entity = mapper.Map<Loan>(request);
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int bookId, int borrowerId)
            => await repository.DeleteAsync(bookId, borrowerId);
    }
}
