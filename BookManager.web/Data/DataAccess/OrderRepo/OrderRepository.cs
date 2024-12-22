
using BookManager.web.Data.DataAccess.OrderRepo.DTO;
using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.EntityFrameworkCore;
using Project.Tracker.Web.Services.IdentityServices;

namespace BookManager.web.Data.DataAccess.OrderRepo
{
	public class OrderRepository : Repository<Book>, IOrderRepository
	{
		private readonly BookManagerDbContext _context;
		private readonly UserResolverService _user;

		public OrderRepository(BookManagerDbContext context, UserResolverService user) : base(context)
		{
			_context = context;
			_user = user;
		}

        public async Task PlaceOrder(int cartId, int quantity, string phoneNumber, string address)
        {
            var cartItem = await _context.Carts.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == cartId);
            if (cartItem == null)
            {
                throw new ArgumentException("Cart item not found.");
            }

            if (quantity > cartItem.Books.AvailableQuantity)
            {
                throw new InvalidOperationException("Insufficient stock.");
            }

            var order = new OrderDetail
            {
                BookId = cartItem.BookId.Value,
                UserId = cartItem.UserId,
                Quantity = quantity,
                ToatalAmount = cartItem.Books.Price * quantity,
                PhoneNumber = phoneNumber,
                Address = address
            };

            _context.OrderDetails.Add(order);

            cartItem.Books.AvailableQuantity -= quantity;

            _context.Carts.Remove(cartItem);

            await _context.SaveChangesAsync();
        }


        public async Task<List<OrderDto>> GetOrdersByUserAsync()
        {
            var userId = _user.GetUserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var orders = await _context.OrderDetails
                .Include(o => o.Books)
                .Where(o => o.UserId == userId.Value)
                .Select(o => new OrderDto
                {
                    OrderId = o.Id,
                    BookTitle = o.Books.Title,
                    BookImage = o.Books.ImageLocation,
                    OrderDate = o.CreatedDateTime,
                    Quantity = o.Quantity,
                    TotalPrice = o.ToatalAmount,
                    PhoneNumber = o.PhoneNumber,
                    Address = o.Address
                })
                .ToListAsync();

            return orders;
        }


    }
}
