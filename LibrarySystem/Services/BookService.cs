using AutoMapper;
using LibrarySystem.Dtos.BookDtos;
using LibrarySystem.Entities;
using LibrarySystem.Repositories;
using LibrarySystem.Results;

namespace LibrarySystem.Services
{
    public class BookService(IBookRepository repository, IMapper mapper) : IBookService
    {
        public async Task<GenericResult<List<GetBookResponse>>> GetAllAsync()
        {
            var entities = await repository.GetAllWithAuthorAsync();
            var response = mapper.Map<List<GetBookResponse>>(entities);
            return GenericResult<List<GetBookResponse>>.Success(response);
        }

        public async Task<GenericResult<PaginatedList<GetBookResponse>>> GetAllAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            var paginatedList = await repository.GetAllAsync(pageNumber, pageSize, searchTerm);

            var response = new PaginatedList<GetBookResponse>
            {
                Data = mapper.Map<IEnumerable<GetBookResponse>>(paginatedList.Data),
                PageNumber = paginatedList.PageNumber,
                PageSize = paginatedList.PageSize,
                TotalRecords = paginatedList.TotalRecords,
                TotalPages = paginatedList.TotalPages
            };

            return GenericResult<PaginatedList<GetBookResponse>>.Success(response);
        }

        public async Task<GenericResult<GetBookResponse>> GetByIdAsync(int id)
        {
            var entity = await repository.GetByIdWithAuthorAsync(id);

            if (entity is null)
                return GenericResult<GetBookResponse>.Failure($"Book with Id = {id} is not found.");

            var response = mapper.Map<GetBookResponse>(entity);
            return GenericResult<GetBookResponse>.Success(response);
        }

        public async Task<GenericResult<GetBookResponse>> CreateAsync(CreateBookRequest request)
        {
            var entity = mapper.Map<Book>(request);
            var created = await repository.CreateAsync(entity);

            var withAuthor = await repository.GetByIdWithAuthorAsync(created.Id);
            var response = mapper.Map<GetBookResponse>(withAuthor);
            return GenericResult<GetBookResponse>.Success(response);
        }

        public async Task<bool> UpdateAsync(UpdateBookRequest request)
        {
            var entity = mapper.Map<Book>(request);
            return await repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
            => await repository.DeleteAsync(id);
    }
}
