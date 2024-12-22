using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookManager.web.Data.Seed.SeedData
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasData
			(
				new Book
				{
					Id = 1,
					Title = "C# Programming",
					Description = "A comprehensive guide to learning the C# programming language, covering basics to advanced concepts.",
					Price = 29.99m,
					PublisherId = 1,
					AuthorId = 1,
					ImageLocation = "/images/csharp_programming.jpg",
					IsAvailable = true,
					AvailableQuantity = 50
				},
				new Book
				{
					Id = 2,
					Title = "Mastering .NET Core",
					Description = "An advanced book for developers to master .NET Core framework and build scalable applications.",
					Price = 39.99m,
					PublisherId = 2,
					AuthorId = 2,
					ImageLocation = "/images/mastering_dotnet_core.jpg",
					IsAvailable = false,
					AvailableQuantity = 0
				},
				new Book
				{
					Id = 3,
					Title = "Entity Framework Essentials",
					Description = "Learn how to use Entity Framework to handle database operations efficiently in your .NET applications.",
					Price = 24.99m,
					PublisherId = 3,
					AuthorId = 3,
					ImageLocation = "/images/entity_framework_essentials.jpg",
					IsAvailable = true,
					AvailableQuantity = 20
				},
				new Book
				{
					Id = 4,
					Title = "Learning ASP.NET Core",
					Description = "A beginner-friendly book to understand and build web applications using ASP.NET Core.",
					Price = 34.99m,
					PublisherId = 4,
					AuthorId = 4,
					ImageLocation = "/images/learning_aspnet_core.jpg",
					IsAvailable = true,
					AvailableQuantity = 25
				},
				new Book
				{
					Id = 5,
					Title = "Clean Architecture",
					Description = "A practical guide to implementing clean architecture principles for robust and maintainable codebases.",
					Price = 49.99m,
					PublisherId = 5,
					AuthorId = 5,
					ImageLocation = "/images/clean_architecture.jpg",
					IsAvailable = false,
					AvailableQuantity = 0
				},
				new Book
				{
					Id = 6,
					Title = "Pro ASP.NET Core MVC",
					Description = "A professional guide to building modern web applications using ASP.NET Core MVC framework.",
					Price = 44.99m,
					PublisherId = 6,
					AuthorId = 6,
					ImageLocation = "/images/pro_aspnet_core_mvc.jpg",
					IsAvailable = true,
					AvailableQuantity = 40
				},
				new Book
				{
					Id = 7,
					Title = "Programming in C#",
					Description = "An in-depth exploration of C# programming, designed for both novice and experienced developers.",
					Price = 54.99m,
					PublisherId = 7,
					AuthorId = 7,
					ImageLocation = "/images/programming_in_csharp.jpg",
					IsAvailable = true,
					AvailableQuantity = 35
				},
				new Book
				{
					Id = 8,
					Title = "The Pragmatic Programmer",
					Description = "A timeless guide to software development best practices and tips for pragmatic programmers.",
					Price = 42.99m,
					PublisherId = 8,
					AuthorId = 8,
					ImageLocation = "/images/the_pragmatic_programmer.jpg",
					IsAvailable = true,
					AvailableQuantity = 45
				},
				new Book
				{
					Id = 9,
					Title = "Refactoring",
					Description = "An insightful book on improving the design of existing code without changing its functionality.",
					Price = 39.99m,
					PublisherId = 9,
					AuthorId = 9,
					ImageLocation = "/images/refactoring.jpg",
					IsAvailable = true,
					AvailableQuantity = 28
				},
				new Book
				{
					Id = 10,
					Title = "Head First Design Patterns",
					Description = "A visually engaging book that simplifies complex design patterns for easier understanding and application.",
					Price = 49.99m,
					PublisherId = 10,
					AuthorId = 10,
					ImageLocation = "/images/head_first_design_patterns.jpg",
					IsAvailable = true,
					AvailableQuantity = 22
				}
			);
		}
	}
}
