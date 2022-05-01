using CryptoAPI.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoAPI.Service
{
    public class OnlineMarketCap
    {
        public string Url { get; set; } = "https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc";
        public OnlineMarketCap()
        {

        }

        public async Task GetOnlineMarketCapAsync()
        {
            using HttpClient client = new();
            var httpResponse = await client.GetAsync(Url);
            var content = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<List<CurrencyModel>>(content);
            if (response != null)
            {
                var listResponse = response.Where(p => p.symbol == "btc" || p.symbol == "sand" || (p.symbol == "mim" || p.symbol == "magic")).ToList();


                if (listResponse.Count != 0)
                {
                    using var context = new CryptoDatabaseContext();
                    foreach (var item in listResponse)
                    {
                        if (item.symbol == "mim")
                            item.symbol = "magic";
                        var currencyItem = await context.Currencysymbols.Where(p => p.Symbol.ToLower() == item.symbol).FirstOrDefaultAsync();
                        if (currencyItem != null)
                        {
                            currencyItem.Price = item.currentPrice;
                            currencyItem.MarketCap = Convert.ToString(item.marketCap);
                            context.Update(currencyItem);
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
