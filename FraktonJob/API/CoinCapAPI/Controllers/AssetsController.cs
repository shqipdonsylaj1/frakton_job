using CoinCapAPI.DTO;
using CoinCapAPI.Helpers;
using Core.Entities.CoinsCap;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProMaker.Arch.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinCapAPI.Controllers
{
    [Authorize]
    public class AssetsController : BaseAPIController
    {
        #region Properties
        private readonly AssetsExtensions _assetsExtensions;
        #endregion

        #region Constructor
        public AssetsController(AssetsExtensions assetsExtensions)
        {
            _assetsExtensions = assetsExtensions ?? throw new ArgumentNullException(nameof(assetsExtensions));
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<ActionResult<List<CoinsData>>> GetCoins([FromQuery] QuerySpecParam querySpec)
        {
            return await _assetsExtensions.GetCoinsValue(querySpec);
        }
    }
    #endregion
}
