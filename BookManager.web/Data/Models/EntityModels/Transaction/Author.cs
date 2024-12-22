using BookManager.web.Data.Models.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.web.Data.Models.EntityModels.Transaction
{
	[Table("Author")]
	public class Author : Entity<int>
	{
		[Column(Order = 2)]
		[StringLength(256)]
		[Required]
		public string FirstName { get; set; }

		[Column(Order = 3)]
		[Required]
		[StringLength(255)]
		public string LastName { get; set; }

		[Column(Order = 4)]
		[StringLength(255)]
		[Required]
        public string Email { get; set; }

		[Column(Order = 5)]
		[StringLength(20)]
		[Required]
        public string PhoneNumber { get; set; }

        ICollection<Book> Books { get; set; }
	}
}
