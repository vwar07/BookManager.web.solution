using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.web.Data.Models.EntityBase
{
	public class Entity<TKey>
	{
		[Key, Column(Order = 1)]
        public TKey Id { get; set; }

		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
		public DateTime? UpdatedDateTime { get; set; } = null;
		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedDateTime { get; set; } = null;
    }
}
