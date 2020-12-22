using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlazorApp_Frontend.Data
{
    public class Marketplace
    {
        [JsonProperty("marketplaceid")]
        public int MarketplaceID { get; set; }


        [JsonProperty("name")]
        public string Name { get; set; }


        [JsonProperty("location")]
        public string Location { get; set; }


        [JsonProperty("startdatetime")]
        public DateTime StartDateTime { get; set; }


        [JsonProperty("enddatetime")]
        public DateTime EndDateTime { get; set; }


        [JsonProperty("marketplacesellers")]
        public ICollection<MarketplaceSeller> MarketplaceSellers { get; set; }
    }
}
