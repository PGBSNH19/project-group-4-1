namespace BlazorApp_Frontend.Data
{
    public class UserProduct
    {
        public int UserID { get; set; }
        public User user { get; set; }
        public int ProductID { get; set; }
        public Product product { get; set; }
    }
}
