using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.web.Data.Models.Identity
{
	public class ApplicationUser : IdentityUser<int>
	{
        [Column(Order = 2)]
        public string FirstName { get; set; }

        [Column(Order = 3)]
        public string LastName { get; set; }

    }
}
