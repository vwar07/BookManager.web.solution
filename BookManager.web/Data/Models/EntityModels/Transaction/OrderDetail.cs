using BookManager.web.Data.Models.EntityBase;
using BookManager.web.Data.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookManager.web.Data.Models.EntityModels.Transaction
{
	[Table("OrderDetail")]
	public class OrderDetail : Entity<int>
	{
        [Column(Order = 2)]
		[ForeignKey("Books")]
        public int? BookId { get; set; }
        public Book Books { get; set; }

		[Column(Order = 3)]
		[ForeignKey("Carts")]
        public int? CartId { get; set; }
        public Cart? Carts { get; set; }

		[Column(Order = 4)]
		[ForeignKey("ApplicationUser")]
        public int? UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

		[Column(Order = 5)]
		public int Quantity { get; set; }

		[Column(Order = 6)]
		public decimal ToatalAmount { get; set; }

		[Column(Order = 7)]
		[StringLength(20)]
		public string PhoneNumber { get; set; }

		[Column(Order = 8)]
		[StringLength(4000)]
		public string Address { get; set; }

    }
}
