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
    public class AccountController : ControllerBase
    {
        UserManager<Account> _userManager;

        public AccountController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager)
        {
            this._userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody][FromForm][FromQuery][Required] CreateAccountDTO request)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                return BadRequest("Email já existe.");

            var myProfiles = request.Profiles.Select(x => new ProfileObject(x.Type, null)).ToList();
            var myNewAccount = new Account(request.Name, request.Email, myProfiles);

            var result = await this._userManager.CreateAsync(myNewAccount, request.Password);

            if (result.Succeeded == false)
            {
                if (result.Errors.Any())
                    return BadRequest(result);

                return Problem(
                    statusCode: (int)HttpStatusCode.InternalServerError,
                    detail: "Erro ao criar o usuário.");
            }

            return Ok(myNewAccount);
        }
    }
}
