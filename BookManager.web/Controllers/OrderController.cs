using BookManager.web.Data.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.web.Controllers
{
	public class OrderController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(int cartId)
        {
            var cartItem = await _unitOfWork.Carts.GetCartItemByIdAsync(cartId);
            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }
            return PartialView("~/Views/Order/_PlaceOrderModal.cshtml", cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int cartId, int quantity, string phoneNumber, string address)
        {
            try
            {
                await _unitOfWork.Orders.PlaceOrder(cartId, quantity, phoneNumber, address);
                return Json(new { success = true, message = "Order placed successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _unitOfWork.Orders.GetOrdersByUserAsync();
            return View(orders);
        }
    }
}
