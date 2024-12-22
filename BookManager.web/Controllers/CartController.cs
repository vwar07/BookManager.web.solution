using BookManager.web.Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.web.Controllers
{
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		public async Task<IActionResult> AddToCart(int bookId)
		{
			try
			{
				await _unitOfWork.Carts.AddItemToCart(bookId);
                return RedirectToAction("ViewBooks", "Book");
            }
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            try
            {
                var cartItems = await _unitOfWork.Carts.GetCartItemsAsync();
                return View(cartItems);
            }
            catch (UnauthorizedAccessException ex)
            {
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                return View("Error", new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItemFromCart(int cartId)
        {
            try
            {
                await _unitOfWork.Carts.RemoveItemFromCart(cartId);
                return RedirectToAction("ViewCart", "Cart");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("ViewCart", "Cart");
            }
        }

    }
}
