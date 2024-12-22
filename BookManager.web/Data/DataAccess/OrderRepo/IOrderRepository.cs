using BookManager.web.Data.DataAccess.OrderRepo.DTO;

namespace BookManager.web.Data.DataAccess.OrderRepo
{
	public interface IOrderRepository
	{
        Task PlaceOrder(int cartId, int quantity, string phoneNumber, string address);
        Task<List<OrderDto>> GetOrdersByUserAsync();
    }
}
