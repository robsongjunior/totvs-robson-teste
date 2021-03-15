using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Robson_Totvs_Test.Application.DTO.Models.Request;
using Robson_Totvs_Test.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Robson_Totvs_Test.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        UserManager<Account> _userManager;
        SignInManager<Account> _signInManager;
        public AuthController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager)
        {
            this._userManager = userManager;
        }

        public async Task<IActionResult> Login(CreateLoginRequestDTO request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
                return NotFound($"User not found with the requested email: {request.Email}");

            var result = await _signInManager.PasswordSignInAsync(existingUser, request.Password, false, false);


            return Ok();
        }
    }
}
