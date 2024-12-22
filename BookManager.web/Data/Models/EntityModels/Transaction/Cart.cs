using BookManager.web.Data.Models.EntityBase;
using BookManager.web.Data.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.web.Data.Models.EntityModels.Transaction
{
	[Table("Cart")]
	public class Cart : Entity<int>
	{
		[Column(Order = 2)]
		[ForeignKey("Books")]
        public int? BookId { get; set; }
        public Book Books { get; set; }

		[Column(Order = 3)]
		[ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }


		ICollection<OrderDetail> OrderDetails { get; set; }

	}
}
