namespace API.Dtos
{
    public class SellerPageProductDto
    {
        public int SellerPageID { get; set; }
        public SellerPageDto sellerPage { get; set; }
        public int ProductID { get; set; }
        public ProductDto product { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
    }
}
