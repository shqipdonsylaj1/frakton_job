using AutoMapper;
using Core.Entities.Identity;
using IdentityAPI.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProMaker.Arch.Errors;
using ProMaker.Arch.Helpers;
using ProMaker.Arch.ITokenServices;
using System;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    public class AccountController : BaseAPIController
    {
        #region Properties
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public AccountController(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        #endregion

        #region Methods
        [HttpPost("register")]
        public async Task<ActionResult<ResponseUserDTO>> Register(RegisterDTO registerDTO)
        {
            if (CheckEmailExistsAsync(registerDTO.Email).Result.Value)
            {
                return new BadRequestObjectResult(new APIValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }
            var users = _mapper.Map<RegisterDTO, Users>(registerDTO);
            var account = new AppUser
            {
                Email = registerDTO.Email,
                DisplayName = users.FirstName + " " + users.LastName,
                UserName = registerDTO.Email,
                Users = users
            };
            var result = await _userManager.CreateAsync(account, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(new APIResponse(400));
            return new ResponseUserDTO
            {
                DisplayName = account.DisplayName,
                Email = account.Email,
                Token = _tokenService.CreateToken(account.Email, account.DisplayName)
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
        #endregion
    }
}
