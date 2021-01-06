using System;
using System.Collections.Generic;

namespace API.Dtos
{
    public class MarketplaceDto
    {
        public int MarketplaceID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }
        public string Image { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public ICollection<MarketplaceSellerDto> MarketplaceSellers { get; set; }
    }
}
