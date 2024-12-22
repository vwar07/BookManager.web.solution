using BookManager.web.Data.DataAccess.BookRepo.DTO;
using BookManager.web.Data.Models.EntityModels.Transaction;

namespace BookManager.web.Data.DataAccess.BookRepo
{
	public interface IBookRepository : IRepository<Book>
	{
		Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
        Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync();
		Task<Book?> GetBookDetail(int bookId);

		Task<PaginatedResult<Book>> GetBooksWithFiltersAsync(string search, string sortBy, int pageNumber, int pageSize);
		Task<PaginatedResult<Book>> GetBooksWithFiltersProceAsync(string search, string sortBy, int pageNumber, int pageSize);
		Task AddAsync(Book book);
	}

}
