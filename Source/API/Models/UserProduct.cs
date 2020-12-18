using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserProduct
    {
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User user { get; set; }

        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public Product product { get; set; }
    }
}
