using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProMaker.Arch.Errors;
using ProMaker.Arch.Helpers;
using ProMaker.Arch.ITokenServices;
using SignInAPI.DTO;
using System;
using System.Threading.Tasks;

namespace SignInAPI.Controllers
{
    public class SignInController : BaseAPIController
    {
        #region Properties
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public SignInController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        #endregion

        #region Methods
        [HttpPost]
        public async Task<ActionResult<ResponseUserDTO>> LogIn(LogInDTO logInDTO)
        {
            var user = await _userManager.FindByEmailAsync(logInDTO.Email);
            if (user == null) return Unauthorized(new APIResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, logInDTO.Password, false);
            if (!result.Succeeded) return Unauthorized(new APIResponse(401));
            return new ResponseUserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user.Email, user.DisplayName)
            };
        }
        #endregion
    }
}
