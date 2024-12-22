namespace BookManager.web.Data.DataAccess.CartRepo.DTO
{
    public class CartItemDto
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public decimal BookPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string? BookImage { get; set; }
    }

}
