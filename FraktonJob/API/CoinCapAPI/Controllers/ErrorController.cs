using Microsoft.AspNetCore.Mvc;
using ProMaker.Arch.Errors;
using ProMaker.Arch.Helpers;

namespace CoinCapAPI.Controllers
{
    [Route("/errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseAPIController
    {
        #region Methods
        public IActionResult Error(int code)
        {
            return new ObjectResult(new APIResponse(code));
        }
        #endregion
    }
}
