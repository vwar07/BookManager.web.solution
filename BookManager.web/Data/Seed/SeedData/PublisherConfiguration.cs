using BookManager.web.Data.Models.EntityModels.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManager.web.Data.Seed.SeedData
{
	public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
	{
		public void Configure(EntityTypeBuilder<Publisher> builder)
		{
			builder.HasData
			(
				new Publisher
				{
					Id = 1,
					Name = "Penguin",
					BuildingAndStreet = "80 Strand, London",
					State = "London",
					Country = "United Kingdom"
				},
				new Publisher
				{
					Id = 2,
					Name = "O'Reilly Media",
					BuildingAndStreet = "1005 Gravenstein Highway North",
					State = "California",
					Country = "United States"
				},
				new Publisher
				{
					Id = 3,
					Name = "Packt Publishing",
					BuildingAndStreet = "Livery Place, 35 Livery Street",
					State = "Birmingham",
					Country = "United Kingdom"
				},
				new Publisher
				{
					Id = 4,
					Name = "Microsoft Press",
					BuildingAndStreet = "One Microsoft Way",
					State = "Washington",
					Country = "United States"
				},
				new Publisher
				{
					Id = 5,
					Name = "Prentice Hall",
					BuildingAndStreet = "501 Boylston Street",
					State = "Massachusetts",
					Country = "United States"
				},
				new Publisher
				{
					Id = 6,
					Name = "Apress",
					BuildingAndStreet = "233 Spring Street",
					State = "New York",
					Country = "United States"
				},
				new Publisher
				{
					Id = 7,
					Name = "Apress",
					BuildingAndStreet = "625 Avenue of the Americas",
					State = "New York",
					Country = "United States"
				},
				new Publisher
				{
					Id = 8,
					Name = "Addison-Wesley",
					BuildingAndStreet = "75 Arlington Street",
					State = "Massachusetts",
					Country = "United States"
				},
				new Publisher
				{
					Id = 9,
					Name = "Addison-Wesley",
					BuildingAndStreet = "501 Boylston Street",
					State = "Massachusetts",
					Country = "United States"
				},
				new Publisher
				{
					Id = 10,
					Name = "O'Reilly Media",
					BuildingAndStreet = "1005 Gravenstein Highway North",
					State = "California",
					Country = "United States"
				}
			);
		}
	}
}
