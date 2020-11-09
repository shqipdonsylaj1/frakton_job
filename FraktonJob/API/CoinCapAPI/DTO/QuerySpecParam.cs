using Newtonsoft.Json;

namespace CoinCapAPI.DTO
{
    public class QuerySpecParam
    {
        #region Properties
        public string Id { get; set; }
        public string Rank { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Supply { get; set; }
        public string maxsupply { get; set; }
        #endregion

        #region Constructor
        public QuerySpecParam()
        {
            Id = "";
            Rank = "";
            Symbol = "";
            Name = "";
            Supply = "";
            maxsupply = "";
        }
        #endregion
    }
}
