namespace API.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserType userType { get; set; }
    }
    public enum UserType
    {
        Admin,
        Buyer,
        Seller
    }
}
