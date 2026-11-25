using AutoMapper;
using LibrarySystem.Dtos.AuthorDtos;
using LibrarySystem.Entities;
using LibrarySystem.Repositories;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public class AuthorService(IAuthorRepository repository, IMapper mapper) : IAuthorService
    {
        public async Task<GenericResult<List<GetAuthorResponse>>> GetAllAsync()
        {
            var entities = await repository.GetAllWithBooksAsync();
            var response = mapper.Map<List<GetAuthorResponse>>(entities);
            return GenericResult<List<GetAuthorResponse>>.Success(response);
        }

        public async Task<GenericResult<GetAuthorResponse>> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdWithBooksAsync(id);

            if (entity is null)
                return GenericResult<GetAuthorResponse>.Failure($"Author with Id = {id} is not found.");

            var response = mapper.Map<GetAuthorResponse>(entity);
            return GenericResult<GetAuthorResponse>.Success(response);
        }

        public async Task<GenericResult<GetAuthorResponse>> CreateAsync(CreateAuthorRequest request)
        {
            var entity = mapper.Map<Author>(request);
            var created = await repository.CreateAsync(entity);
            var response = mapper.Map<GetAuthorResponse>(created);
            return GenericResult<GetAuthorResponse>.Success(response);
        }

        public async Task<bool> UpdateAsync(UpdateAuthorRequest request)
        {
            var entity = mapper.Map<Author>(request);
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
            => await repository.DeleteAsync(id);
    }
}
