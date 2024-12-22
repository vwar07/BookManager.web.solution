using BookManager.web.Data.Models.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.web.Data.Models.EntityModels.Transaction
{
	[Table("Book")]
	public class Book : Entity<int>
	{
		[Column(Order = 2)]
		[StringLength(255)]
		[Required]
		public string Title { get; set; }

		[Column(Order = 3)]
		[StringLength(4000)]
		[Required]
		public string Description { get; set; }

		[Column(Order = 4)]
		[ForeignKey("Publishers")]
		public int? PublisherId { get; set; }
		public Publisher Publishers { get; set; }

		[Column(Order = 5)]
		[ForeignKey("Authors")]
		public int? AuthorId { get; set; }
		public Author Authors { get; set; }

		[Column(Order = 6/*, TypeName = "decimal(18,2)"*/)]
		[Required]
		public decimal Price { get; set; }

		[Column(Order = 7)]
		[StringLength(4000)]
		public string? ImageLocation { get; set; } = null;

        [Column(Order = 8)]
		[Required]
		public bool IsAvailable { get; set; } = true;

        [Column(Order = 9)]
        [Required]
        public int AvailableQuantity { get; set; }


		ICollection<Cart> carts { get; set; }
		ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
