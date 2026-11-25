using AutoMapper;
using LibrarySystem.Dtos.BorrowerDtos;
using LibrarySystem.Entities;
using LibrarySystem.Repositories;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public class BorrowerService(IBorrowerRepository repository, IMapper mapper) : IBorrowerService
    {
        public async Task<GenericResult<List<GetBorrowerResponse>>> GetAllAsync()
        {
            var entities = await repository.GetAllWithLoansAsync();
            var response = mapper.Map<List<GetBorrowerResponse>>(entities);
            return GenericResult<List<GetBorrowerResponse>>.Success(response);
        }

        public async Task<GenericResult<GetBorrowerResponse>> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdWithLoansAsync(id);

            if (entity is null)
                return GenericResult<GetBorrowerResponse>.Failure($"Borrower with Id = {id} is not found.");

            var response = mapper.Map<GetBorrowerResponse>(entity);
            return GenericResult<GetBorrowerResponse>.Success(response);
        }

        public async Task<GenericResult<GetBorrowerResponse>> CreateAsync(CreateBorrowerRequest request)
        {
            var entity = mapper.Map<Borrower>(request);
            var created = await repository.CreateAsync(entity);
            var response = mapper.Map<GetBorrowerResponse>(created);
            return GenericResult<GetBorrowerResponse>.Success(response);
        }

        public async Task<bool> UpdateAsync(UpdateBorrowerRequest request)
        {
            var entity = mapper.Map<Borrower>(request);
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
            => await repository.DeleteAsync(id);
    }
}
