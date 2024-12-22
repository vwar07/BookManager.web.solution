using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookManager.web.Data.Seed.SeedData
{
	public class AuthorConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.HasData
			(
				new Author
				{
					Id = 1,
					FirstName = "John",
					LastName = "Doe",
					Email = "john.doe@example.com",
					PhoneNumber = "+1234567890"
				},
				new Author
				{
					Id = 2,
					FirstName = "Jane",
					LastName = "Smith",
					Email = "jane.smith@example.com",
					PhoneNumber = "+1987654321"
				},
				new Author
				{
					Id = 3,
					FirstName = "Emily",
					LastName = "Johnson",
					Email = "emily.johnson@example.com",
					PhoneNumber = "+1123456789"
				},
				new Author
				{
					Id = 4,
					FirstName = "Michael",
					LastName = "Brown",
					Email = "michael.brown@example.com",
					PhoneNumber = "+1445566778"
				},
				new Author
				{
					Id = 5,
					FirstName = "Robert",
					LastName = "Martin",
					Email = "robert.martin@example.com",
					PhoneNumber = "+1556677889"
				},
				new Author
				{
					Id = 6,
					FirstName = "Adam",
					LastName = "Freeman",
					Email = "adam.freeman@example.com",
					PhoneNumber = "+1667788990"
				},
				new Author
				{
					Id = 7,
					FirstName = "Andrew",
					LastName = "Troelsen",
					Email = "andrew.troelsen@example.com",
					PhoneNumber = "+1778899001"
				},
				new Author
				{
					Id = 8,
					FirstName = "Andrew",
					LastName = "Hunt",
					Email = "andrew.hunt@example.com",
					PhoneNumber = "+1889900112"
				},
				new Author
				{
					Id = 9,
					FirstName = "Martin",
					LastName = "Fowler",
					Email = "martin.fowler@example.com",
					PhoneNumber = "+1990011223"
				},
				new Author
				{
					Id = 10,
					FirstName = "Eric",
					LastName = "Freeman",
					Email = "eric.freeman@example.com",
					PhoneNumber = "+1101122334"
				}
			);
		}

	}
}
