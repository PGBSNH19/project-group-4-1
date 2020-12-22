namespace BlazorApp_Frontend.Data
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        //public ICollection<UserProduct> UserProducts { get; set; }
        //public ICollection<SellerPageProduct> SellerPageProducts { get; set; }
    }
}
