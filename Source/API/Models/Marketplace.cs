using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Marketplace
    {
        [Key] 
        public int ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
