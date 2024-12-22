using BookManager.web.Constants;
using BookManager.web.Data.DataAccess;
using BookManager.web.Data.DataAccess.BookRepo.DTO;
using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.web.Controllers
{
	[Authorize]
	public class BookController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public BookController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBooks()
		{
			var books = await _unitOfWork.Books.GetAllAsync();
			return Ok(books);
		}

		//[HttpPost]
		//public async Task<IActionResult> AddBook(Book book)
		//{
		//	await _unitOfWork.Books.AddAsync(book);
		//	await _unitOfWork.SaveChangesAsync();
		//	return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, book);
		//}

		[HttpGet]
        public async Task<IActionResult> ViewBooks(string search = "", string sortBy = "default", int pageNumber = 1, int pageSize = 3)
        {
			PaginatedResult<Book>? result = await Books(search, sortBy, pageNumber, pageSize);
			ViewData["TotalPages"] = result.TotalPages;
            ViewData["CurrentPage"] = result.CurrentPage;
            ViewData["Search"] = search;
            ViewData["SortBy"] = sortBy;

            return View(result.Items);
        }


		[HttpGet("Books")]
		public async Task<PaginatedResult<Book>> Books(string search = "", string sortBy = "default", int pageNumber = 1, int pageSize = 3)
		{
			return await _unitOfWork.Books.GetBooksWithFiltersAsync(search, sortBy, pageNumber, pageSize);
		}

		[HttpGet("books-with-filters")]
		public async Task<IActionResult> GetBooksWithFilters([FromQuery] string search = "", [FromQuery] string sortBy = "default", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		{
			try
			{
				var result = await _unitOfWork.Books.GetBooksWithFiltersProceAsync(search, sortBy, pageNumber, pageSize);

				return Ok(new
				{
					items = result.Items,
					totalPages = result.TotalPages,
					currentPage = result.CurrentPage
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An error occurred while retrieving books.", error = ex.Message });
			}
		}

		public async Task<IActionResult> ViewBook(int Id)
		{
			var book = await _unitOfWork.Books.GetBookDetail(Id);
			return View(book);
		}


		[HttpGet]
		[Authorize(Roles = "ApplicationAdmin")]
		public async Task<IActionResult> AddBook()
		{
			var authors = await _unitOfWork.Authors.GetAllAsync();
			var publishers = await _unitOfWork.Publishers.GetAllAsync();

			ViewBag.Authors = authors.Select(a => new { a.Id, Name = $"{a.FirstName} {a.LastName}" });
			ViewBag.Publishers = publishers.Select(p => new { p.Id, p.Name });

			return View();
		}

		[HttpPost]
		[Authorize(Roles = "ApplicationAdmin")]
		public async Task<IActionResult> AddBook(Book book)
		{
			try
			{
				await _unitOfWork.Books.AddAsync(book);
				await _unitOfWork.SaveChangesAsync();
				TempData["SuccessMessage"] = "Book added successfully!";
				return RedirectToAction("ViewBooks");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");

				var authors = await _unitOfWork.Authors.GetAllAsync();
				var publishers = await _unitOfWork.Publishers.GetAllAsync();

				ViewBag.Authors = authors.Select(a => new { a.Id, Name = $"{a.FirstName} {a.LastName}" });
				ViewBag.Publishers = publishers.Select(p => new { p.Id, p.Name });

				return View(book);
			}
		}
	}
}
