namespace BookManager.web.Data.DataAccess.OrderRepo.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string BookTitle { get; set; }
        public string BookImage { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

}
