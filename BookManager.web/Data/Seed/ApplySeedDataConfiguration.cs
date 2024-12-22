

using Microsoft.EntityFrameworkCore;

namespace BookManager.web.Data.Seed
{
	public class ApplySeedDataConfiguration
	{
		private readonly IServiceProvider _services;
        public ApplySeedDataConfiguration(IServiceProvider services)
        {
			_services = services;
        }
		public async Task ApplyExistingMigrationAsync()
		{
			using var scope = _services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<BookManagerDbContext>();
			var pendingMigration = context.Database.GetPendingMigrations();
			if (context.Database.GetPendingMigrations().Any())
			{
				await context.Database.MigrateAsync();
			}
		}

        public async Task SeedStoredProceduresAsync()
        {
            using var scope = _services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BookManagerDbContext>();

            var dropProcedureSql = @"
                        IF OBJECT_ID('dbo.GetBooksWithFilters', 'P') IS NOT NULL
                            DROP PROCEDURE dbo.GetBooksWithFilters;
                    ";

            var createProcedureSql = @"

                create procedure GetBooksWithFilters
                    @Search nvarchar(MAX) = null,
                    @SortBy nvarchar(50) = 'default',
                    @PageNumber int = 1,
                    @PageSize int = 10
                as
                begin
                    set nocount on;

                    declare @Offset int = (@PageNumber - 1) * @PageSize;
                    select 
                        b.Id,
                        b.CreatedDateTime,
                        b.UpdatedDateTime,
                        b.IsDeleted,
                        b.DeletedDateTime,
                        b.Title,
                        b.Description,
                        b.PublisherId,
                        p.Id AS PublisherId,
                        p.CreatedDateTime AS PublisherCreatedDateTime,
                        p.UpdatedDateTime AS PublisherUpdatedDateTime,
                        p.IsDeleted AS PublisherIsDeleted,
                        p.DeletedDateTime AS PublisherDeletedDateTime,
                        p.Name AS PublisherName,
                        p.BuildingAndStreet AS PublisherBuildingAndStreet,
                        p.State AS PublisherState,
                        p.Country AS PublisherCountry,
                        b.AuthorId,
                        a.Id AS AuthorId,
                        a.CreatedDateTime AS AuthorCreatedDateTime,
                        a.UpdatedDateTime AS AuthorUpdatedDateTime,
                        a.IsDeleted AS AuthorIsDeleted,
                        a.DeletedDateTime AS AuthorDeletedDateTime,
                        a.FirstName AS AuthorFirstName,
                        a.LastName AS AuthorLastName,
                        a.Email AS AuthorEmail,
                        a.PhoneNumber AS AuthorPhoneNumber,
                        b.Price,
                        b.ImageLocation,
                        b.IsAvailable,
                        b.AvailableQuantity
                    from Book b
                    left join Publisher p on b.PublisherId = p.Id
                    left join Author a on b.AuthorId = a.Id
                    where (@Search is null or 
                           b.Title like '%' + @Search + '%' or
                           (a.FirstName + ' ' + a.LastName) like '%' + @Search + '%' or
                           p.Name like '%' + @Search + '%')
                    order by
                        case when @SortBy = 'publisherAuthorTitle' then p.Name end asc,
                        case when @SortBy = 'publisherAuthorTitle' then a.LastName end asc,
                        case when @SortBy = 'publisherAuthorTitle' then a.FirstName end asc,
                        case when @SortBy = 'publisherAuthorTitle' then b.Title end asc,
                        case when @SortBy = 'authorTitle' then a.LastName end asc,
                        case when @SortBy = 'authorTitle' then a.FirstName end asc,
                        case when @SortBy = 'authorTitle' then b.Title end asc,
                        case when @SortBy = 'title' then b.Title end asc,
                        case when @SortBy = 'priceLowHigh' then b.Price end asc,
                        case when @SortBy = 'priceHighLow' then b.Price end asc,
                        b.Id
                    offset @Offset rows fetch next @PageSize rows only;

                    select count(*) AS TotalCount
                    from Book b
                    left join Publisher p ON b.PublisherId = p.Id
                    left join Author a ON b.AuthorId = a.Id
                    where (@Search is null or 
                           b.Title like '%' + @Search + '%' or
                           (a.FirstName + ' ' + a.LastName) like '%' + @Search + '%' or
                           p.Name like '%' + @Search + '%');
                END;

            ";

            await context.Database.ExecuteSqlRawAsync(dropProcedureSql);

            await context.Database.ExecuteSqlRawAsync(createProcedureSql);
        }
    }
}
