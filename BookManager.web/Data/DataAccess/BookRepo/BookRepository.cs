using BookManager.web.Data.DataAccess.BookRepo.DTO;
using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookManager.web.Data.DataAccess.BookRepo
{

	public class BookRepository : Repository<Book>, IBookRepository
	{
		private readonly BookManagerDbContext _context;

		public BookRepository(BookManagerDbContext context) : base(context)
		{
			_context = context;
		}

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync()
        {
            IEnumerable<Book>? bookDetailList = await _context.Books	
														.Include(b => b.Authors)
														.Include(b => b.Publishers)
														.ToListAsync();

            var books = bookDetailList.AsParallel().Select(book =>
            {
                if (!string.IsNullOrEmpty(book.ImageLocation))
                {
                    book.ImageLocation = book.ImageLocation.ToUniverselPath();
                }
                return book;
            });

            return books;
        }

        public async Task<PaginatedResult<Book>> GetBooksWithFiltersAsync(string search, string sortBy, int pageNumber, int pageSize)
        {
            var query = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Publishers)
                .AsQueryable(); 

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(book =>
                    book.Title.ToLower().Contains(search) ||
                    (book.Authors.FirstName + " " + book.Authors.LastName).ToLower().Contains(search) ||
                    book.Publishers.Name.ToLower().Contains(search));
            }

            query = sortBy switch
            {
                "publisherAuthorTitle" => query
                    .OrderBy(b => b.Publishers.Name)
                    .ThenBy(b => b.Authors.LastName)
                    .ThenBy(b => b.Authors.FirstName)
                    .ThenBy(b => b.Title),
                "authorTitle" => query
                    .OrderBy(b => b.Authors.LastName)
                    .ThenBy(b => b.Authors.FirstName)
                    .ThenBy(b => b.Title),
                "title" => query.OrderBy(b => b.Title),
                "priceLowHigh" => query.OrderBy(b => b.Price),
                "priceHighLow" => query.OrderByDescending(b => b.Price),
                _ => query 
            };

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<Book>
            {
                Items = items,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            };
        }

		public async Task<PaginatedResult<Book>> GetBooksWithFiltersProceAsync(string search, string sortBy, int pageNumber, int pageSize)
		{
			var items = new List<Book>();
			int totalCount = 0;

			using (var command = _context.Database.GetDbConnection().CreateCommand())
			{
				command.CommandText = "dbo.GetBooksWithFilters";
				command.CommandType = CommandType.StoredProcedure;

				command.Parameters.Add(new SqlParameter("@Search", string.IsNullOrEmpty(search) ? (object)DBNull.Value : search));
				command.Parameters.Add(new SqlParameter("@SortBy", string.IsNullOrEmpty(sortBy) ? "default" : sortBy));
				command.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
				command.Parameters.Add(new SqlParameter("@PageSize", pageSize));

				if (command.Connection.State != ConnectionState.Open)
				{
					await command.Connection.OpenAsync();
				}

				using (var reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						var book = new Book
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
							//UpdatedDateTime = reader.GetDateTime(reader.GetOrdinal("UpdatedDateTime")),
							UpdatedDateTime = reader.IsDBNull(reader.GetOrdinal("UpdatedDateTime")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdatedDateTime")),
							IsDeleted = reader.GetBoolean(reader.GetOrdinal("IsDeleted")),
							DeletedDateTime = reader.IsDBNull(reader.GetOrdinal("DeletedDateTime")) ? null : reader.GetDateTime(reader.GetOrdinal("DeletedDateTime")),
							Title = reader.GetString(reader.GetOrdinal("Title")),
							Description = reader.GetString(reader.GetOrdinal("Description")),
							PublisherId = reader.GetInt32(reader.GetOrdinal("PublisherId")),
							Publishers = new Publisher
							{
								Id = reader.GetInt32(reader.GetOrdinal("PublisherId")),
								CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("PublisherCreatedDateTime")),
								//UpdatedDateTime = reader.GetDateTime(reader.GetOrdinal("PublisherUpdatedDateTime")),
								UpdatedDateTime = reader.IsDBNull(reader.GetOrdinal("PublisherUpdatedDateTime")) ? null : reader.GetDateTime(reader.GetOrdinal("PublisherUpdatedDateTime")),
								IsDeleted = reader.GetBoolean(reader.GetOrdinal("PublisherIsDeleted")),
								DeletedDateTime = reader.IsDBNull(reader.GetOrdinal("PublisherDeletedDateTime")) ? null : reader.GetDateTime(reader.GetOrdinal("PublisherDeletedDateTime")),
								Name = reader.GetString(reader.GetOrdinal("PublisherName")),
								BuildingAndStreet = reader.GetString(reader.GetOrdinal("PublisherBuildingAndStreet")),
								State = reader.GetString(reader.GetOrdinal("PublisherState")),
								Country = reader.GetString(reader.GetOrdinal("PublisherCountry")),
							},
							AuthorId = reader.GetInt32(reader.GetOrdinal("AuthorId")),
							Authors = new Author
							{
								Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
								CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("AuthorCreatedDateTime")),
								//UpdatedDateTime = reader.GetDateTime(reader.GetOrdinal("AuthorUpdatedDateTime")),
								UpdatedDateTime = reader.IsDBNull(reader.GetOrdinal("AuthorUpdatedDateTime")) ? null : reader.GetDateTime(reader.GetOrdinal("AuthorUpdatedDateTime")),
								IsDeleted = reader.GetBoolean(reader.GetOrdinal("AuthorIsDeleted")),
								DeletedDateTime = reader.IsDBNull(reader.GetOrdinal("AuthorDeletedDateTime")) ? null : reader.GetDateTime(reader.GetOrdinal("AuthorDeletedDateTime")),
								FirstName = reader.GetString(reader.GetOrdinal("AuthorFirstName")),
								LastName = reader.GetString(reader.GetOrdinal("AuthorLastName")),
								Email = reader.GetString(reader.GetOrdinal("AuthorEmail")),
								PhoneNumber = reader.GetString(reader.GetOrdinal("AuthorPhoneNumber")),
							},
							Price = reader.GetDecimal(reader.GetOrdinal("Price")),
							ImageLocation = reader.IsDBNull(reader.GetOrdinal("ImageLocation")) ? null : reader.GetString(reader.GetOrdinal("ImageLocation")),
							IsAvailable = reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
							AvailableQuantity = reader.GetInt32(reader.GetOrdinal("AvailableQuantity"))
						};

						items.Add(book);
					}

					if (await reader.NextResultAsync() && await reader.ReadAsync())
					{
						totalCount = reader.GetInt32(reader.GetOrdinal("TotalCount"));
					}
				}
			}

			return new PaginatedResult<Book>
			{
				Items = items,
				TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
				CurrentPage = pageNumber
			};
		}


		public async Task<Book?> GetBookDetail(int bookId)
		{
			return await _context.Books
				.Include(b => b.Authors)
				.Include(b => b.Publishers)
				.FirstOrDefaultAsync(b => b.Id == bookId);
		}


		public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
		{
			return await _context.Books
				.Where(b => b.AuthorId == authorId)
				.ToListAsync();
		}


		public async Task AddAsync(Book book)
		{
			await _context.Books.AddAsync(book);
		}

	}

}
