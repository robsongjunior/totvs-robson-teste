using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Application.DTO.Models.Response;
using Robson_Totvs_Test.Configuration.TokenService;
using Robson_Totvs_Test.Domain.Entities;
using System.Net;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        UserManager<Account> _userManager;
        SignInManager<Account> _signInManager;
        ITotvsTokenService _tokenService;

        public AuthController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            ITotvsTokenService tokenService)
        {
            this._userManager = userManager;
            this._tokenService = tokenService;
            this._signInManager = signInManager;
        }

        [HttpPost("/auth/login")]
        public async Task<IActionResult> Login([FromBody] CreateLoginRequestDTO request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
                return StatusCode(
                    statusCode: (int)HttpStatusCode.NotFound,
                    value: new GetErrorResponseDTO(HttpStatusCode.NotFound, $"User not found with the requested email: {request.Email}"));

            var loginSuccessResult = await _signInManager.PasswordSignInAsync(existingUser, request.Password, false, false);
            if (loginSuccessResult.Succeeded == false)
                return StatusCode(
                    statusCode: (int)HttpStatusCode.Unauthorized,
                    value: new GetErrorResponseDTO(HttpStatusCode.Unauthorized, "Invalid username/password"));

            var myToken = await _tokenService.GenerateTokenAsync(existingUser.UserName);

            return Ok(myToken);
        }
    }
}
