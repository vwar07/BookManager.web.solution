using BookManager.web.Data.DataAccess.BookRepo;
using BookManager.web.Data.DataAccess.CartRepo;
using BookManager.web.Data.DataAccess.OrderRepo;
using BookManager.web.Data.Models.EntityModels.Transaction;
using Project.Tracker.Web.Services.IdentityServices;

namespace BookManager.web.Data.DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly BookManagerDbContext _context;
		private readonly UserResolverService _user;

		public UnitOfWork(BookManagerDbContext context, UserResolverService user)
		{
			_context = context;
			_user = user;

			Books = new BookRepository(_context);
			Authors = new Repository<Author>(_context);
			Publishers = new Repository<Publisher>(_context);
			Carts = new CartRepository(_context, Books, _user);
			Orders = new OrderRepository(_context, _user);
		}

		public IBookRepository Books { get; private set; }
		public IRepository<Author> Authors { get; private set; }
		public IRepository<Publisher> Publishers { get; private set; }
		public ICartRepository Carts { get; private set; }
		public IOrderRepository Orders { get; private set; }

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}

}
