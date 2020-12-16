using System.Collections.Generic;

namespace API.Models
{
    public class Reservations
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public List<Products> Products { get; set; }
        public int TotalCost { get; set; }
    }
}
