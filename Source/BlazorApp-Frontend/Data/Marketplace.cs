using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlazorApp_Frontend.Data
{
    public class Marketplace
    {
        public int MarketplaceID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }


        [JsonProperty("marketplacesellers")]
        public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
