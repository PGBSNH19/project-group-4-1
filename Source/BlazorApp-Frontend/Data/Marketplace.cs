using System;

namespace BlazorApp_Frontend.Data
{
    public class Marketplace
    {
        public int MarketplaceID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        //public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }
    }
}
