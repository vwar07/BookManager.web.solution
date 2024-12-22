namespace BookManager.web.Data.DataAccess.BookRepo.DTO
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

}
