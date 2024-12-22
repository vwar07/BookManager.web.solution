using BookManager.web.Data.DataAccess.BookRepo;
using BookManager.web.Data.DataAccess.CartRepo;
using BookManager.web.Data.DataAccess.OrderRepo;
using BookManager.web.Data.Models.EntityModels.Transaction;

namespace BookManager.web.Data.DataAccess
{
	public interface IUnitOfWork : IDisposable
	{
		IBookRepository Books { get; }
		IRepository<Author> Authors { get; }
		IRepository<Publisher> Publishers { get; }
		ICartRepository Carts { get; }
		IOrderRepository Orders { get; }
		Task SaveChangesAsync();
	}

}
