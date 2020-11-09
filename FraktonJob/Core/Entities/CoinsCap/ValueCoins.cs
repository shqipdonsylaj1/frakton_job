using Newtonsoft.Json;

namespace Core.Entities.CoinsCap
{
    public class ValueCoins
    {
        #region Properties
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("rank")]
        public string Rank { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("supply")]
        public string Supply { get; set; }
        [JsonProperty("maxsupply")]
        public string maxsupply { get; set; }
        #endregion
    }
}
