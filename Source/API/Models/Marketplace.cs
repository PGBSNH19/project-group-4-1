using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Marketplace
    {
        [Key]
        public int MarketplaceID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public byte[] PictureBytes { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }
    }
}
