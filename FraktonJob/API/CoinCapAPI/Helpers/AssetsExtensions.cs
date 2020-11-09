using CoinCapAPI.DTO;
using Core.Entities.CoinsCap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProMaker.Arch.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinCapAPI.Helpers
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AssetsExtensions : BaseAPIController
    {
        #region Properties
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public AssetsExtensions(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        #endregion

        #region Methods
        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public async Task<ActionResult<List<CoinsData>>> GetCoinsValue(QuerySpecParam querySpec)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GetSection("CoinsAPI").GetSection("Url").Value)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await client.GetAsync(_configuration.GetSection("CoinsAPI").GetSection("Url").Value);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var coins = JsonConvert.DeserializeObject<CoinsData>(result);
                List<ValueCoins> valueCoins = coins.Data;
                var values = valueCoins.FindAll(x =>
                      x.Id == querySpec.Id ||
                      x.Rank == querySpec.Rank ||
                      x.Symbol == querySpec.Symbol ||
                      x.Name == querySpec.Name ||
                      x.Supply == querySpec.Supply ||
                      x.maxsupply == querySpec.maxsupply);
                if (values.Count > 0)
                {
                    return Ok(values);
                }
                return Ok(valueCoins);
            }
            return NotFound("error");
        }
        #endregion
    }
}
