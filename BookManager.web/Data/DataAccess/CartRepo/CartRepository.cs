
using BookManager.web.Data.DataAccess.BookRepo;
using BookManager.web.Data.DataAccess.CartRepo.DTO;
using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.EntityFrameworkCore;
using Project.Tracker.Web.Services.IdentityServices;

namespace BookManager.web.Data.DataAccess.CartRepo
{
	public class CartRepository : Repository<Book>, ICartRepository
	{
		private readonly BookManagerDbContext _context;
		private readonly IBookRepository _bookRepository;
		private readonly UserResolverService _user;

		public CartRepository(BookManagerDbContext context, IBookRepository bookRepository, UserResolverService user) : base(context)
		{
			_context = context;
			_bookRepository = bookRepository;
			_user = user;
		}

		public async Task AddItemToCart(int bookId)
		{
			var book = await _bookRepository.GetBookDetail(bookId);
			if (book == null)
			{
				throw new ArgumentException("The specified book does not exist.", nameof(bookId));
			}

			if (!book.IsAvailable)
			{
				throw new InvalidOperationException("No stock in this book");
			}

			var userId = _user.GetUserId();
			if (userId == null)
			{
				throw new UnauthorizedAccessException("User is not authenticated.");
			}

			var existingCartItem = await _context.Carts
				.FirstOrDefaultAsync(c => c.BookId == bookId && c.UserId == userId.Value);

			if (existingCartItem != null)
			{
				throw new InvalidOperationException("This book is already in the cart.");
			}

			var cart = new Cart
			{
				BookId = bookId,
				UserId = userId.Value,
			};

			_context.Carts.Add(cart);
			await _context.SaveChangesAsync();
		}


        public async Task<List<CartItemDto>> GetCartItemsAsync()
        {
            var userId = _user.GetUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var cartItems = await _context.Carts
                .Include(c => c.Books)
                .Where(c => c.UserId == userId.Value)
                .Select(c => new CartItemDto
                {
                    CartId = c.Id,
                    BookId = c.BookId.Value,
                    BookTitle = c.Books.Title,
                    BookPrice = c.Books.Price,
                    Quantity = 1, 
                    TotalPrice = c.Books.Price, 
                    BookImage = c.Books.ImageLocation
                })
                .ToListAsync();

            return cartItems;
        }

        public async Task RemoveItemFromCart(int cartId)
        {
            var cartItem = await _context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cartItem == null)
            {
                throw new ArgumentException("The specified cart item does not exist.", nameof(cartId));
            }

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
        }


        public async Task<CartItemDto> GetCartItemByIdAsync(int cartId)
        {
            var cartItem = await _context.Carts
                .Include(c => c.Books)
                .Where(c => c.Id == cartId)
                .Select(c => new CartItemDto
                {
                    CartId = c.Id,
                    BookId = c.BookId.Value,
                    BookTitle = c.Books.Title,
                    BookPrice = c.Books.Price,
                    Quantity = c.Books.AvailableQuantity,
                    TotalPrice = c.Books.Price,
                    BookImage = c.Books.ImageLocation
                })
                .FirstOrDefaultAsync();

            return cartItem;
        }


    }
}

