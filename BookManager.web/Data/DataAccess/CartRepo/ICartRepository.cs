using BookManager.web.Data.DataAccess.CartRepo.DTO;

namespace BookManager.web.Data.DataAccess.CartRepo
{
	public interface ICartRepository
	{
		Task AddItemToCart(int bookId);
		Task<List<CartItemDto>> GetCartItemsAsync();
		Task RemoveItemFromCart(int cartId);
		Task<CartItemDto> GetCartItemByIdAsync(int cartId);
    }
}
