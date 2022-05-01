using Newtonsoft.Json;


namespace CryptoAPI.Service
{
    public class CurrencyModel
    {
        [JsonProperty("symbol")]
        public string symbol { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("current_price")]
        public decimal currentPrice { get; set; }
        [JsonProperty("market_cap")]
        public long marketCap { get; set; }

    }
}
