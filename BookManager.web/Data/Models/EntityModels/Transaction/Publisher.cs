using BookManager.web.Data.Models.EntityBase;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.web.Data.Models.EntityModels.Transaction
{
	[Table("Publisher")]
	public class Publisher : Entity<int>
	{
		[Column(Order = 2)]
		[StringLength(255)]
		[Required]
		public string Name { get; set; }

        [Column(Order = 3)]
        [StringLength(4000)]
		[Required]
        public string BuildingAndStreet { get; set; }

        [Column(Order = 4)]
        [StringLength(255)]
        [Required]
        public string State { get; set; }

        [Column(Order = 5)]
        [StringLength(255)]
        [Required]
        public string Country { get; set; }

        ICollection<Book> Books { get; set; }
	}
}
