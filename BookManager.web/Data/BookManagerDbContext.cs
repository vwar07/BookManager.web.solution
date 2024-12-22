

using BookManager.web.Data.Models.EntityBase;
using BookManager.web.Data.Models.EntityModels.Transaction;
using BookManager.web.Data.Models.Identity;
using BookManager.web.Data.Seed.SeedData;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookManager.web.Data
{
	public class BookManagerDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
	{
		public BookManagerDbContext(DbContextOptions<BookManagerDbContext> options) : base(options)
		{

		}


		public DbSet<Book> Books { get; set; } = default!;
		public DbSet<Author> Authors { get; set; } = default!;
		public DbSet<Publisher> Publishers { get; set; } = default!;
		public DbSet<Cart> Carts { get; set; } = default!;
		public DbSet<OrderDetail> OrderDetails { get; set; } = default!;



		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Book>(entity =>
			{
				entity.Property(prop => prop.Price).HasPrecision(18, 6);
			});

			builder.Entity<OrderDetail>(entity =>
			{
				entity.Property(prop => prop.ToatalAmount).HasPrecision(18, 6);
			});

			builder.ApplyConfiguration(new PublisherConfiguration());
			builder.ApplyConfiguration(new AuthorConfiguration());
			builder.ApplyConfiguration(new BookConfiguration());

			base.OnModelCreating(builder);
		}


		#region #ChangeTrakker
		public override int SaveChanges()
		{
			var entries = ChangeTracker
				.Entries()
				.Where(e => e.Entity is Entity<int> &&
						   (e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach (var entry in entries)
			{
				((Entity<int>)entry.Entity).UpdatedDateTime = DateTime.Now;

				if (entry.State == EntityState.Added)
				{
					((Entity<int>)entry.Entity).CreatedDateTime = DateTime.Now;
				}
				else if (entry.State == EntityState.Deleted)
				{
					((Entity<int>)entry.Entity).DeletedDateTime = DateTime.Now;
				}
			}

			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			var entries = ChangeTracker
				.Entries()
				.Where(e => e.Entity is Data.Models.EntityBase.Entity<int> &&
						   (e.State == EntityState.Added || e.State == EntityState.Modified));

			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					((Entity<int>)entry.Entity).CreatedDateTime = DateTime.Now;
				}
				else if (entry.State == EntityState.Deleted)
				{
					((Data.Models.EntityBase.Entity<int>)entry.Entity).DeletedDateTime = DateTime.Now;
				}
			}

			return await base.SaveChangesAsync(cancellationToken);
		}

		#endregion
	}
}
